using Recommender.Models;
using System;
using System.Diagnostics.Contracts;
using Xamarin.Forms;

namespace Recommender.ViewModels
{
    public class MovieDetailViewModel : BaseViewModel
    {

        private readonly UserMoviePreferences preferences;

        //the name of the icons to indicate if a movie is liked or not 
        private const string likedMovieIcon = "after_liked.png";
        private const string notLikedMovieIcon = "before_liked.png";

        public Movie Movie { get; }
        public Command<Movie> LikeClicked { get; }
        
        //property that is data-bound to the Like button image source 
        private string movieLikeButtonImageSource = notLikedMovieIcon;
        public string MovieLikeButtonImageSource {
            get{ return movieLikeButtonImageSource; }
            set{ SetProperty(ref movieLikeButtonImageSource, value); }
        }

        public MovieDetailViewModel(Movie movie, UserMoviePreferences preferences)
        {
            Contract.Requires(preferences != null);
            this.preferences = preferences;
            Movie = movie;
            MovieLikeButtonImageSource = IsMovieLiked() ? likedMovieIcon : notLikedMovieIcon;
            LikeClicked = new Command<Movie>(ToggleLike);
            preferences.CollectionChanged += UpdateMovieLikeButtonSource;
        }
        
        // Whenever user movie preferences gets updated, will check itself to see if it needs to update the like button source
        private void UpdateMovieLikeButtonSource(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MovieLikeButtonImageSource = IsMovieLiked() ? likedMovieIcon : notLikedMovieIcon;
        }

        // Toggles the like/not-liked state of the movie by adding or removing it to UserMoviePreferences and sets the appropriate like-button image 
        public void ToggleLike(Movie movie)
        {
            if (!preferences.RemoveIfPresent(movie))
            {
                preferences.AddPreference(movie);
                MovieLikeButtonImageSource = likedMovieIcon;
            }
            else
            {
                MovieLikeButtonImageSource = notLikedMovieIcon;
            }
        }

        // Returns if Movie is liked by user 
        private bool IsMovieLiked()
        {
            return preferences.HasMovie(Movie);
        }

        public override bool Equals(object obj)
        {
            var item = obj as MovieDetailViewModel;
            if (item == null)
            {
                return false;
            }

            return this.Movie.ItemID.Equals(item.Movie.ItemID, StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            return this.Movie.ItemID.GetHashCode();
        }
    }
}
