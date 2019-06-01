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

        public string[] getText()
        {

            string[] txts = new string[p.ingredientes.Count + 1];

            string text = "";

            int i = 0;

            if (p.ingredientes.Count == 0)
            {
                if (p.tempo == 0)
                {
                    text = p.operacao.desc;
                    txts[i++] = text;
                }
                else
                {
                    text = p.operacao.desc + " durante " + p.tempo + " minutos.";
                    txts[i++] = text;
                }
            }
            else
            {
                foreach (var ing in p.ingredientes)
                {
                    if (p.tempo == 0)
                    {

                        text = p.operacao.desc + " " + ing.Value.quantidade + " " + ing.Value.unidade + " de " + ing.Key.nome;
                        txts[i++] = text;
                    }
                    else
                    {
                        text = p.operacao.desc + " " + ing.Value.quantidade + " " + ing.Value.unidade + " de " + ing.Key.nome + " durante " + p.tempo + " minutos.";
                        txts[i++] = text;
                    }
                }
            }

            return txts;

        }

    }
}
