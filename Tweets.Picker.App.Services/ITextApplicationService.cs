        
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tweets.Picker.App.Services
{
    public interface ITextApplicationService
    {
        Task TreatFile(string filename);
    }
}
