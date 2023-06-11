﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactManagerServices.Data;

namespace ContactManager.Models
{
    public class EmailViewModel
    {
        public EmailType Type { get; set; }
        public string Email { get; set; }
    }
}
