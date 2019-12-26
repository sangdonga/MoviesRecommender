using Recommender.ViewModels;
using Recommender.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenresCustomPage : ContentPage
    {
        SelectGenresViewModel ViewModel;
        public GenresCustomPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new SelectGenresViewModel();
        }

        private async void RedirectToMovieCarousel(object sender, EventArgs e)
        {
            if (ViewModel.ChangeButtonColor())
            {
                await Navigation.PushModalAsync(new MovieCustomPage()).ConfigureAwait(false);
            }
        }
    }
}