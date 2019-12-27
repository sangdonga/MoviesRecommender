using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Recommender.ViewModels;
using System.Globalization;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        SearchViewModel viewModel;
        public SearchPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new SearchViewModel(async ex => await this.DisplayAlert("Error", ex.Message, "Ok").ConfigureAwait(false), new Services.RestClient());
        }

        public SearchPage(string search)
        {
            InitializeComponent();
            userMovieQuery_entry.Text = search;
            BindingContext = viewModel = new SearchViewModel(async ex => await this.DisplayAlert("Error", ex.Message, "Ok").ConfigureAwait(false), new Services.RestClient());
            viewModel.GetSearchResults(userMovieQuery_entry.Text.ToString(CultureInfo.InvariantCulture), "1");
        }

        private void OnSearchClick(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.SearchBar)sender;
            viewModel.GetSearchResults(userMovieQuery_entry.Text.ToString(CultureInfo.InvariantCulture), "1");
        }

        private void ViewSettingsButtonClicked(object sender, EventArgs e)
        {

        }

        private async void OnItemSelected(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.StackLayout)sender;
            await Navigation.PushModalAsync(new MovieDetailPage((MovieDetailViewModel)item.BindingContext)).ConfigureAwait(false);
        }

        private async void ScanCode(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new SearchBarCodePage()).ConfigureAwait(false);
            await Navigation.PopAsync();
        }
    }
}