using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tweets.Picker.Services.Model;

namespace Tweets.Picker.Services
{
    public interface ITwitterService
    {
        Task<List<Tweet>> GetTweetsByExpression(string expression);

        List<Tweet> InsertTweetsByExpression(string expression, string filename);
    }
}
