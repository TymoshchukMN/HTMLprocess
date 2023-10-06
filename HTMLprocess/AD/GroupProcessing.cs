using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;

namespace HTMLprocess.AD
{
    internal class GroupProcessing
    {
        public void RemoveUserFromGroup(string userId, string groupName)
        {
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "COMPANY"))
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(pc, groupName);
                    group.Members.Remove(pc, IdentityType.UserPrincipalName, userId);
                    group.Save();
                }
            }
            catch (System.DirectoryServices.DirectoryServicesCOMException E)
            {
                //doSomething with E.Message.ToString(); 

            }
        }
    }
}
