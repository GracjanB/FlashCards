using FlashCards.Models.Common;

namespace FlashCards.Models.DTOs.ToServer
{
    public class LessonParams : PagedResourceParams
    {
        public string Category { get; set; }
    }
}
