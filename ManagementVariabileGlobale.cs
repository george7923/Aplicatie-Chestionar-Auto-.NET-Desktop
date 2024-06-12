using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chestionar_Auto
{
    public static class ManagementVariabileGlobale
    {
        public static string GetUserName()
        {
            return User.username;
        }

        public static void SetUserName(string newUsername)
        {
            User.username = newUsername;
        }
        public static string GetMod()
        {
            return ModuldeJoc.mod;
        }
        public static void SetMod(string mod)
        {
            ModuldeJoc.mod = mod;
        }
    }
}
