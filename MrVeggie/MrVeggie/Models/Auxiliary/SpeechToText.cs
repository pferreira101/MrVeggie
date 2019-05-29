using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models.Auxiliary
{
    public class SpeechToText
    {

        public string listen()
        {
            var config = SpeechConfig.FromSubscription("652842a020de4fc9990d5cfc8f82fb98", "westeurope");

            var language = "pt-PT";
            config.SpeechRecognitionLanguage = language;

            var recognizer = new SpeechRecognizer(config);

            string result = "";

            recognizer.StartContinuousRecognitionAsync();

            recognizer.Recognizing += (s, e) => {
                if (e.Result.Text.Contains("próximo"))
                {
                    result = "próximo";
                    recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                }

                if (e.Result.Text.Contains("anterior"))
                {
                    result = "anterior";
                    recognizer.StopContinuousRecognitionAsync().ConfigureAwait(false);
                }


            };

            return result;
        }
    }
}
