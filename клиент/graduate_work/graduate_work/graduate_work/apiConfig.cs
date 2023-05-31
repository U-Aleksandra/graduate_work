using System.Net.Http;

namespace graduate_work.Droid
{
    public static class apiConfig
    {
        public static HttpClientHandler clientHandler = new HttpClientHandler();
        public static HttpClient client;
        public static string url = "172.20.10.2";
        static apiConfig()
        {
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            client = new HttpClient(clientHandler);
        }
    }
}