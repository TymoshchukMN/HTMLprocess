using System.Collections.Generic;
using TitleProcessing;

namespace HTMLprocess
{
    internal class Starter
    {
        public static void Run(string[] htmlDoc)
        {
            HendlerHTML hendlerHTML = new HendlerHTML();

            // get useres from HTML 
            List<string> users = hendlerHTML.ProccessHTML(htmlDoc);
            LDAP ldap = new LDAP();

            foreach (string user in users )
            {
                ldap.GetGroupsByUser(user);
            }
        }
    }
}
