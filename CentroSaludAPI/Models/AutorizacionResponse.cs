﻿using Microsoft.VisualBasic;

namespace CentroSaludAPI.Models
{
    public class AutorizacionResponse
    {
        public string Token { get; set; }
        public bool Resultado { get; set; }
        public string Msg { get; set; }
    }
}
