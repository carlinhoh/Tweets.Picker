using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweet.Picker.App.Messages;
using Tweets.Picker.App.Services;
using Tweets.Picker.Services.Model;

namespace Tweets.Picker.Controllers.v1
{
    /// <summary>
    /// API to fetch Tweets
    /// </summary>
    [Route("api/v1/[Controller]"), Produces("application/json")]
    public class TextController : Controller
    {
        private ITextApplicationService TextApplicationService { get; }


        public TextController(ITextApplicationService textApplicationService) => TextApplicationService = textApplicationService;

        /// <summary>
        /// Processes and treats the data contained in the file for the use of artificial intelligence
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Performs the return</returns>
        [HttpPost, Produces("application/json", Type = typeof(TweetResponseMessage))]
        public async Task<IActionResult> Post(string filename) => Ok(TextApplicationService.TreatFile(filename));


    }
}
