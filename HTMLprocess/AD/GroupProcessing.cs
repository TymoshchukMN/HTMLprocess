//////////////////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 09/10/2023
// Last Modified On :
// Description: Work with AD groups
// Project: HTMLprocess
//////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace HTMLprocess.AD
{
    internal class GroupProcessing
    {
        public static void RemoveUserFromGroup(string userId, string groupName)
        {
            using (PrincipalContext principalContext = new PrincipalContext(ContextType.Domain))
            {
                using (var user = UserPrincipal.FindByIdentity(principalContext, userId))
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(principalContext, groupName);

                    var entry = group.GetUnderlyingObject() as DirectoryEntry;
                    var userEntry = user.GetUnderlyingObject() as DirectoryEntry;
                    Console.Write($"{entry.Name}\t{user.Name}");
                    entry.Invoke("Remove", new object[] { userEntry.Path });
                }
            }
        }

        public static void RemoveUserFromGroup(List<GroupMember> groupMembers)
        {
            using (PrincipalContext principalContext
                = new PrincipalContext(ContextType.Domain))
            {
                for (int i = 0; i < groupMembers.Count; i++)
                {
                    string userId = groupMembers[i].UserName;

                    using (var user
                        = UserPrincipal.FindByIdentity(
                            principalContext,
                            userId))
                    {
                        string groupName = groupMembers[i].GroupName;
                        GroupPrincipal group
                            = GroupPrincipal.FindByIdentity(
                                principalContext,
                                groupName);

                        var entry = group.GetUnderlyingObject() as DirectoryEntry;
                        var userEntry = user.GetUnderlyingObject() as DirectoryEntry;
                        Console.WriteLine($"{entry.Name}\t{user.Name}");
                        entry.Invoke("Remove", new object[] { userEntry.Path });
                    }
                }
            }
        }
    }
}
