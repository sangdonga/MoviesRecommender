using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Recommender.ViewModels;
using Recommender.Models;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesPage : ContentPage
    {
        UserMoviePreferencesViewModel viewModel;
        UserMoviePreferences preferences = UserMoviePreferences.getInstance();
        private List<string> otherGenres = new List<string>(new string[] { "Animation", "Children's", "Crime", "Documentary", "Fantasy", "Film-Noir", "Musical", "Mystery", "Sci-Fi", "Thriller", "War", "Western" });
        public FavoritesPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new UserMoviePreferencesViewModel(async ex => await this.DisplayAlert("Error", ex.Message, "Ok").ConfigureAwait(false), UserMoviePreferences.getInstance());
            viewModel.UpdatePairedListForGenre(preferences.GetFilters());
        }

        private void RemovePreference(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.ImageButton)sender;
            viewModel.RemoveFromPreference((MovieDetailViewModel)item.CommandParameter);
        }

        private void genreButtonClicked(object sender, EventArgs e)
        {
            var button = (ImageButton)sender;
            var classId = button.ClassId;

            if (classId == "Action")
            {
                actionCheck.IsVisible = !actionCheck.IsVisible;
                updateFilter(actionCheck, classId);
            }
            else if (classId == "Comedy")
            {
                comedyCheck.IsVisible = !comedyCheck.IsVisible;
                updateFilter(comedyCheck, classId);
            }
            else if (classId == "Adventure")
            {
                adventureCheck.IsVisible = !adventureCheck.IsVisible;
                updateFilter(adventureCheck, classId);
            }
            else if (classId == "Drama")
            {
                dramaCheck.IsVisible = !dramaCheck.IsVisible;
                updateFilter(dramaCheck, classId);
            }
            else if (classId == "Horror")
            {
                horrorCheck.IsVisible = !horrorCheck.IsVisible;
                updateFilter(horrorCheck, classId);
            }
            else if (classId == "Romance")
            {
                romanceCheck.IsVisible = !romanceCheck.IsVisible;
                updateFilter(romanceCheck, classId);
            }
            else if (classId == "Other")
            {
                otherCheck.IsVisible = !otherCheck.IsVisible;
                foreach (string genre in otherGenres)
                {
                    updateFilter(otherCheck, genre);
                }
            }
            viewModel.UpdatePairedListForGenre(preferences.GetFilters());
        }

        private void OnItemSelected(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.StackLayout)sender;
            //await Navigation.PushModalAsync(new MovieDetailPage((MovieDetailViewModel)item.BindingContext)).ConfigureAwait(false);
        }

        void updateFilter(Image checkmark, String genre)
        {
            if (checkmark.IsVisible)
            {
                preferences.AddFilter(genre);
            }
            else
            {
                preferences.RemoveFilter(genre);
            }
        }
    }
}