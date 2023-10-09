//////////////////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 09/10/2023
// Last Modified On :
// Description: Work with LDAP
// Project: TitleProcessing
//////////////////////////////////////////
///
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
                    entry.Invoke("Remove", new object[] { userEntry.Path });
                }
            }
        }
    }
}
