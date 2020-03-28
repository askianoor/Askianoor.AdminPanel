using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Askianoor.AdminPanel.Data
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string googleRecaptcha { get; set; }
    }

    public class LoginResponse
    {
        public string accessToken { get; set; }
    }

    public class ReCaptcha
    {
        public string Success { get; set; }
        public string Score { get; set; }
        public string ChallengeTs { get; set; }
        public string HostName { get; set; }
    }

}
