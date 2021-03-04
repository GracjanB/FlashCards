using FlashCards.Data.DataModel;
using FlashCards.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace FlashCards.Data
{
    public static class Seed
    {
        public static void SeedAdministratorsAndDefaultCourse(FlashcardsDataModel context)
        {
            if (!context.Users.Any())
            {
                var defaultAdministrator = CreateDefaultAdministrator();
                var superAdministrator = CreateSuperAdministrator();
                context.Users.Add(defaultAdministrator);
                context.Users.Add(superAdministrator);
                context.SaveChanges();

                SeedDefaultCourse(context, defaultAdministrator.UserInfo.Id);
            }
        }

        public static void SeedDefaultCourse(FlashcardsDataModel context, int accountId)
        {
            if (!context.Courses.Any())
            {
                var course = CreateDefaultCourse();
                course.AccountCreatedId = accountId;
                context.Courses.Add(course);
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

        private static Course CreateDefaultCourse()
        {
            return new Course
            {
                Id = 0,
                Name = "Angielski dla specjalistów IT",
                Description = "Kurs ten przeznaczony jest dla szerokiego grona odbiorców, którzy władają językiem angielskim na poziomie średnio zaawansowanym. Osoby znające ten język na poziomie zaawansowanym również odnajdą w kursie wiele przydatnych słów. Znajdą tu coś dla siebie zarówno eksperci z branży IT, jak i osoby, które dopiero rozpoczynają swoją przygodę zawodową w tej dziedzinie.",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                CourseType = Enums.CourseTypeEnum.Public,
                Status = Enums.CourseStatusEnum.Accepted,
                AccountCreatedId = 1,
                CourseInfo = new CourseInfo
                {
                    Id = 0,
                    AmountOfEnrolled = 0
                },
                Lessons = new List<Lesson>()
                {
                    new Lesson
                    {
                        Id = 0,
                        Name = "Lekcja 1",
                        Description = "",
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        Category = "Computer Science",
                        Flashcards = new List<Flashcard>()
                        {
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "accurate",
                                TranslatedPhrase = "dokładny",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "to allocate resources",
                                TranslatedPhrase = "alokować zasoby",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "assets",
                                TranslatedPhrase = "aktywa",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "at once",
                                TranslatedPhrase = "od razu",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "brand",
                                TranslatedPhrase = "marka",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "business goal",
                                TranslatedPhrase = "cel biznesowy",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "challenge",
                                TranslatedPhrase = "wyzwanie",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "completion",
                                TranslatedPhrase = "zakończenie",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "crucial",
                                TranslatedPhrase = "istotny",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "currently",
                                TranslatedPhrase = "obecnie",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                        }
                    },
                    new Lesson
                    {
                        Id = 0,
                        Name = "Lekcja 2",
                        Description = "",
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        Category = "Computer Science",
                        Flashcards = new List<Flashcard>()
                        {
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "device",
                                TranslatedPhrase = "urządzenie",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "effective",
                                TranslatedPhrase = "efektywny",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "efficient",
                                TranslatedPhrase = "wydajny",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "to evaluate",
                                TranslatedPhrase = "szacować",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "future gain",
                                TranslatedPhrase = "przyszły zysk",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "hardware",
                                TranslatedPhrase = "sprzęt",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "impact",
                                TranslatedPhrase = "wpływ",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "in light of",
                                TranslatedPhrase = "w świetle",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "in other words",
                                TranslatedPhrase = "innymi słowy",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "information system",
                                TranslatedPhrase = "system informacyjny",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                        }
                    },
                    new Lesson
                    {
                        Id = 0,
                        Name = "Lekcja 3",
                        Description = "",
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        Category = "Computer Science",
                        Flashcards = new List<Flashcard>()
                        {
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "key to success",
                                TranslatedPhrase = "klucz do sukcesu",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "meaningful",
                                TranslatedPhrase = "znaczący",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "network",
                                TranslatedPhrase = "sieć",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "on time",
                                TranslatedPhrase = "na czas",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "to optimize",
                                TranslatedPhrase = "optymalizować",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "periodic report",
                                TranslatedPhrase = "raport okresowy",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "to process",
                                TranslatedPhrase = "przetwarzać",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "profitability",
                                TranslatedPhrase = "zyskowność",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "raw data",
                                TranslatedPhrase = "surowe dane",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            },
                            new Flashcard
                            {
                                Id = 0,
                                Phrase = "valuable",
                                TranslatedPhrase = "cenny",
                                LanguageLevel = Enums.LanguageLevelEnum.B2_C1,
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now
                            }
                        }
                    }
                }
            };
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
