using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Recommender.Views;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LandingPage : ContentPage
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        private async void Redirect(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new FirstOnboardingPage()).ConfigureAwait(false);
        }
    }
}