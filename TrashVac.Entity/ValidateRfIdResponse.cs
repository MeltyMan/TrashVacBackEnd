﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TrashVac.Entity
{
    public class ValidateRfIdResponse
    {
        public string RfId { get; set; }
        public bool IsValid { get; set; }
        public User User { get; set; }
    }
}
