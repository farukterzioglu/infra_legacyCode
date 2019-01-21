using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL
{
    //public class EntityBase
    //{
    //    [Key]
    //    public int Id { get; set; }

    //    internal List<string> ChangedColumnNames = new List<string>();
    //    protected void OnPropChanged(string propName)
    //    {
    //        if (!ChangedColumnNames.Contains(propName))
    //        {
    //            //iceride degisiklikleri tutmak istersek
    //            //changedColumnNames.Add (propName);

    //            //dısarıda tutmak istersek
    //            //ChangeTrackingManager.Instance().Add(this, propName);
    //        }
    //    }

    //    public EntityBase ShallowCopy()
    //    {
    //        return (EntityBase)this.MemberwiseClone();
    //    }
    //}
}
