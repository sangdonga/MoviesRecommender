using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Recommender.Models
{
    public class Movie : IComparable
    {
        [JsonProperty]
        public string ItemID { get; set; }

        [JsonProperty]
        public string Title { get; set; }

        [JsonProperty]
        public List<string> Genre { get; set; }

        [JsonProperty]
        public string Year { get; set; }

        [JsonProperty]
        public string ImageUrl { get; set; }

        [JsonProperty]
        public string Overview { get; set; }

        [JsonProperty("prediction")]
        public string Prediction { get; set; }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }


    }
}
