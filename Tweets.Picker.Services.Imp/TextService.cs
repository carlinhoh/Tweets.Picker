using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tweets.Picker.Services.Imp
{
    public class TextService : ITextService
    {
        public Task TreatFile(string filename)
        {
            string path = @"C:\Users\carlinho\Desktop\TCC\TreatedFiles\TreatedNonHomofobia.txt";

            string line = "";
            string treatedLine = "";

            if (!File.Exists(path))
            {
                File.CreateText(path);
            }

            using (StreamWriter sw = new StreamWriter(path))
            {
                using (var sr = new StreamReader(filename))
                {
                    while (!sr.EndOfStream)
                    {
                        line = sr.ReadLine().Trim();

                        treatedLine = TreatTweetText(line).Trim();

                        //sw.WriteLine("Antes:" + line);
                        //sw.WriteLine("Depois:" + treatedLine);
                        //sw.WriteLine("--");
                        sw.WriteLine(treatedLine);
                    }
                }

            }
            Console.WriteLine("");

            return Task.CompletedTask;
        }

        private static string TreatTweetText(string text)
        {
            string newText = "";
            //TODO: transformar em variável constante
            string[] acronimo = { "bjs", "c", "msg", "msm", "n", "obg", "p", "pra", "pq", "tbm", "tb", "num", "ap", "cel", "tel", "pf", "pfv", "pfvr", "qq", "qlqr", "vc", "ql", "qm", "vdd", "sdd", "ctz", "dsclp", "amg", "q", "td", "tds", "algm", "ngm", "ta", "tá", "to", "tô", "bixa", "bixoila", "chola", "cholinha", "emosexual", "florsinha", "gaysao", "gaysinho", "homosexual", "homosexualismo", "lesba", "lesbia", "lesbiana", "mulhersinha", "transexual", "smp", "mt", "mto", "mts", "mtos", "dps" };
            //TODO: transformar em variável constante
            string[] nomeExtenso = { "beijos", "com", "mensagem", "mesmo", "nao", "obrigado", "para", "para", "porque", "tambem", "tambem", "numero", "apartamento", "celular", "telefone", "por favor", "por favor", "por favor", "qualquer", "qualquer", "voce", "qual", "quem", "verdade", "saudade", "certeza", "desculpa", "amigo", "que", "tudo", "todos", "alguem", "ninguem", "esta", "esta", "estou", "estou", "bicha", "bichoila", "tchola", "tcholinha", "emossexual", "florzinha", "gayzao", "gayzinho", "homossexual", "homossexualismo", "lesbica", "lesbica", "lesbica", "mulherzinha", "transsexual", "sempre", "muito", "muito", "muitos", "muitos", "depois" };

            string[] palavrasTexto = RemoveAccents(text).ToLower().Replace("  ", " ").Replace('\n', ' ').Replace("_", "").Replace(".", " ").Replace(",", " ").Replace(";", " ").Replace("-", " ").Replace("?", " ").Replace(":", " ").Replace("'", " ").Trim().Split(' ');

            for (int i = 0; i < palavrasTexto.Length; i++)
            {
                for (int index = 0; index < acronimo.Length; index++)
                {
                    if (palavrasTexto[i].Equals(acronimo[index]))
                    {
                        palavrasTexto[i] = nomeExtenso[index];
                    }
                }
                newText += palavrasTexto[i] + " ";
            }

            var textWithoutEmoticons = Regex.Replace(newText, @"[^\w\ ]", " ", RegexOptions.None);

            var finalizeText = Regex.Replace(textWithoutEmoticons, @"\s+", " ").Trim();

            return finalizeText;
        }

        private static string RemoveAccents(string text)
        {
            StringBuilder sbReturn = new StringBuilder();

            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }

            return sbReturn.ToString();
        }

    }
}
