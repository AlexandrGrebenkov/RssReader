using Plugin.Multilingual;
using RssReader.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace RssReader
{
    public partial class App : Application
    {
        public App()
        {
            RssReader.Resources.Lang.Common.Culture = CrossMultilingual.Current.DeviceCultureInfo; 
            RssReader.Resources.Lang.Titles.Culture = CrossMultilingual.Current.DeviceCultureInfo; 

            InitializeComponent();

            MainPage = new NavigationPage(new RssListPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
