using Tweetinvi;
using System.Configuration;
using Tweetinvi.Models;
using System.Threading.Tasks;
using System;
using Tweetinvi.Parameters;
using Tweetinvi.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Tweets.Picker.Infra.Service.Provider.TwitterGateway.Imp
{
    public class TwitterServiceProvider : ITwitterServiceProvider
    {
        readonly IConfiguration Configuration;
        public TwitterServiceProvider(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public async Task<List<Services.Model.Tweet>> GetTweetsByExpression(string expression)
        {
            AuthenticateTwitter();

            var searchParameter = new SearchTweetsParameters(expression)
            {
                MaximumNumberOfResults = 300,
                TweetSearchType = TweetSearchType.OriginalTweetsOnly
            };

            var tweets = Search.SearchTweets(searchParameter);

            List<Services.Model.Tweet> tweetList = MapperTweet(expression, tweets);

            return tweetList;
        }


        private List<Services.Model.Tweet> MapperTweet(string expression, IEnumerable<ITweet> tweets)
        {
            List<Services.Model.Tweet> tweetsList = new List<Services.Model.Tweet>();



            foreach (var tweet in tweets)
            {
                Services.Model.Tweet tweetModel = new Services.Model.Tweet();

                tweetModel.Text = tweet.FullText;
                tweetModel.Url = tweet.Url;
                tweetModel.KeyWord = expression;
                tweetModel.Location = tweet.CreatedBy.Location;
                tweetModel.TwitterUserName = tweet.CreatedBy.ScreenName;
                tweetModel.UserName = tweet.CreatedBy.Name;
                tweetModel.TweetCreateAt = tweet.CreatedAt;

                tweetsList.Add(tweetModel);
            }
            return tweetsList;
        }

        public Task<string> ReadStremByWord(string wordToSearch)
        {
            AuthenticateTwitter();
            //try
            //{
            //    var authenticatedUser = User.GetAuthenticatedUser(null, new GetAuthenticatedUserParameters());
            //}


            var stream = Stream.CreateFilteredStream();

            // Keywords to Track
            stream.AddTrack(wordToSearch);

            // Limit to English 
            stream.AddTweetLanguageFilter(LanguageFilter.English);

            // Message so you know its running
            Console.WriteLine("I am listening to Twitter");

            // Called when a tweet maching the tracker is produced
            stream.MatchingTweetReceived += (sender, arguments) =>
            {
                Console.WriteLine(arguments.Tweet.CreatedAt);
                Console.WriteLine(arguments.Tweet.CreatedBy);
                Console.WriteLine(arguments.Tweet.FullText + "\n");
                Console.WriteLine("------------------------------");

            };

            stream.StartStreamMatchingAllConditions();

            return null;
        }

        private string GetConfigValue(string key) => Configuration.GetSection(key).Value;

        private void AuthenticateTwitter()
        {
            //var ApplicationKey = GetConfigValue("TwitterCredentials");
            //var ApplicationKey = Configuration.GetSection("TwitterCredentials:ApplicationKey").Value;
            //var ApplicationSecretKey = Configuration.GetSection("TwitterCredentials:ApplicationSecretKey").Value;
            //var AcessToken = Configuration.GetSection("TwitterCredentials:AcessToken").Value;
            //var AcessTokenSecret = Configuration.GetSection("TwitterCredentials:AcessTokenSecret").Value;

            ExceptionHandler.SwallowWebExceptions = false;

            //Auth.SetUserCredentials(ApplicationKey, ApplicationSecretKey, AcessToken, AcessTokenSecret);
            Auth.SetUserCredentials("1dYbwkBUcT2ZL5SnRkaQ0zaZj", "cwUxpmhYJPNoX92Q4B8BVSArkuecDoRWky9peSe6XAJetcE5F3", "1236396002342510592-san2uKncs4yL6LZzLJmNVOm5fxaaNJ", "BQYxKYQMUq4P6QtjxjI7DlYMvKRYAPNg7raeLEac3XYoE");


            try
            {
                var authenticatedUser = User.GetAuthenticatedUser(null, new GetAuthenticatedUserParameters());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Request parameters are invalid: '{0}'", ex.Message);
            }
            catch (TwitterException ex)
            {
                Console.WriteLine("An error occurred while calling twitter: ", ex.TwitterDescription, ExceptionHandler.GetLastException().TwitterDescription);
            }

        }
    }
}
