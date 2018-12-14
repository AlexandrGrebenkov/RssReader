using System;
using System.IO;
using RssReader.Services.Abstract;

[assembly: Xamarin.Forms.Dependency(typeof(RssReader.Droid.Services.PlatformInfo_Droid))]
namespace RssReader.Droid.Services
{
    class PlatformInfo_Droid : IPlatformInfo
    {
        public string DbFileName => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "database.db");
    }
}