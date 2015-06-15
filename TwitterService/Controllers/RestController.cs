using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using TwitterService.OAuth;
using TwitterService.Infrastructure;

namespace TwitterService.Controllers {

    [RoutePrefix("api/rest")]
    public sealed class RestController : ApiController {

        // POST api/rest/nowplaying
        [HttpPost, ActionName("nowplaying")]
        public async Task<object> GetNowPlaying([FromBody] JObject coordinates) {
            string lat = "", lon = "", rad = "", geocode = "";
            string entities = "&include_entities=false";
            string count = "&count=5";

            // Validates the received argument
            if (coordinates != null) {
                lat = Utilities.CheckNumber(coordinates["latitude"]);
                lon = Utilities.CheckNumber(coordinates["longitude"]);
                rad = Utilities.CheckString(coordinates["radius"]);
                if (lat != "0" && lon != "0" && rad != "")
                    geocode = "&geocode=" + string.Join(",", new[] { lat, lon, rad });
            }

            //TODO: if geocode is specified, then get no success status
            geocode = "";

            // https://dev.twitter.com/rest/reference/get/search/tweets
            string url = "https://api.twitter.com/1.1/search/tweets.json?q=%23nowplaying%20youtube&src=typd" + geocode + entities + count;

            // Create client and insert an OAuth message handler in the message path that 
            // inserts an OAuth authentication header in the request
            HttpClient client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler()));

            // Send asynchronous request to twitter and read the response as JToken
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode) {
                return await response.Content.ReadAsAsync<JToken>();
            }
            // No success status
            return new Infrastructure.Error(response.ReasonPhrase);
        }

        // POST api/rest/sendtweet
        [HttpPost, ActionName("sendtweet")]
        public async Task<object> SendTweet([FromBody] JObject args) {
            string video = "", comments = "";
            // Validates the received argument
            if (args != null) {
                video = Utilities.CheckString(args["video"]);
                comments = Utilities.CheckString(args["comments"]);
            }
            string url = "";

            // Create client and insert an OAuth message handler in the message path that 
            // inserts an OAuth authentication header in the request
            HttpClient client = new HttpClient(new OAuthMessageHandler(new HttpClientHandler()));

            //TODO: search the correct url in the API
            return new Infrastructure.Error("Not implemented yet");
        }

    }//end class
}//end namespace