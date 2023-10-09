//////////////////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 09/10/2023
// Last Modified On :
// Description: Starter
// Project: HTMLprocess
//////////////////////////////////////////

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

            GroupProcessing.RemoveUserFromGroup(members);
        }
    }
}
