using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweet.Picker.App.Messages;

namespace Tweets.Picker.Mappers
{
    public class TweetsMapper
    {
        public static TweetResponseMessage ToTweetsResponseMessage(List<Services.Model.Tweet> models)
        {
            if (models == null) return null;

            var responseMessage = ToMapperTweets(models);

            return responseMessage;
        }

        private static TweetResponseMessage ToMapperTweets(List<Services.Model.Tweet> models)
        {
            TweetResponseMessage responseMessage = new TweetResponseMessage();
            List<TweetMessage> tweetMessageList = new List<TweetMessage>();

            foreach (var tweet in models)
            {
                tweetMessageList.Add(new TweetMessage() { 
                    KeyWord = tweet.KeyWord,
                    Country = tweet.Country,
                    Location = tweet.Location,
                    Text  = tweet.Text,
                    TweetCreateAt = tweet.TweetCreateAt,
                    TwitterUserName = tweet.UserName,
                    UserName = tweet.UserName, 
                    Url = tweet.Url
                });
            }
            responseMessage.TweetsCount = tweetMessageList.Count;
            responseMessage.TweetResponseMessages = tweetMessageList;

            return responseMessage;

        }
    }
}
