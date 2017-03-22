using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WoodWorkingSA.Models
{
    public class YoutubeRecommendationsModel
    {
        public string id { get; set; }
        public string channel_name { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string image_url { get; set; }
        public DateTime date_created { get; set; }
        public DateTime date_updated { get; set; }
        
    }
}