using Android.Content;
using Android.Support.V4.Widget;
using Android.Views;
using Controls.BasePages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AToolbar = Android.Support.V7.Widget.Toolbar;

[assembly: ExportRenderer(typeof(BaseContentPage), typeof(Droid.Controls.BaseContentPageRenderer))]
namespace Droid.Controls
{
    class BaseContentPageRenderer : PageRenderer
    {
        public BaseContentPageRenderer(Context context) : base(context)
        {

        }

        AToolbar toolbar = null;
        

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            MasterDetailPage mdPage;
            var element = Element;
            NavigationPage parent = element.Parent as NavigationPage;
            mdPage = parent.Parent as MasterDetailPage;
            var r = parent.GetRenderer();
            ViewGroup vg = r.ViewGroup;

            
            for (int i = 0; i < r.ViewGroup.ChildCount; i++)
            {
                var child = vg.GetChildAt(i);
                toolbar = child as AToolbar;
                if (toolbar != null)
                {
                    break;
                }
            }

            toolbar?.SetNavigationOnClickListener(new MenuClickListener(parent, mdPage));
        }

        protected override void OnDetachedFromWindow()
        {
            base.OnDetachedFromWindow();
            //toolbar?.SetNavigationOnClickListener(null);
        }

        private class MenuClickListener : Java.Lang.Object, IOnClickListener
        {
            readonly NavigationPage navigationPage;
            private DrawerLayout layout;
            MasterDetailPage mdPage;

            public MenuClickListener(NavigationPage navigationPage, MasterDetailPage layout)
            {
                this.navigationPage = navigationPage;
                this.mdPage = layout;
            }

            public async void OnClick(Android.Views.View v)
            {
                var page = navigationPage.CurrentPage as BaseContentPage;

                if (page != null)
                {
                    if (await page.OnNavigationBackButtonPressed())
                    {
                        navigationPage?.PopAsync();
                    }
                }
                else
                {
                    if (navigationPage.Navigation.NavigationStack.Count > 1)
                        navigationPage?.PopAsync();
                }

                if (navigationPage.Navigation.NavigationStack.Count <= 1)
                {
                    //layout?.OpenDrawer((int)GravityFlags.Left);
                    if (mdPage != null)
                    mdPage.IsPresented = true;
                }
            }
        }
    }
}