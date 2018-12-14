using Controls.BasePages;
using Helpers;
using RssReader.Models;
using RssReader.Resources.Lang;
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
            if (VM.IsChanged && VM.CanSave)
            {// Данные изменились и можем сохранить
                var answer = await DisplayActionSheet(Strings.SaveChanges,
                            Common.Cancel, "",
                            Strings.SaveAndExit, Strings.ExitWithoutSaving);
                if (answer == Strings.SaveAndExit)
                {
                    VM.Save();
                    return true;
                }
                else if (answer == Strings.ExitWithoutSaving)
                {
                    return true;
                }
                else return false;
            }
            else if (VM.IsChanged)
            {// ошибка ввода данных (нельзя сохранять)
                if (await DisplayAlert(Common.Attention,
                                Strings.InputDataError,
                                Strings.ExitWithoutSaving, Strings.ComeBack))
                    return true;
                else
                    return false;
            }
            else
                return true;// Выход без изменений
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