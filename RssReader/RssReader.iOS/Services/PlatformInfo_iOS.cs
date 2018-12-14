using System;
using System.IO;
using RssReader.Services.Abstract;

[assembly: Xamarin.Forms.Dependency(typeof(RssReader.iOS.Services.PlatformInfo_iOS))]
namespace RssReader.iOS.Services
{
    class PlatformInfo_iOS : IPlatformInfo
    {
        public string DbFileName => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", "database.db");
    }
}