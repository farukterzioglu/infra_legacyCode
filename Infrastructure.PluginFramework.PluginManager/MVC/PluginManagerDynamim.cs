using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Hosting;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System.Runtime;
using Infrastructure.AspectOriented.Aspects;
using Infrastructure.CrossCutting.IocManager;
using Infrastructure.PluginFramework.Core;
using Infrastructure.WebApps.Common;
using Infrastructure.PluginFramework.Core.MVC;

namespace Infrastructure.PluginFramework.PluginManager.MVC
{
    public class PluginManagerContext
    {
        public PluginManagerContext(IUserClaimsManager userClaimsManager, DirectoryInfo pluginFolder, DirectoryInfo shadowCopyFolder)
        {
            PluginFolder = pluginFolder;
            ShadowCopyFolder = shadowCopyFolder;
            UserClaimsManager = userClaimsManager;
        }
        public PluginManagerContext(IUserClaimsManager userClaimsManager)
        {
            UserClaimsManager = userClaimsManager;
        }

        public DirectoryInfo PluginFolder;
        public DirectoryInfo ShadowCopyFolder;
        public IUserClaimsManager UserClaimsManager;
    }

    //TODO : Register to IoC  (in PreApplicationStartMethod. PreApplicationStartMethod uses plugin manager)
    public class PluginManagerDynamim
    {
        private static readonly Lazy<PluginManagerDynamim> _instance =
            new Lazy<PluginManagerDynamim>(() => new PluginManagerDynamim());
        public static PluginManagerDynamim Instance
        {
            get
            {
                return _instance.Value;
            }
        }

        // The source plugin folder from which to shadow copy from
        // This folder can contain sub folders to organize plugin types
        private static DirectoryInfo _pluginFolder;

        //The folder to shadow copy the plugin DLLs to use for running the app
        private static DirectoryInfo _shadowCopyFolder;

        //Claim manager
        private static IUserClaimsManager _userClaimsManager;

        //TODO : Aspect -> trace
        //TODO : Add trace information attribute
        static PluginManagerDynamim()
        {
            //TODO : Create folders if not exist

            _pluginFolder = HostingEnvironment.MapPath("~/plugins") != null ? 
                new DirectoryInfo(HostingEnvironment.MapPath("~/plugins")) : 
                new DirectoryInfo(@"c:\plugins");

            _shadowCopyFolder = HostingEnvironment.MapPath("~/plugins/temp") != null ? 
                new DirectoryInfo(HostingEnvironment.MapPath("~/plugins/temp")) : 
                new DirectoryInfo(@"c:\plugins");
            
            //TODO : How to set default
            _userClaimsManager = null;

            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += (s, e) =>
            {
                var a = Assembly.ReflectionOnlyLoad(e.Name);
                if (a == null) throw new TypeLoadException("Could not load assembly " + e.Name);
                return a;
            };
        }

        public void SetContext(PluginManagerContext context)
        {
            #region Plugin folder
            if (context.PluginFolder.Exists)
                    _pluginFolder = context.PluginFolder;
                else
                {
                    //TODO : Create folder
                    _pluginFolder = context.PluginFolder;
                }
            #endregion

            #region Plugin shadow copy folder
            if (context.ShadowCopyFolder.Exists)
                _shadowCopyFolder = context.ShadowCopyFolder;
            else
            {
                //TODO : Create folder
                _shadowCopyFolder = context.ShadowCopyFolder;
            }
            #endregion

            #region User claim manager 

            if (context.UserClaimsManager != null)
            {
                _userClaimsManager = context.UserClaimsManager;
            }

            #endregion
        }

        //private static readonly List<IPlugin> _pluginList;
        private static readonly List<string> PluginNames = new List<string>();
        
        public List<string> GetPluginNames()
        {
            if (_userClaimsManager == null)
            {
                //TODO : Log : claim manager is null
                return null;
            }

            var allowedPlugins = PluginNames.Where(x => _userClaimsManager.CheckUserClaim(x)).ToList();
            return allowedPlugins;
        }


        ////TODO : Aspect -> trace
        //public void AddPlugin(IPlugin plugin)
        //{
        //    _pluginList.Add(plugin);
        //}

