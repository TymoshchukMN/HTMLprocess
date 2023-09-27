using System.Collections.Generic;

namespace HTMLprocess.Interfaces
{
    internal interface IGetGroupsByUser
    {
        List<string> GetGroupsByUser(string userName);
    }
}
