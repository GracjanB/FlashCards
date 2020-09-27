using FlashCards.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace FlashCards.Helpers
{
    public static class AutomapperExtensions
    {
        public static decimal CalculateAverageRating(this ICollection<CourseOpinion> courseOpinions)
        {
            return courseOpinions.Count != 0 ? courseOpinions.Sum(x => x.Rating) / courseOpinions.Count : 0M;
        }
    }
}
