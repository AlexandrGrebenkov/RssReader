using System.Threading.Tasks;

namespace Controls.BasePages
{
    public interface IBasePage
    {
        Task<bool> OnNavigationBackButtonPressed();
        Task<bool> CanClose();
    }
}
