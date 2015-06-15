using System;
using System.Linq;
using System.Web.Http;
using TwitterService.App_Start;

namespace TwitterService {

    /// <summary>
    /// Configures the application
    /// </summary>
    /// <!-- Created by: David Rivera -->
    public class WebApiApplication : System.Web.HttpApplication {

        protected void Application_Start() {

            //WebAPI 2: Enables Attribute Routing
            GlobalConfiguration.Configure(WebApiConfig.Register);

            //Remove the XML formatter
            ConfigureApi(GlobalConfiguration.Configuration);
        }

        protected void Application_Error() {
            //Logs the exception in case the ExceptionHandler does not catch it
            //for instance, exceptions generated outside of the controller
            var err = Server.GetLastError();

            Infrastructure.Logger.GetInstance.LogException(err);
            Server.ClearError();
        }

        protected void Application_BeginRequest(object sender, EventArgs e) {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }

        private void ConfigureApi(HttpConfiguration config) {
            //It can also be done in App_Start/WebApiConfig
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }

    }//end class
}//end namespace