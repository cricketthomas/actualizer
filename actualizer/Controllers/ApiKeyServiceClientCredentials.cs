namespace actualizer.Controllers
{
    internal class ApiKeyServiceClientCredentials
    {
        private string subscriptionKey;

        public ApiKeyServiceClientCredentials(string subscriptionKey)
        {
            this.subscriptionKey = subscriptionKey;
        }
    }
}