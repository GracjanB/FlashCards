using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Models.DTOs.ToClient
{
    public class SubscribedLessonForList
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Progress { get; set; }
    }
}
