﻿using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RentalDetailDto : IDto
    {
        public string CarName { get; set; }
        public string CustomerName { get; set; }
    }
}
