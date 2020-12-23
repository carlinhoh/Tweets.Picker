using System;

namespace Tweets.Picker.Services.Model
{
    public class Tweet
    {
        public string UserName { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }
        public string TwitterUserName { get; set; }
        public string Text { get; set; }
        public string KeyWord { get; set; }
        public string Url { get; set; }
        public DateTime TweetCreateAt { get; set; }
    }
}
