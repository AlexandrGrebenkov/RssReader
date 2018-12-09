using System.Threading.Tasks;
using Xamarin.Forms;

namespace Controls.BasePages
{
    public class BaseContentPage : ContentPage, IBasePage
    {
        public virtual Task<bool> OnNavigationBackButtonPressed() => Task.FromResult(true);

        public virtual Task<bool> CanClose() => Task.FromResult(true);
    }
}