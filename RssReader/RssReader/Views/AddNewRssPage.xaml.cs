using Controls.BasePages;
using Helpers;
using RssReader.Models;
using RssReader.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;

namespace RssReader.Views
{
    /// <summary>
    /// Страница создания/редактирования RSS-Канала
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddNewRssPage : BaseContentPage
    {
        /// <summary>Создание нового Rss-канала</summary>
        public AddNewRssPage()
        {
            InitializeComponent();
            BindingContext = new AddNewRssVM(Navigation);
        }

        /// <summary>Редактирование</summary>
        /// <param name="rss">Канал, который нужно отредактировать</param>
        public AddNewRssPage(Rss rss)
        {
            InitializeComponent();
            BindingContext = new AddNewRssVM(Navigation, rss);
        }

        /// <summary>
        /// Навигация назад через аппаратную кнопку
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            var VM = (AddNewRssVM)BindingContext;
            return VM.IsChanged; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async override Task<bool> CanClose()
        {
            var VM = (AddNewRssVM)BindingContext;
            if (VM.IsChanged)
            {
                if (VM.cmdSave.CanExecute(null))
                {
                    var answer = await DisplayActionSheet("Сохранить изменения?",
                                "Отмена", "",
                                "Сохранить и выйти", "Выйти без сохранения");
                    switch (answer)
                    {
                        case "Сохранить и выйти":
                        {
                            VM.Save();
                            return true;
                        }
                        case "Выйти без сохранения":
                        {
                            return true;
                        }
                        default:
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (await DisplayAlert("Внимание!",
                                    "Ошибка ввода данных",
                                    "Выйти без сохранения", "Вернуться назад"))
                        return true;
                    else
                        return false;
                }
            }
            else
                return true;
        }

        /// <summary>
        /// Навигация назад в Android через стрелочку в ToolBar
        /// </summary>
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