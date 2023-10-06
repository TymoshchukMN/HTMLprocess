using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLprocess
{
    internal class GroupMember
    {
        public GroupMember(string userName, string groupName)
        {
            UserName = userName;
            GroupName = groupName;
        }

        public string UserName { get; set; }

        public string GroupName { get; set; }
    }
}
