using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweet.Picker.App.Messages;
using Tweets.Picker.Services.Model;

namespace Tweets.Picker.App.Services
{
    public interface ITwitterApplicationService
    {
        Task<TweetResponseMessage> GetTweetsByExpression(string expression);
        Task<TweetResponseMessage> InsertTweetsByExpression(string expression, string filename);
    }
}
