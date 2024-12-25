﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.Services.TokenServices.Realizations
{
    public class JWTTokenConfiguration
    {
        public double AccessTokenExpirationMinutes { get; set; }        
        public string? SecretKey { get; set; } = string.Empty;
        public string? Issuer { get; set; } = string.Empty;
        public string? Audience { get; set; } = string.Empty;        
    }
}