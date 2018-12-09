using Controls.BasePages;
using Helpers;
using RssReader.Models;
using RssReader.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RssReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewRssPage : BaseContentPage
    {
        public AddNewRssPage()
        {
            InitializeComponent();
            BindingContext = new AddNewRssVM(Navigation);
        }

        public AddNewRssPage(Rss rss)
        {
            InitializeComponent();
            BindingContext = new AddNewRssVM(Navigation, rss);
        }

        protected override bool OnBackButtonPressed()
        {
            var VM = (AddNewRssVM)BindingContext;
            return !VM.cmdSave.CanExecute(null);
        }

        public async override Task<bool> CanClose()
        {
            var VM = (AddNewRssVM)BindingContext;
            if (VM.IsChanged)
            {
                string answer = null;
                if (VM.cmdSave.CanExecute(null))
                    answer = await DisplayActionSheet("Сохранить изменения?",
                                "Отмена", "",
                                "Сохранить и выйти", "Выйти без сохранения");
                else
                {
                    if (await DisplayAlert("Внимание!",
                                    "Ошибка ввода данных",
                                    "Выйти без сохранения", "Вернуться назад"))
                        return true;
                    else
                        return false;
                }

                if (answer == "Сохранить и выйти")
                {
                    VM.Save();
                    return true;
                }
                if (answer == "Выйти без сохранения")
                    return true;
                return false;
            }
            else
                return true;
        }

        public override async Task<bool> OnNavigationBackButtonPressed()
        {
            return await CanClose();
        }

        #region Для отображения диалоговых окон из VM

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (BindingContext is BaseViewModel VM)
            {
                VM.DisplayAlert = DisplayAlertFromVM;
                VM.DisplayActionSheet = DisplayActionSheetFromVM;
            }
        }

        async Task<bool> DisplayAlertFromVM(string title, string message, string ok, string cancel) =>
            await DisplayAlert(title, message, ok, cancel);

        async Task<string> DisplayActionSheetFromVM(string title, string cancel, string destruction, params string[] buttons) =>
            await DisplayActionSheet(title, cancel, destruction, buttons);

        #endregion
    }
}