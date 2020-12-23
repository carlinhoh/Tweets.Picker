using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tweets.Picker.Services
{
    public interface ITextService
    {
        Task TreatFile(string filename);
    }
}
