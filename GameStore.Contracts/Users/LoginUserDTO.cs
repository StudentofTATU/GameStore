﻿namespace GameStore.Contracts.Users
{
    public class LoginUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Remember { get; set; }
    }
}
