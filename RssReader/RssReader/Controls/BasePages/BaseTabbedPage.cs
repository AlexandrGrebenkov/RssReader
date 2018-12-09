using System.Threading.Tasks;
using Xamarin.Forms;

namespace Controls.BasePages
{
    public class BaseTabbedPage : TabbedPage, IBasePage
    {
        public virtual Task<bool> OnNavigationBackButtonPressed() => Task.FromResult(true);

        public virtual Task<bool> CanClose() => Task.FromResult(true);
    }
}
