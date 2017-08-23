using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Client;
using Microsoft.SharePoint.Client.UserProfiles;

namespace TestUserProfile
{
    class Program
    {
        static void Main(string[] args)
        {
            const string serverUrl = "https://lath.sharepoint.com/sites/beehive";
            const string targetUser = "IslamS@LATHROPGAGE.COM";

            SecureString password = ToSecureString("PleaseChangeThisPassword2017!");

            using (var clientContext = new ClientContext(serverUrl))
            {
                clientContext.Credentials = new SharePointOnlineCredentials(targetUser, password);

                // Get the people manager instance and load current properties
                PeopleManager peopleManager = new PeopleManager(clientContext);
                PersonProperties personProperties = peopleManager.GetMyProperties();
                clientContext.Load(personProperties);
                clientContext.ExecuteQuery();
                
                foreach (var item in personProperties.UserProfileProperties)
                {
                   Console.WriteLine("Key: {0} Value: {1}", item.Key, item.Value);
                }

                Console.ReadKey();
            }
        }

        public static SecureString ToSecureString(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return null;
            else
            {
                SecureString result = new SecureString();
                foreach (char c in source.ToCharArray())
                    result.AppendChar(c);
                return result;
            }
        }
        private static SecureString GetPassword(string password)
        {
            ConsoleKeyInfo info;
            //Get the user's password as a SecureString  
            SecureString securePassword = new SecureString();
            do
            {
                info = Console.ReadKey(true);
                if (info.Key != ConsoleKey.Enter)
                {
                    securePassword.AppendChar(info.KeyChar);
                }
            }
            while (info.Key != ConsoleKey.Enter);
            return securePassword;
        }
    }
}
