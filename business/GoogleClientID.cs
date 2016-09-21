namespace business
{
    using System.IO;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Reads a client_id.json from Google + API and deserializes JSON-data to a 
    /// </summary>
    public class GoogleClientID
    {
        private string _googleClientIdJsonFile = "";
        private WebJSONContent _webJSONContent = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="googleClientIdJsonFile">Entire filepath</param>
        public GoogleClientID(string googleClientIdJsonFile)
        {
            this._googleClientIdJsonFile = googleClientIdJsonFile;
        }

        public void GetDataFromFile()
        {
            using (var sr = new StreamReader(this._googleClientIdJsonFile))
            {
                var dataFromJsonFile = sr.ReadToEnd();
                dynamic googleClientIdJsonObject = JObject.Parse(dataFromJsonFile);
                _webJSONContent = new WebJSONContent()
                {
                    client_id = googleClientIdJsonObject.web.client_id,
                    project_id = googleClientIdJsonObject.web.project_id,
                    token_uri = googleClientIdJsonObject.web.token_uri,
                    auth_provider_x509_cert_url = googleClientIdJsonObject.web.auth_provider_x509_cert_url,
                    client_secret = googleClientIdJsonObject.web.client_secret
                };
            }
        }

        public class WebJSONContent
        {
            public string client_id { get; set; }

            public string project_id { get; set; }
            public string token_uri { get; set; }
            public string auth_provider_x509_cert_url { get; set; }
            public string client_secret { get; set; }
        }

        public WebJSONContent web
        {
            get { return _webJSONContent; }
        }
    }
}
