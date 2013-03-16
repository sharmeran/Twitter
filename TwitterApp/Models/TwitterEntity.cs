using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TwitterApp.Models
{
    public class TwitterEntity
    {
       
        string screenName;
        string tweet;
        string userID;

        public string UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public string Tweet
        {
            get { return tweet; }
            set { tweet = value; }
        }

        public string ScreenName
        {
            get { return screenName; }
            set { screenName = value; }
        }

        
    }
}