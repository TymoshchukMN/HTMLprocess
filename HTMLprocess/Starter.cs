using System;
using System.Collections.Generic;
using TitleProcessing;

namespace HTMLprocess
{
    internal class Starter
    {
        public static void Run()
        {
            HendlerHTML hendlerHTML = new HendlerHTML();

            List<string> users = hendlerHTML.ProccessHTML();

            LDAP ldap = new LDAP();

            List<GroupMember> members = new List<GroupMember>();
            foreach (string user in users)
            {
                var groups = ldap.GetGroupsByUser(user);

                foreach (string group in groups)
                {
                    members.Add(new GroupMember(user, group));
                }
            }

           
        }
    }
}
