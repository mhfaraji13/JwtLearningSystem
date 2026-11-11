using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtLearningSystem.Shared.Constants
{
    // create a constant field || define a field that value is specified in compile time  and it does not change when program is running.
    public static class AppRoles
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";
        public const string Support = "Support";
    }
}
