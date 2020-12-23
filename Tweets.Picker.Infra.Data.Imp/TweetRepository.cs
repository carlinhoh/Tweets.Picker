using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Tweets.Picker.Services.Model;

namespace Tweets.Picker.Infra.Data.Imp
{
    public class TweetRepository : ITweetRepository
    {
        public TweetRepository()
        {

        }

        public void Insert(IEnumerable<Tweet> tweets)
        {

            var credential = new BasicAWSCredentials("", "");

            AmazonDynamoDBClient client = new AmazonDynamoDBClient(credential, RegionEndpoint.USEast2);

            foreach (var tweet in tweets)
            {
                PutItemRequest tweetRequest = CreateTweetInsertRequest(tweet);

                try
                {
                    var response = client.PutItemAsync(tweetRequest).Result;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private static PutItemRequest CreateTweetInsertRequest(Tweet tweet)
        {
            Tweet tweetInsert = new Tweet();
            tweetInsert.Country = string.IsNullOrEmpty(tweet.Country) ? "Brasil" : tweet.Country;
            tweetInsert.Location = string.IsNullOrEmpty(tweet.Location) ? "LocationEmpty" : tweet.Location;
            tweetInsert.Text = /*string.IsNullOrEmpty(tweet.Text) ? "TextEmpty" :*/ tweet.Text;
            tweetInsert.TweetCreateAt = tweet.TweetCreateAt;
            tweetInsert.TwitterUserName = /*string.IsNullOrEmpty(tweet.TwitterUserName) ? "Brasil" :*/ tweet.TwitterUserName;
            tweetInsert.Url = /*string.IsNullOrEmpty(tweet.Url) ? "Brasil" :*/ tweet.Url;
            tweetInsert.UserName = /*string.IsNullOrEmpty(tweet.UserName) ? "Brasil" : */ tweet.UserName;
            tweetInsert.KeyWord = tweet.KeyWord;

            return new PutItemRequest
            {
                TableName = "TweetsOutros",
                Item = new Dictionary<string, AttributeValue>
                      {
                        { "TweetId", new AttributeValue { S = GetTweetId(tweetInsert) }},
                        { "Keyword", new AttributeValue { S = tweetInsert.KeyWord}},
                        { "TwitterUserName", new AttributeValue { S = tweetInsert.TwitterUserName}},
                        { "TreatedText", new AttributeValue { S = TreatTweetText(tweetInsert.Text)}},
                        { "Text", new AttributeValue { S = tweetInsert.Text }},
                        { "Url", new AttributeValue { S = tweetInsert.Url }},
                        { "Location", new AttributeValue { S = tweetInsert.Location }},
                        { "UserName", new AttributeValue { S = tweetInsert.UserName }},
                        { "Country", new AttributeValue { S = tweetInsert.Country }},
                        { "TweetCreateAt", new AttributeValue { S = tweetInsert.TweetCreateAt.ToString() }},
                      }
            };

        }

        private static string TreatTweetText(string text)
        {
            string newText = "";

            string[] acronimo = { "bjs", "c", "msg", "msm", "n", "obg", "p", "pra", "pq", "tbm", "tb", "num", "ap", "cel", "tel", "pf", "pfv", "pfvr", "qq", "qlqr", "vc", "ql", "qm", "vdd", "sdd", "ctz", "dsclp", "amg", "q", "td", "tds", "algm", "ngm", "ta", "tá", "to", "tô", "bixa", "bixoila", "chola", "cholinha", "emosexual", "florsinha", "gaysao", "gaysinho", "homosexual", "homosexualismo", "lesba", "lesbia", "lesbiana", "mulhersinha", "transexual", "smp", "mt", "mto", "mts", "mtos", "dps" };

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

        private static object TreatSentence(string expression)
        {
            return expression.Replace(".", " ").Replace(",", " ").Replace(";", " ").Replace("-", " ").Replace("?", " ").Replace(":", " ").Replace("'", " ");
        }

        private static string GetTweetId(Tweet tweetInsert)
        {
            return tweetInsert.TwitterUserName + "|" + tweetInsert.KeyWord + "|" + Guid.NewGuid().ToString("N");
        }
    }
}
