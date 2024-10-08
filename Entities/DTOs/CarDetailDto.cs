﻿using Core;
using System.Collections.Generic;

namespace Entities.DTOs
{
    public class CarDetailDto : IDto
    {
        public int CarId { get; set; }
        public int BrandId { get; set; }

        public int ColorId { get; set; }
        public string CarName { get; set; }
        public string BrandName { get; set; }
        public int DailyPrice { get; set; }
        public string ColorName { get; set; }
        public List<string> Images { get; set; }
        public int MinFindeksScore { get; set; }
    }
}
