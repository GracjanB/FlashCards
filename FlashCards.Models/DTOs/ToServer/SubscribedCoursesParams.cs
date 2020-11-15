using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Models.DTOs.ToServer
{
    public class SubscribedCoursesParams
    {
        private const int MaxPageSize = 20;

        private int _pageSize = 10;

        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }
        }

        public int PageNumber { get; set; } = 1;
    }
}
