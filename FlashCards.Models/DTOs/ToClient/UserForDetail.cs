using System;
using System.Collections.Generic;
using System.Text;

namespace FlashCards.Models.DTOs.ToClient
{
    public class UserForDetail
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public string PhotoUrl { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
