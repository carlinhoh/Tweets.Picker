using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tweets.Picker.Infra.Data;
using Tweets.Picker.Infra.Service.Provider.TwitterGateway;
using Tweets.Picker.Services.Model;

namespace Tweets.Picker.Services.Imp
{
    public class TwitterService : ITwitterService
    {

        private ITwitterServiceProvider TwitterServiceProvider { get; }
        private ITweetRepository TwitterRepository { get; }
        public TwitterService(ITwitterServiceProvider twitterServiceProvider, ITweetRepository twitterRepository)
        {
            TwitterServiceProvider = twitterServiceProvider;
            TwitterRepository = twitterRepository;
        }
        public async Task<List<Model.Tweet>> GetTweetsByExpression(string expression)
        {
            return await TwitterServiceProvider.GetTweetsByExpression(expression);
        }

        public List<Model.Tweet> InsertTweetsByExpression(string expression, string filename)
        {
            List<Picker.Services.Model.Tweet> tweetsResult = new List<Tweet>();

            if (!string.IsNullOrEmpty(filename))
            {
                string[] lines;

                if (File.Exists(filename))
                {
                    lines = File.ReadAllLines(filename);
                    foreach (string line in lines)
                    {
                        var tweets = TwitterServiceProvider.GetTweetsByExpression(line).Result;
                        TwitterRepository.Insert(tweets);
                        Thread.Sleep(1000);
                        Console.WriteLine("Expressão {0} inserida com sucesso!", line);
                    }
                }
            }
            else
            {
                var tweets =  TwitterServiceProvider.GetTweetsByExpression(expression).Result;
                TwitterRepository.Insert(tweets);
            }

            return tweetsResult;
        }
    }
}
