using System;
using System.Web;
using Funq;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.WebHost.Endpoints;
using BWA.bigWebDesk.Api.Services;
using ServiceStack.Common.Web;
using System.Net;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.ServiceInterface.Cors;
using ServiceStack.WebHost.Endpoints.Extensions;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace BWA.bigWebDesk.Api
{
    public partial class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            PreSendRequestHeaders += new EventHandler(OnPreSendRequestHeaders);
            new AppHost().Init();
        }

        protected void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("Server");
            HttpContext.Current.Response.Headers.Remove("ETag");
            HttpContext.Current.Response.Headers.Remove("Last-Modified");
            HttpContext.Current.Response.Cookies.Remove("WAWebSiteSID");
            HttpContext.Current.Response.Cookies.Remove("ARRAffinity");
        }

        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("Server");
            HttpContext.Current.Response.Headers.Remove("ETag");
            HttpContext.Current.Response.Headers.Remove("Last-Modified");
            HttpContext.Current.Response.Cookies.Remove("WAWebSiteSID");
            HttpContext.Current.Response.Cookies.Remove("ARRAffinity");
        }
    }

    public class AppHost : AppHostBase
    {
        public AppHost() : base("SherpaDesk API service.", typeof(LoginService).Assembly) { }
    
        public override void Configure(Container container)
        {
            //Permit modern browsers (e.g. Firefox) to allow sending of any REST HTTP Method
            base.SetConfig(new EndpointHostConfig
            {
                ServiceStackHandlerFactoryPath = "api",
                DefaultRedirectPath = "/login",
                EnableFeatures = (Feature.Csv | Feature.Jsv | Feature.Json | Feature.Html | Feature.Metadata),
                GlobalResponseHeaders = new Dictionary<string, string>()
            });
            
            this.RequestFilters.Add((httpReq, httpRes, requestDto) =>
            {
                //Handles Request and closes Responses after emitting global HTTP Headers
                string origin = httpReq.Headers["Origin"];
                if (string.IsNullOrEmpty(origin) || !origin.StartsWith("http"))
                    origin = "*";
                httpRes.AddHeader("Access-Control-Allow-Origin", origin);
                //httpRes.AddHeader("Access-Control-Allow-Credentials", "true");
                if (httpReq.HttpMethod.ToLower() == "options")
                {
                    httpRes.AddHeader("Access-Control-Allow-Headers", "Content-Type, Authorization, Accept, Origin");
                    httpRes.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
                    httpRes.EndServiceStackRequest();
                }
            });

            //Add a response filter to add a 'Content-Disposition' header so browsers treat it as a native .csv file
            this.ResponseFilters.Add((req, res, dto) =>
            {
                res.DeleteCookie("ARRAffinity");
                //res.Close();
            });
            
            ServiceStack.Text.JsConfig.DateHandler = ServiceStack.Text.JsonDateHandler.ISO8601;
            //ServiceStack.Text.JsConfig.EmitLowercaseUnderscoreNames = true;
            ServiceStack.Text.JsConfig.IncludeNullValues = true;
            
            //Routes
            //      .Add<BWA.bigWebDesk.Api.Models.Ticket>("/tickets", "POST");
        }
    }
}