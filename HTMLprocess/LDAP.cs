//////////////////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 11/04/2023
// Last Modified On :
// Description: Work with LDAP
// Project: TitleProcessing
//////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using HTMLprocess.Interfaces;

namespace TitleProcessing
{
    internal class LDAP : IGetGroupsByUser
    {
        #region FIELDS

        /// <summary>
        /// Current domain name.
        /// </summary>
        private string _domainName;

        #endregion FIELDS

        #region CTORs

        public LDAP()
        {
            _domainName = GetCurrentDomainPath();
        }

        #endregion CTORs

        #region METHODS

        /// <summary>
        /// Get users from active directory.
        /// </summary>
        /// <param name="currentTitles">
        /// List for filleing titles and logins.
        /// </param>
        public void GetAllUsers(List<string> currentTitles)
        {
            SearchResultCollection results;
            DirectorySearcher directorySearcher;
            using (DirectoryEntry directoryEntry = new DirectoryEntry(_domainName))
            {
                directorySearcher = new DirectorySearcher(directoryEntry);
                directorySearcher.Filter = "(&(&(&(&(&(&(&(&(objectCategory=user)" +
               "(!objectClass=contact)" +
               "(!((userAccountControl:1.2.840.113556.1.4.803:=2)))" +
               "(!sAMAccountName=**_**)(&(Title=*))))))))))";

                const int PAGE_SIZE = 1000;
                directorySearcher.PageSize = PAGE_SIZE;
                directorySearcher.PropertiesToLoad.Add("sAMAccountName");
                directorySearcher.PropertiesToLoad.Add("title");

                results = directorySearcher.FindAll();

                foreach (SearchResult sr in results)
                {
                    if (!sr.Path.Contains("OU=Trash")
                        && !sr.Path.Contains("OU=HRFutureStaff")
                        && !sr.Path.Contains("OU=HRMaternity")
                        && !sr.Path.Contains("OU=HRMobilized"))
                    {
                        currentTitles.Add(
                       string.Format(
                           $"{sr.Properties["sAMAccountName"][0]};" +
                           $"{sr.Properties["title"][0]}"));
                    }
                }
            }
        }

        public List<string> GetGroupsByUser(string userName)
        {
            // "(&(objectCategory = user)(objectClass = user)(cn = {}))"

            SearchResultCollection results;
            DirectorySearcher directorySearcher;
            List<string> groups = new List<string>();
            using (DirectoryEntry directoryEntry = new DirectoryEntry(_domainName))
            {
                directorySearcher = new DirectorySearcher(directoryEntry);
                directorySearcher.Filter = $"(&(objectCategory=user)" +
                    $"(objectClass=user)(cn={userName}))";

                directorySearcher.PropertiesToLoad.Add("memberof");

                results = directorySearcher.FindAll();

                foreach (SearchResult sr in results)
                {
                    foreach (string item in sr.Properties["memberof"])
                    {
                        string groupName =
                            item.Split(',')[0].Replace("CN=", string.Empty).Trim();
                        groups.Add(groupName);
                        System.Console.WriteLine(groupName);
                    }
                }
            }

            return groups;
        }

        /// <summary>
        /// Ger root domain.
        /// </summary>
        /// <returns>
        /// Domain name.
        /// </returns>
        private string GetCurrentDomainPath()
        {
            DirectoryEntry de = new DirectoryEntry("LDAP://RootDSE");

            return "LDAP://" + de.Properties["defaultNamingContext"][0].ToString();
        }

        #endregion METHODS
    }
}
