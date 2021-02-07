using FlashCards.Data.DataModel;
using FlashCards.Data.Models;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FlashCards.Data
{
    public static class Seed
    {
        public static void SeedAdministrators(FlashcardsDataModel context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(CreateDefaultAdministrator());
                context.Users.Add(CreateSuperAdministrator());
                context.SaveChanges();
            }
        }

        private static User CreateSuperAdministrator()
        {
            var superAdministrator = new User()
            {
                Id = 0,
                Email = "superadmin@admin.admin",
                Role = Enums.UserRoleEnum.SuperAdministrator,
                UserInfo = new UserInfo
                {
                    Id = 0,
                    FirstName = "Super",
                    LastName = "Admin",
                    DisplayName = "Super Administrator"
                }
            };

            CreatePasswordHash("password", out byte[] passwordHash, out byte[] passwordSald);
            superAdministrator.PasswordHash = passwordHash;
            superAdministrator.PasswordSalt = passwordSald;

            return superAdministrator;
        }

        private static User CreateDefaultAdministrator()
        {
            var administrator = new User()
            {
                Id = 0,
                Email = "admin@admin.admin",
                Role = Enums.UserRoleEnum.Administrator,
                UserInfo = new UserInfo
                {
                    Id = 0,
                    FirstName = "Admin",
                    LastName = "Admin",
                    DisplayName = "Administrator"
                }
            };

            CreatePasswordHash("password", out byte[] passwordHash, out byte[] passwordSald);
            administrator.PasswordHash = passwordHash;
            administrator.PasswordSalt = passwordSald;

            return administrator;
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var HMAC = new HMACSHA512())
            {
                passwordSalt = HMAC.Key;
                passwordHash = HMAC.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
