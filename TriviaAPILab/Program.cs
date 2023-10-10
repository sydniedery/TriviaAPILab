using System;
using System.Globalization;
using System.IO.Pipes;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace TriviaAPILab
{
    [DataContract]
    public class Questions
    {
        [DataMember(Name = "category")]
        public string Category { get; set; }

        [DataMember(Name = "question")]
        public string Question { get; set; }

        [DataMember(Name = "answer")]
        public string Answer { get; set; }

    }

    public class Program
    {
        public static int getResponse()
        {
            Console.WriteLine("What catagory do you want a trivia question from?");
            Console.Write("\n1: Art Literature");
            Console.Write("\n2: Language");
            Console.Write("\n3: Science Nature");
            Console.Write("\n4: General");
            Console.Write("\n5: Food/Drink");
            Console.Write("\n6: People/Places");
            Console.Write("\n7: Geography");
            Console.Write("\n8: History/Holidays");
            Console.Write("\n9: Entertainment");
            Console.Write("\n10: Toys/Games");
            Console.Write("\n11: Music");
            Console.Write("\n12: Mathematics");
            Console.Write("\n13: Religion/Mythology");
            Console.Write("\n14: Sports/Leisure");
            Console.Write("\n15: Random");
            Console.WriteLine("\n0: QUIT PROGRAM!");
            int res = Int32.Parse(Console.ReadLine());
            return res;
        }

        public static string chooseCategory(int res)
        {
            string category = $"?category=";
            switch (res)
            {
                case 1:
                    category += "artliterature";
                    break;
                case 2:
                    category += "language";
                    break;
                case 3:
                    category += "sciencenature";
                    break;
                case 4:
                    category += "general";
                    break;
                case 5:
                    category += "fooddrink";
                    break;
                case 6:
                    category += "peopleplaces";
                    break;
                case 7:
                    category += "geography";
                    break;
                case 8:
                    category += "historyholidays";
                    break;
                case 9:
                    category += "entertainment";
                    break;
                case 10:
                    category += "toysgames";
                    break;
                case 11:
                    category += "music";
                    break;
                case 12:
                    category += "mathematics";
                    break;
                case 13:
                    category += "religionmythology";
                    break;
                case 14:
                    category += "sportsleisure";
                    break;
                case 15:
                    category = $"";
                    break;


            }
            return category;

        }
        static void Main(string[] args)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri($"https://api.api-ninjas.com/v1/trivia");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("X-Api-Key", "C5QBbG1Q8DMy5Le8MWxg7w==x7ajVnliTmeBp4FY");
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
            // Get data response

            Boolean keepGoing = true;

            while (keepGoing == true)
            {
                int res = getResponse();
                if (res == 0)
                {
                    keepGoing = false;
                    Console.WriteLine("Okay bye then");
                }
                else
                {
                    string category = chooseCategory(res);
                    var response = client.GetAsync(category).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response body
                        var dataObjects = response.Content
                                       .ReadAsAsync<IEnumerable<Questions>>().Result;
                        foreach (var d in dataObjects)
                        {
                            Console.WriteLine("\nCategory: {0}", d.Category);
                            Console.WriteLine("\nQuestion: {0}", d.Question);
                            string answer = Console.ReadLine();
                            if (answer.ToLower() == d.Answer.ToLower())
                            {
                                Console.WriteLine("\nYou are correct!");
                            }
                            else
                            {
                                Console.WriteLine("\nYou are incorrect:(\nThe correct answer is {0}", d.Answer);
                            }
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("{0} ({1})", (int)response.StatusCode,
                                      response.ReasonPhrase);
                    }

                }


            }

        }
    }

    /*
       [DataContract]
        public class Results
        {
            [DataMember(Name = "category")] 
            public string Category { get; set; }

            [DataMember(Name = "question")]
            public string Question { get; set; }

            [DataMember(Name = "answer")]
            public string Answer { get; set; }

        }




        public static class APICall
        {
            private static HttpClient GetHttpClient(string url)
            {
                var client = new HttpClient { BaseAddress = new Uri(url) };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("X-Api-Key", "C5QBbG1Q8DMy5Le8MWxg7w==x7ajVnliTmeBp4FY");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return client;
            }

            private static async Task<T> GetAsync<T>(string url, string urlParameters)
            {
                try
                {
                    using (var client = GetHttpClient(url))
                    {
                        HttpResponseMessage response = await client.GetAsync(urlParameters);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            var json = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<Results>(json);
                            return result;
                        }

                        return default(T);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return default(T);
                }
            }

            public static async Task<T> RunAsync<T>(string url, string urlParameters)
            {
                return await GetAsync<T>(url, urlParameters);
            }
        }
        public class Program
        {
            const string url = $"https://api.api-ninjas.com/v1/trivia";
            static void Main(string[] args)
            {
                ShowRestaurants();
            }

            private static void ShowRestaurants()
            {
                var response = GetQuestion();

                Console.WriteLine(response.Question);


                Console.ReadLine();
            }

            public static Results GetQuestion()
            { 


                string urlParameters = "?category=music";
                var response = APICall.RunAsync <Results>(url, urlParameters).GetAwaiter().GetResult();

                return response;
            }
        }
    */
}