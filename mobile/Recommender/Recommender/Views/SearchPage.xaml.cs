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

        private void OnSearchClick(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.SearchBar)sender;
            viewModel.GetSearchResults(userMovieQuery_entry.Text.ToString(CultureInfo.InvariantCulture), "1");
        }

        private void ViewSettingsButtonClicked(object sender, EventArgs e)
        {

        }

        private void OnItemSelected(object sender, EventArgs e)
        {

        }
    }
}