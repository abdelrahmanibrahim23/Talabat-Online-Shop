﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification
{
    public class ProductSpecPrams
    {
        private const int MaxSize = 10;
        public int PageIndex { get; set; } =1;
        private int pageSize=5;

        public int PageSize 
        {
            get { return pageSize; }
            set { pageSize = value> MaxSize? MaxSize:value; }
        }

        private string search;

        public string Search
        {
            get { return search; }
            set { search = value; }
        }

        public string Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

    }
}
