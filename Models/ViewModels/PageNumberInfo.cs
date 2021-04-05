﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment10.Models.ViewModels
{
    public class PageNumberInfo
    {
        public int NumItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumItems { get; set; }
        //Calculate Number of Pages
        //When given a decimal, returns next int up
        public int NumPages => (int) Math.Ceiling((decimal) TotalNumItems / NumItemsPerPage);
    }
}
