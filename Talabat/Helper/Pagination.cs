﻿using System.Collections;
using System.Collections.Generic;
using Talabat.DTO;

namespace Talabat.Helper
{
    public class Pagination<T>
    {

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }
        public Pagination(int pageIndex, int pageSize, IReadOnlyList<T> data, int count)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = data;
            Count = count;
        }

        
    }
}
