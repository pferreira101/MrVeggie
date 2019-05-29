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

            recognizer.Recognized += (s, e) => {
                if (e.Result.Text.Contains("próximo"))
                {
                    result = "próximo";
                    recognizer.StopContinuousRecognitionAsync();
                }

                if (e.Result.Text.Contains("anterior"))
                {
                    result = "anterior";
                    recognizer.StopContinuousRecognitionAsync();
                }

                if (e.Result.Text.Contains("finalizar"))
                {
                    result = "finalizar";
                    recognizer.StopContinuousRecognitionAsync();
                }

                
                Console.WriteLine("*************** OUVI: " + e.Result.Text);

            };

            recognizer.StartContinuousRecognitionAsync();

            Console.WriteLine("*************** MANDEI: " + result);  // nao esta a mandar o resultado
            return result;
        }
    }
}
