﻿using AutoMapper;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Models.DTOs.ToServer;
using FlashCards.Services.Abstracts;
using FlashCards.Services.Repositories.Abstracts;
using FlashCards.Services.UnitOfWork.Abstracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FlashCards.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IConfiguration config, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? 
                throw new ArgumentNullException(nameof(unitOfWork));

            _config = config ??
                throw new ArgumentNullException(nameof(config));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _userRepository = unitOfWork.GetUserRepository();
        }

        public TokenDTO Login(string email, string password)
        {
            var userFromRepo = _userRepository.GetDetail(email);
            if (userFromRepo == null || VerifyPasswordHash(password, userFromRepo.PasswordHash, userFromRepo.PasswordSalt))
                return null;

            var tokenDTO = new TokenDTO
            {
                User = _mapper.Map<UserForDetail>(userFromRepo),
                Token = CreateToken(userFromRepo)
            };

            return tokenDTO;
        }

        public bool Logout(int userId)
        {
            throw new NotImplementedException();
        }

        public bool Register(User user, string password)
        {
            CreatePasswordHash(password, out byte[] passwordhash, out byte[] passwordSalt);
            user.PasswordHash = passwordhash;
            user.PasswordSalt = passwordSalt;
            _userRepository.Add(user);
            _unitOfWork.Save();

            return true;
        }

        public bool AuthorizeUser(int userId, string token)
        {
            throw new NotImplementedException();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var HMAC = new HMACSHA512())
            {
                passwordSalt = HMAC.Key;
                passwordHash = HMAC.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var HMAC = new HMACSHA512(passwordSalt))
            {
                var computedHash = HMAC.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                    if (computedHash[i] != passwordHash[i])
                        return false;
                return true;
            }
        }

        private string CreateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), // for educational purposes
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string token = tokenHandler.WriteToken(securityToken);

            return token;
        }
    }
}
