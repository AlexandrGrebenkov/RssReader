﻿using Helpers;
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
	public partial class AddNewRssPage : ContentPage
	{
		public AddNewRssPage ()
		{
			InitializeComponent ();
            BindingContext = new AddNewRssVM(Navigation);
		}

        public AddNewRssPage(Rss rss)
        {
            InitializeComponent();
            BindingContext = new AddNewRssVM(Navigation, rss);
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