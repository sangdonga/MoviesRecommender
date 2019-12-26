using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Recommender.Models;
using Recommender.ViewModels;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieCustomPage : ContentPage
    {
        MovieCustomViewModel ViewModel;
        public MovieCustomPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new MovieCustomViewModel(UserMoviePreferences.getInstance(), new Services.RestClient());

            var prevItem = new ToolbarItem
            {
                Text = "**Prev**",
                IconImageSource = "prev",
                CommandParameter = false
            };
            prevItem.SetBinding(MenuItem.CommandProperty, nameof(ViewModel.PanPositionChangedCommand));

            var nextItem = new ToolbarItem
            {
                Text = "**Next**",
                IconImageSource = "next",
                CommandParameter = true
            };
            nextItem.SetBinding(MenuItem.CommandProperty, nameof(ViewModel.PanPositionChangedCommand));

            ToolbarItems.Add(prevItem);
            ToolbarItems.Add(nextItem);
        }

        private async void Redirect(object sender, EventArgs e)
        {
            if (ViewModel.ChangeButtonColor())
                await Navigation.PushModalAsync(new MainPage()).ConfigureAwait(false);
        }

        private void checkValid(object sender, EventArgs e)
        {

        }
    }
}