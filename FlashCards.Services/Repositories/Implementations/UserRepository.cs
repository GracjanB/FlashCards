﻿using AutoMapper;
using FlashCards.Data.DataModel;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Services.Exceptions;
using FlashCards.Services.Repositories.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCards.Services.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly FlashcardsDataModel _context;
        private readonly IMapper _mapper;

        public UserRepository(FlashcardsDataModel context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Create(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public User Get(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public UserForDetailCourses GetDetailedWithCourses(int id)
        {
            var userFromRepo = _context.Users
                .Include(x => x.UserInfo)
                .Include(x => x.UserInfo.CreatedCourses)
                .ThenInclude(x => x.AccountCreated)
                .Include(x => x.UserInfo.SubscribedCourses)
                .FirstOrDefault(x => x.Id == id);

            if (userFromRepo != null)
            {
                var subscribedCourseIds = userFromRepo.UserInfo.SubscribedCourses.Select(x => x.CourseId).ToList();
                var subscribedCourses = _context.Courses.Where(x => subscribedCourseIds.Contains(x.Id)).ToList();
                var privateCourses = _context.Courses.Where(x => x.AccountCreatedId == userFromRepo.UserInfoId && x.CourseType == Data.Enums.CourseTypeEnum.Private);
                var draftCourses = _context.Courses.Where(x => x.AccountCreatedId == userFromRepo.UserInfoId && x.CourseType == Data.Enums.CourseTypeEnum.Draft);
                var userToReturn = _mapper.Map<UserForDetailCourses>(userFromRepo);
                userToReturn.SubscribedCourses = _mapper.Map<IEnumerable<CourseForList>>(subscribedCourses);
                userToReturn.CreatedCourses = _mapper.Map<IEnumerable<CourseForList>>(userFromRepo.UserInfo.CreatedCourses);
                userToReturn.PrivateCourses = _mapper.Map<IEnumerable<CourseForList>>(privateCourses);
                userToReturn.DraftCourses = _mapper.Map<IEnumerable<CourseForList>>(draftCourses);
                userToReturn.NumberOfAlreadyLearntFlashcards = _context.SubscribedFlashcards.Where(x => x.SubscribedLesson.SubscribedCourse.AccountId == userFromRepo.UserInfoId && x.TrainLevel >= 10).Count();
                userToReturn.NumberOfCreatedCourses = _context.Courses.Where(x => x.AccountCreatedId == userFromRepo.UserInfoId).Count();
                userToReturn.NumberOfSubscribedCourses = _context.SubscribedCourses.Where(x => x.IsSubscribed && x.AccountId == userFromRepo.UserInfoId).Count();

                return userToReturn;
            }
            else throw new UserNotFoundException();
        }

        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public User Update(int userId, UserForUpdate userForUpdate)
        {
            var userFromRepo = _context.Users.Include(x => x.UserInfo).FirstOrDefault(x => x.Id == userId);

            if (userFromRepo != null)
            {
                userFromRepo.UserInfo.FirstName = userForUpdate.FirstName;
                userFromRepo.UserInfo.LastName = userForUpdate.LastName;
                userFromRepo.UserInfo.DisplayName = userForUpdate.DisplayName;
                userFromRepo.UserInfo.City = userForUpdate.City;
                userFromRepo.UserInfo.Country = userForUpdate.Country;
                _context.SaveChanges();

                return userFromRepo;
            }
            else throw new UserNotFoundException();
        }

        public User GetDetail(int id)
        {
            return _context.Users.Include(x => x.UserInfo).FirstOrDefault(x => x.Id == id);
        }

        public User GetDetail(string email)
        {
            return _context.Users.Include(x => x.UserInfo).FirstOrDefault(x => x.Email == email);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Include(x => x.UserInfo).ToList();
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }

        public int GetUserAccountId(int userId)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == userId);
            return user != null ? user.UserInfoId : 0;
        }

        public bool IsAdministrator(int id)
        {
            var user = _context.Users.Include(x => x.UserInfo).FirstOrDefault(x => x.Id == id);

            return user != null && (user.Role == Data.Enums.UserRoleEnum.Administrator || user.Role == Data.Enums.UserRoleEnum.SuperAdministrator);
        }
    }
}
