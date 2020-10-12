namespace FlashCards.Models.DTOs.ToServer
{
    public class CourseParams
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

        public int CourseType { get; set; }
    }
}
