namespace FlashCards.Data.Models
{
    public class CourseInfo
    {
        public int Id { get; set; }

        public int AmountOfEnrolled { get; set; }


        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
