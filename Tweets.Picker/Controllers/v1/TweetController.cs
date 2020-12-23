using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tweet.Picker.App.Messages;
using Tweets.Picker.App.Services;

namespace Tweets.Picker.Controllers.v1
{
    /// <summary>
    /// API to fetch Tweets
    /// </summary>
    [Route("api/v1/[Controller]"), Produces("application/json")]
    public class TweetController : Controller
    {
        private ITwitterApplicationService TwitterApplicationService { get;}


        public TweetController(ITwitterApplicationService twitterApplicationService) => TwitterApplicationService = twitterApplicationService;

        /// <summary>
        /// Gets Tweets with the given expression
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Performs the return</returns>
        [HttpGet, Produces("application/json", Type = typeof(TweetResponseMessage))]
        public async Task<IActionResult> Get(string expression) => Ok(TwitterApplicationService.GetTweetsByExpression(expression));

        /// <summary>
        /// Inserts Tweets with the given expression in database. 
        /// If the file field is filled, the file path will be used as the data source.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Performs the return</returns>
        [HttpPost, Produces("application/json", Type = typeof(TweetResponseMessage))]
        public async Task<IActionResult> Post(string expression, string filename) => Ok(TwitterApplicationService.InsertTweetsByExpression(expression, filename));

    }
}