        public void InitializePlugins()
        {
            Directory.CreateDirectory(_shadowCopyFolder.FullName);

            //Clear out plugins)
            foreach (var f in _shadowCopyFolder.GetFiles("*.dll", SearchOption.AllDirectories))
            {
                try { f.Delete(); }
                catch (Exception)
                { // ignored 
                }
            }

            //Shadow copy files
            var dllFiles = _pluginFolder.GetFiles("*.dll", SearchOption.AllDirectories);
            var pluginFolders = dllFiles.Select(x => x.Directory.FullName).Distinct().ToList();

            foreach (var plug in dllFiles)
            {
                var di = Directory.CreateDirectory(Path.Combine(_shadowCopyFolder.FullName, plug.Directory.Name));
                // NOTE: You cannot rename the plugin DLL to a different name, it will fail because the assembly name is part if it's manifest
                // (a reference to how assemblies are loaded: http://msdn.microsoft.com/en-us/library/yx7xezcf )

                try
                {
                    File.Copy(plug.FullName, Path.Combine(di.FullName, plug.Name), true);
                }
                catch (Exception)
                { // ignored 
                }
            }

            List<Assembly> allFoundDlls = new List<Assembly>();
            foreach (var directoryInfo in pluginFolders)
            {
                string[] errorMessages;
                var foundDlls = PluginBinaryInspector.ScanAssembliesForTypeReference<IPlugin>(directoryInfo, out errorMessages);
                allFoundDlls.AddRange(foundDlls);
            }
            var pluginDlls = allFoundDlls.GroupBy( x=> x.FullName).Select(x => x.FirstOrDefault()).ToList();

            ////For every plug-in dll in shadow copy folder ->
            //var pluginShadowCopies = _shadowCopyFolder.GetFiles("*.dll", SearchOption.AllDirectories);


            ////Load all dll to 'Reflection only context'
            //List<string> errors = new List<string>();
            //var assembliesWithErrors = new List<string>();
            //foreach (var f in pluginShadowCopies.Select( x=> x.FullName))
            //{
            //    Assembly reflectedAssembly;
            //    try
            //    {
            //        reflectedAssembly = Assembly.ReflectionOnlyLoadFrom(f);
            //    }
            //    catch (FileLoadException)
            //    {
            //        continue;
            //    }

            //    foreach (var assemblyName in reflectedAssembly.GetReferencedAssemblies())
            //    {
            //        try
            //        {
            //            Assembly.ReflectionOnlyLoad(assemblyName.FullName);
            //        }
            //        catch (FileNotFoundException)
            //        {
            //            //if an exception occurs it means that a referenced assembly could not be found                        
            //            errors.Add(
            //                string.Concat("This package references the assembly '",
            //                    assemblyName.Name,
            //                    "' which was not found, this package may have problems running"));
            //            assembliesWithErrors.Add(f);
            //        }
            //        catch (FileLoadException)
            //        {
            //            //if an exception occurs it means that a referenced assembly could not be found                        
            //            errors.Add(
            //                string.Concat("This package references the assembly '",
            //                    assemblyName.Name,
            //                    "' which was not found, this package may have problems running"));
            //            assembliesWithErrors.Add(f);
            //        }
            //    }
            //}

            //foreach (var f in pluginShadowCopies.Select(x => x.FullName).Except(assembliesWithErrors))
            //{
            //    //now we need to see if they contain any type 'MyType'
            //    var reflectedAssembly = Assembly.ReflectionOnlyLoadFrom(f);
            //    var found = reflectedAssembly.GetExportedTypes()
            //        .Where( x=> x.GetInterface(typeof(IPlugin).Name) != null);

            //    if (!found.Any()) continue;
            ////}


            ////foreach (var pluginAssemblyFile in pluginShadowCopies)
            ////{
            ////    //Load dll

            foreach (var pluginDll in pluginDlls)
            {
                var asm = Assembly.LoadFrom(pluginDll.Location);

                //check if it is a plug-in dll or not ->
                Type pluginType = asm.GetTypes().SingleOrDefault(x => x.GetInterface(typeof(IPlugin).Name) != null);

                if (pluginType != null)
                {
                    #region (Commented) Get controllers inside plugin
                    //var pluginControllerTypes =
                    //asm.GetTypes()
                    //    .Where(x => x.GetInterface(typeof(IPluginController).Name) != null && x.IsAbstract == false).ToList();

                    ////Add controllers to plugin instance
                    //foreach (Type pluginControllerType in pluginControllerTypes)
                    //{
                    //    IPluginController pluginControllerInstance = Activator.CreateInstance(pluginControllerType) as IPluginController;
                    //    if (pluginControllerInstance == null) continue;

                    //    Type type = pluginControllerInstance.GetType();
                    
                    //    //IoCManager.Container.RegisterType<IPluginController, Sample12Controller>("Sample12Controller",
                    //    //    new Interceptor<InterfaceInterceptor>(),
                    //    //    new InterceptionBehavior<PluginAuthorizationInterceptionBehavior>());

                    //    plgIns.ControllerList.Add(pluginControllerInstance);
                    //}
                    #endregion

                    var pluginTypeName = pluginType.Name;
                    ////Add plugin to manager
                    PluginNames.Add(pluginTypeName);

                    

                    //Register plugin
                    //TODO : Intercepts too much, for every call to every property
                    IoCManager.Container
                        .RegisterType(typeof(IPlugin), pluginType , pluginTypeName,
                            new Interceptor<InterfaceInterceptor>(),
                            new InterceptionBehavior<PluginAuthorizationBehavior>());

                    //Load controllers to assembly
                    try
                    {
                        BuildManager.AddReferencedAssembly(asm);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        //TODO : Implement this 
        public void AddProbingPaths()
        {
        }

        //TODO : Implement this 
        public void RegisterRoutes()
        {

        }
    }
}