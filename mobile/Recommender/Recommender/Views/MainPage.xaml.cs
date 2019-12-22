using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace Recommender.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : Xamarin.Forms.TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            InitTabbedPage();
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom)
                .SetIsSmoothScrollEnabled(true)
                .SetIsSwipePagingEnabled(true);
        }

        public void InitTabbedPage()
        {
            NavigationPage browsepage, searchpage, favoritespage;
            browsepage = new NavigationPage(new BrowsePage())
            {
                IconImageSource = "home.xml",
                Title = "Home"
            };

            searchpage = new NavigationPage(new SearchPage())
            {
                IconImageSource = "search.xml",
                Title = "Search"
            };

            favoritespage = new NavigationPage(new FavoritesPage())
            {
                IconImageSource = "favorite.xml",
                Title = "Favorites"
            };

            Children.Add(browsepage);
            Children.Add(searchpage);
            Children.Add(favoritespage);
        }
    }
}