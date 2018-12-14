using RssReader.Services.Abstract;
using System.IO;
using Windows.Storage;

[assembly: Xamarin.Forms.Dependency(typeof(RssReader.UWP.Services.PlatformInfo_UWP))]
namespace RssReader.UWP.Services
{
    class PlatformInfo_UWP : IPlatformInfo
    {
        public string DbFileName => Path.Combine(ApplicationData.Current.LocalFolder.Path, "database.db");
    }
}
