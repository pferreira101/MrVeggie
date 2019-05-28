using Microsoft.CognitiveServices.Speech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MrVeggie.Models.Auxiliary
{
    public class TextToSpeech
    {
        private Passo p;

        public TextToSpeech(Passo p1)
        {
            p = p1;
        }

        public void readStep()
        {
            var config = SpeechConfig.FromSubscription("652842a020de4fc9990d5cfc8f82fb98", "westeurope");

            var language = "pt-PT";
            config.SpeechSynthesisLanguage = language;
            var voice = "Microsoft Server Speech Text to Speech Voice (pt-PT, HeliaRUS)";
            config.SpeechSynthesisVoiceName = voice;


            var synthesizer = new SpeechSynthesizer(config);

            string text = "";

            if (p.ingredientes.Count == 0)
            {
                if (p.tempo == 0)
                {
                    text = p.operacao.desc;
                }
                else
                {
                    text = p.operacao.desc + " durante " + p.tempo + " minutos.";
                }

                var result = synthesizer.SpeakTextAsync(text);
            }
            else
            {
                foreach (var ing in p.ingredientes)
                {
                    if (p.tempo == 0)
                    {

                        text = p.operacao.desc + " " + ing.Value.quantidade + " " + ing.Value.unidade + " de " + ing.Key.nome;
                    }
                    else
                    {
                        text = p.operacao.desc + " " + ing.Value.quantidade + " " + ing.Value.unidade + " de " + ing.Key.nome + " durante " + p.tempo + " minutos.";
                    }

                    var result = synthesizer.SpeakTextAsync(text);

                }
            }

        }

    }
}
