using Newtonsoft.Json;
using Recommender.Models;
using Recommender.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Recommender.Services
{
    public enum Genre
    {
        Action, Adventure, Animation, Comedy, Documentary, Drama, Horror, Romance
    }

    public class RestClient
    {

        private HttpClient _client;

        public RestClient()
        {
            _client = new HttpClient();
        }

        // Gets a list of recommended movies based off of the user's movie preferences 
        public virtual async Task<IList<Movie>> GetMovieRecommendationsFromPreferences(UserMoviePreferences preferences)
        {
            Dictionary<string, List<Movie>> movieList = null;
            HttpResponseMessage response = null;
            Contract.Requires(preferences != null);
            try
            {
                var builder = new UriBuilder(Resources.RecommendationEndpoint);
                builder.Query = "movies=" + HttpUtility.UrlPathEncode(string.Join("|", preferences.GetListOfMovieIDs().ToArray())) + "&alg=" + App.Algorithm;
                response = await _client.GetAsync(builder.Uri).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    movieList = JsonConvert.DeserializeObject<Dictionary<string, List<Movie>>>(content);
                }
                else
                {
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                throw HTTPExceptionHandler(ex);
            }
            return movieList["Movies"];
        }

        // Gets a list of popular movies from the backend
        public virtual async Task<IDictionary<string,List<Movie>>> GetPopularMovies(List<Genre> genres)
        {
            Dictionary<string, List<Movie>> movieList = null;
            HttpResponseMessage response = null;
            Contract.Requires(genres != null);
            try
            {
                var builder = new UriBuilder(Resources.PopularEndpoint);
                var genresAsStrings = new List<String>();
                genres.ForEach(genre => genresAsStrings.Add(genre.ToString()));
                builder.Query = "genres=" + HttpUtility.UrlPathEncode(string.Join("|", genresAsStrings));
                response = await _client.GetAsync(builder.Uri).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    movieList = JsonConvert.DeserializeObject<Dictionary<string, List<Movie>>>(content);
                }
                else
                {
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                throw HTTPExceptionHandler(ex);
            }
            return movieList;
        }

        // Grabs the movies that are similar to the user's search results through the backend
        public async Task<IList<Movie>> getSearchResults(string userInput, string pageNumber)
        {
            Dictionary<string, List<Movie>> movieList = null;
            HttpResponseMessage response = null;

            try
            {
                var url_builder = new UriBuilder(Resources.SearchEndpoint);
                url_builder.Query = "q=" + HttpUtility.UrlPathEncode(userInput) + "&page=" + HttpUtility.UrlPathEncode(pageNumber); // each page displays 5 movies. 1st page is first 5, 2nd is next 5, etc.
                response = await _client.GetAsync(url_builder.Uri).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    movieList = JsonConvert.DeserializeObject<Dictionary<string, List<Movie>>>(content);
                }
                else
                {
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                throw HTTPExceptionHandler(ex);
            }
            return movieList["Movies"];
        }

        // Fetches the personas list from the backend and returns it as a dictionary that has a string as a key that points 
        public async Task<Dictionary<string, List<Movie>>> GetPersonas()
        {
            Dictionary<string, List<Movie>> personasList = null;
            HttpResponseMessage response = null;
            try
            {
                var url_builder = new UriBuilder(Resources.PersonasEndpoint);
                response = await _client.GetAsync(url_builder.Uri).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    personasList = JsonConvert.DeserializeObject<Dictionary<string, List<Movie>>>(content);
                }
                else
                {
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                throw HTTPExceptionHandler(ex);
            }

            return personasList;
        }

        public async Task<IList<Movie>> GetMovieOnboarding(IList<string> genreList)
        {
            Dictionary<string, List<Movie>> movieList = null;

            HttpResponseMessage response = null;
            try
            {
                var builder = new UriBuilder(Resources.OnboardingEndpoint); ;
                builder.Query = "genres=" + HttpUtility.UrlPathEncode(string.Join("|", genreList));
                response = await _client.GetAsync(builder.Uri).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    movieList = JsonConvert.DeserializeObject<Dictionary<string, List<Movie>>>(content);
                }
                else
                {
                    throw new HttpRequestException(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                throw HTTPExceptionHandler(ex);
            }
            return movieList["Movies"];
        }

        // Handles HTTP exceptions by checking for internet connectivity issues.
        private static BadBackendRequestException HTTPExceptionHandler(Exception e)
        {
            if (e.InnerException.Message.ToString(CultureInfo.InvariantCulture).Contains("A connection with the server could not be established"))
            {
                throw new BadBackendRequestException(Resources.NoInternetMessage, e);
            }
            else
            {
                throw new BadBackendRequestException(Resources.InternalErrorMessage, e);
            }
        }
    }

    // Custom exception class for when there is a problem requesting recommendations from the backend service
    public class BadBackendRequestException : Exception
    {
        public BadBackendRequestException() { }
        public BadBackendRequestException(string message) : base(message) { }
        public BadBackendRequestException(string message, Exception exception) : base(message,exception) { }
    }

}