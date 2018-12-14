using RssReader.Services.Abstract;
using RssReader.Services.Mock;
using System;
using Xamarin.Forms;

namespace RssReader.Services.Providers
{
    /// <summary>
    /// Доступ к информации платформы 
    /// </summary>
    public static class PlatformInfoProvider
    {
        static Lazy<IPlatformInfo> implementation = new Lazy<IPlatformInfo>(Create, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        
        /// <summary>
        /// Доступ к текущей имплементации переферии
        /// </summary>
        public static IPlatformInfo Current
        {
            get
            {
                var ret = implementation.Value ?? new PlatformInfo_Mock();
                return ret;
            }
        }

        static IPlatformInfo Create() => DependencyService.Get<IPlatformInfo>();

        internal static Exception NotImplementedInReferenceAssembly() =>
            new NotImplementedException("Этот сервис не реализован на данной платформе");
    }
}
