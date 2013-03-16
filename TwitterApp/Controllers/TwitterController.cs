using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LinqToTwitter;
using TwitterApp.Constants;
using TwitterApp.Models;

namespace TwitterApp.Controllers
{
    public class TwitterController : Controller
    {
        

        public ActionResult Index()
        {
           
            IOAuthCredentials credentials = new SessionStateCredentials();
            MvcAuthorizer auth;
            TwitterContext twitterCtx;
            if (credentials.ConsumerKey == null || credentials.ConsumerSecret == null)
            {
                credentials.ConsumerKey = ConfigurationManager.AppSettings[CommonConstants.ConsumerKey];
                credentials.ConsumerSecret = ConfigurationManager.AppSettings[CommonConstants.ConsumerSecret];
                credentials.AccessToken = ConfigurationManager.AppSettings[CommonConstants.AccessToken];
                credentials.OAuthToken = ConfigurationManager.AppSettings[CommonConstants.OAuthToken];
                credentials.ScreenName = ConfigurationManager.AppSettings[CommonConstants.ScreenName];
            }

            auth = new MvcAuthorizer
            {
                Credentials = credentials
            };
            auth.CompleteAuthorization(Request.Url);
            if (!auth.IsAuthorized)
            {
                Uri specialUri = new Uri(Request.Url.ToString());
                return auth.BeginAuthorization(specialUri);
            }
             twitterCtx = new TwitterContext(auth);
            
            var friendTweets =
                (from tweet in twitterCtx.Status
                 where tweet.Type == StatusType.Home
                 select new TwitterEntity
                 {
                     
                     ScreenName = tweet.User.Name,                     
                     Tweet = tweet.Text
                    
                 })
                .ToList();

           
            return View(friendTweets);
        }


       
    }
}
