﻿using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class UserForUpdateDto : IDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

