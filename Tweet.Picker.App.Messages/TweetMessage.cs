using System;
using System.Runtime.Serialization;

namespace Tweet.Picker.App.Messages
{
    public class TweetMessage
    {
        [DataMember(Name = "userName")]
        public string UserName { get; set; }
        [DataMember(Name = "location")]
        public string Location { get; set; }
        [DataMember(Name = "country")]
        public string Country { get; set; }
        [DataMember(Name = "twitterUserName")]
        public string TwitterUserName { get; set; }
        [DataMember(Name = "text")]
        public string Text { get; set; }
        [DataMember(Name = "keyWord")]
        public string KeyWord { get; set; }
        [DataMember(Name = "url")]
        public string Url { get; set; }
        [DataMember(Name = "tweetCreateAt")]
        public DateTime TweetCreateAt { get; set; }
    }
}

