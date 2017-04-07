using System.Web;

namespace CrewWhitelistApps.Security
{
    public static class SessionPersister
    {
        static string usernameSessionvar = "Username";

        public static string username
        {
            get
            {
                if (HttpContext.Current == null)
                    return string.Empty;
                var sessionVar = HttpContext.Current.Session[usernameSessionvar];

                if (sessionVar != null)
                    return sessionVar as string;
                return null;
            }
            set
            {
                HttpContext.Current.Session[usernameSessionvar] = value;
            }
        }
    }
}