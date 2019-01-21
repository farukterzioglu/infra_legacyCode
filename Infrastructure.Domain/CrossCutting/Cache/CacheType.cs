namespace Infrastructure.Domain.CrossCutting.Cache
{
    public enum CacheType
    {
        /// <summary>
        /// No cache type set
        /// </summary>
        Null = 0,
        Memory,
        NCacheExpress,
        AppFabric,
        Memcached,
        AzureTableStorage,
        Disk
    }
}
