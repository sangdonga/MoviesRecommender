using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ZXing;
using ZXing.Net.Mobile.Forms;

using Recommender.Views;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchBarCodePage : ZXingScannerPage
    {
        public SearchBarCodePage()
        {
            InitializeComponent();
        }

        public async void Handle_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushModalAsync(new SearchPage(result.Text)).ConfigureAwait(false);
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            IsScanning = false;
        }
    }
}