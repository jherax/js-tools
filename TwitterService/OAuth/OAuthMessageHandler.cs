using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace TwitterService.OAuth {

    /// <summary>
    /// Basic DelegatingHandler that creates an OAuth authorization header based on the OAuthBase
    /// class downloaded from http://oauth.net
    /// </summary>
    /// <remarks>
    /// Every time a request is submitted it hits the SendAsync method where we get the information from the request
    /// and update it with an appropriate OAuth authentication header before sending it on its way to the next handler.
    /// </remarks>
    public class OAuthMessageHandler : DelegatingHandler {
        
        // Obtain these values by creating a Twitter app at http://dev.twitter.com/
        // account: @BInowplaying
        // user: dev@bunnyinc.com
        // pass: dev22763404
        // consumer key (API Key): gEFEbGdkVTDfzVgyiiCbzUImi
        private static string _consumerKey = "gEFEbGdkVTDfzVgyiiCbzUImi";
        private static string _consumerSecret = "Q4yQ000AiJL110zoNEkBYL0ASl84SUcnaxkCJ01uZzeghqWeXX";
        private static string _token = "2834545563-3Gp2kIjsN5fFSiph2560NAJanr3WZ6S8pMjzPVU";
        private static string _tokenSecret = "iESkAmBbHXWaCHLrkXN89CUyigMMZ5QHboNYsczUDQLlg";

        private OAuthBase _oAuthBase = new OAuthBase();

        public OAuthMessageHandler(HttpMessageHandler innerHandler)
            : base(innerHandler) {
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            // Compute OAuth header 
            string normalizedUri;
            string normalizedParameters;
            string authHeader;

            string signature = _oAuthBase.GenerateSignature(
                request.RequestUri,
                _consumerKey,
                _consumerSecret,
                _token,
                _tokenSecret,
                request.Method.Method,
                _oAuthBase.GenerateTimeStamp(),
                _oAuthBase.GenerateNonce(),
                out normalizedUri,
                out normalizedParameters,
                out authHeader);

            request.Headers.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);
            return base.SendAsync(request, cancellationToken);
        }

    }//end class
}//end namespace