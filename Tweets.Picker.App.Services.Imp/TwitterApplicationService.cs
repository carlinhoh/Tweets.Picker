using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tweet.Picker.App.Messages;
using Tweets.Picker.Mappers;
using Tweets.Picker.Services;
using Tweets.Picker.Services.Model;

namespace Tweets.Picker.App.Services.Imp
{
    public class TwitterApplicationService : ITwitterApplicationService
    {
        private ITwitterService TwitterService { get; }
        public TwitterApplicationService(ITwitterService twitterService)
        {
            TwitterService = twitterService;
        }
        public async Task<TweetResponseMessage> GetTweetsByExpression(string expression)
        {
            var result = await TwitterService.GetTweetsByExpression(expression);

            return TweetsMapper.ToTweetsResponseMessage(result);
        }

        public async Task<TweetResponseMessage> InsertTweetsByExpression(string expression, string filename)
        {
            var result =  TwitterService.InsertTweetsByExpression(expression, filename);

            return TweetsMapper.ToTweetsResponseMessage(result);
        }

    }
}
