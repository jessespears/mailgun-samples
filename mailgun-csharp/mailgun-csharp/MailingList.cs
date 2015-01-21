using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace sample
{
    public class MailingList
    {
        public static String my_api_key = "no";
        public static String my_domain = "mg.jessespears.com";
        public static String my_list = "jesses@mg.jessespears.com";

        public static void Main(String [] args){
            Console.Write("Enter your api key: ");
            my_api_key = Console.ReadLine();
            IRestResponse response = RemoveMember();

            Console.WriteLine(response.Content);
            Console.ReadKey();
        }

        public static IRestResponse SendSimpleMessage()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v2");
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               my_api_key);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                 my_domain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "jesse <jesse@jessespears.com>");
            request.AddParameter("to", "jesse@mailgunhq.com");
            request.AddParameter("subject", "Hello");
            request.AddParameter("text", "Testing some Mailgun awesomness!");
            request.Method = Method.POST;
            return client.Execute(request);
        }

        public static IRestResponse CreateMailingList()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v2");
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               my_api_key);
            RestRequest request = new RestRequest();
            request.Resource = "lists";
            request.AddParameter("address", my_list);
            request.AddParameter("description", "jesses on this site");
            request.Method = Method.POST;
            return client.Execute(request);
        }

        public static IRestResponse AddListMember()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new System.Uri("https://api.mailgun.net/v2");
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               my_api_key);
            RestRequest request = new RestRequest();
            request.Resource = "lists/{list}/members";
            request.AddParameter("list", my_list, ParameterType.UrlSegment);
            request.AddParameter("address", "bar@example.com");
            request.AddParameter("subscribed", true);
            request.AddParameter("name", "Bob Bar");
            request.AddParameter("description", "Developer");
            request.AddParameter("vars", "{\"age\": 26}");
            request.Method = Method.POST;
            return client.Execute(request);
        }

        public static IRestResponse RemoveMember()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v2");
            client.Authenticator =
                    new HttpBasicAuthenticator("api",
                                               my_api_key);
            RestRequest request = new RestRequest();
            request.Resource = "lists/{list}/members/{member}";
            request.AddParameter("list", my_list, ParameterType.UrlSegment);
            request.AddParameter("member", "bar@example.com", ParameterType.UrlSegment);
            request.Method = Method.DELETE;
            return client.Execute(request);
        }
    }
}
