using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tweets.Picker.Services;

namespace Tweets.Picker.App.Services.Imp
{
    public class TextApplicationService : ITextApplicationService
    {
        private ITextService TextService { get; }
        
        public TextApplicationService(ITextService textService)
        {
            TextService = textService;
        }

        public Task TreatFile(string filename)
        {
            return TextService.TreatFile(filename);
        }

    }
}
