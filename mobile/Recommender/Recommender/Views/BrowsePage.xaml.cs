using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Recommender.Models;
using Recommender.ViewModels;
using System.ComponentModel;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowsePage : ContentPage
    {
        public BrowseViewModel ViewModel { get; set; }
        public BrowsePage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new BrowseViewModel(async ex => await this.DisplayAlert("Error", ex.Message, "Ok").ConfigureAwait(false), UserMoviePreferences.getInstance(), new Services.RestClient());
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel.RecommendedMovies.Count == 0)
                _ = ViewModel.RefreshRequestRecommendations();
        }

        private async void ViewSettingsButtonClicked(object sender, EventArgs e)
        {
            //await Navigation.PushModalAsync(new SettingsPage()).ConfigureAwait(false);
        }

        async void OnItemSelected(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.StackLayout)sender;
            await Navigation.PushModalAsync(new MovieDetailPage((MovieDetailViewModel)item.BindingContext)).ConfigureAwait(false);
        }
    }
}