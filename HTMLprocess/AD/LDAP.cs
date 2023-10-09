//////////////////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 11/04/2023
// Last Modified On :
// Description: Work with LDAP
// Project: HTMLprocess
//////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using HTMLprocess.Interfaces;

namespace HTMLprocess.AD
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

        protected string DomainName
        {
            get { return _domainName; }
        }

        #region METHODS

        public List<string> GetGroupsByUser(string userName)
        {
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
