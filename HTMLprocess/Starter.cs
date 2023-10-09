using System;
using System.Collections.Generic;
using HTMLprocess.AD;

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

            for (int i = 0; i < members.Count; i++)
            {
                GroupProcessing.RemoveUserFromGroup(members[i].UserName, members[i].GroupName);
            }
        }
    }
}
