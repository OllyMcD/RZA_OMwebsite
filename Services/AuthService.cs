using System;

namespace RZA_OMwebsite.Services
{
    public class AuthService
    {
        private bool isLoggedIn;

        public bool IsLoggedIn => isLoggedIn;

        public void Login()
        {
            isLoggedIn = true;
        }

        public void Logout()
        {
            isLoggedIn = false;
        }
    }
}
