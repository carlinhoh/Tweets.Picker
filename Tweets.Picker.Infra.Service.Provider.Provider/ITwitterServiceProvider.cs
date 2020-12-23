using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweets.Picker.Services.Model;

namespace Tweets.Picker.Infra.Service.Provider.TwitterGateway
{
    public interface ITwitterServiceProvider
    {
        Task<string> ReadStremByWord(string word);
        Task<List<Tweet>> GetTweetsByExpression(string word);

    }
}
