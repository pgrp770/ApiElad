using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Newtonsoft.Json;

namespace ApiElad
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var baseUrl = "https://localhost:7278/Missions";
            User user = new User() { firstName="dfdf", lastName="df", password="dfdf", email="df"};
            var a = new
            {

                StartTime = DateTime.Now,

                AgentId = 2,

                TargetId = 1,


            };
          /*  await GetAll(baseUrl);
            await GetById(baseUrl, 10);*/
            Console.WriteLine(await PostExample(baseUrl, user));
            Console.Write(await PutExample(baseUrl,10));
            Console.Write(await DeleteExample(baseUrl,10));

            Task t1 = Task.Run(async () =>
            {
                while (true)
                {
                    string getResponse =  await GetExample(baseUrl);

                    List<User> users = JsonConvert.DeserializeObject<List<User>>(getResponse);
                    if (users.Count > 10)
                    {
                        Console.WriteLine("you have mor then 10 users");
                    }
                }
            });
           
            
        }

        
        static async Task<List<User>> GetAll(string baseUrl)
        {
            // GET example
            string getResponse = await GetExample(baseUrl);
            Console.WriteLine(getResponse);
            List<User> users = JsonConvert.DeserializeObject<List<User>>(getResponse);
            return users;
        }
        static async Task<User> GetById(string baseUrl, int id)
        {
            // GET example
            string getResponse = await GetExample($"{baseUrl}/{id}");
            Console.WriteLine(getResponse);
            User user = JsonConvert.DeserializeObject<User>(getResponse);
            return user;
        }
        static async Task<string> GetExample(string url)
        {
            using var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        static async Task<string> PostExample(string url, User user)
        {
            using var httpClient = new HttpClient();


            var jsonContent = JsonConvert.SerializeObject(user);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        static async Task<string> PutExample(string url, int id)
        {
            using var httpClient = new HttpClient();

            User user = new User() { id=17 ,firstName = "dfdf", lastName = "df", password = "dfdf", email = "df" };
            var jsonContent = JsonConvert.SerializeObject(user);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync($"{url}/{id}", content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        static async Task<string> DeleteExample(string url, int id)
        {
            using var httpClient = new HttpClient();
;

            HttpResponseMessage response = await httpClient.DeleteAsync($"{url}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        
    }
}
