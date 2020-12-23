using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Tweet.Picker.App.Messages
{
    public class TweetResponseMessage
    {
        [DataMember(Name = "tweetCount")]
        public int TweetsCount { get; set; }
        [DataMember(Name = "tweetResponseMessages")]
        public IEnumerable<TweetMessage> TweetResponseMessages { get; set; }
    }
}
