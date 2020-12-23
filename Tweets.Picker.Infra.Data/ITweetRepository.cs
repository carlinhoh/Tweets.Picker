using System;
using System.Collections.Generic;

namespace Tweets.Picker.Infra.Data
{
    public interface ITweetRepository
    {
        void Insert(IEnumerable<Services.Model.Tweet> tweets);
    }
}
