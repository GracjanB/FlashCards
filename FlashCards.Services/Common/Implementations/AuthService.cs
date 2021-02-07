using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using FlashCards.Data.Models;
using FlashCards.Models.DTOs.ToClient;
using FlashCards.Services.Abstracts;
using FlashCards.Services.Repositories.Abstracts;
using AutoMapper;

namespace FlashCards.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthService(IUserRepository userRepository, IConfiguration config, IMapper mapper)
        {
            _config = config ??
                throw new ArgumentNullException(nameof(config));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            _userRepository = userRepository ??
                throw new ArgumentNullException(nameof(userRepository));
        }

        public TokenDTO Login(string email, string password)
        {
            var userFromRepo = _userRepository.GetDetail(email);

            if (userFromRepo == null || !VerifyPasswordHash(password, userFromRepo.PasswordHash, userFromRepo.PasswordSalt))
                return null;

            var tokenDTO = new TokenDTO
            {
                User = _mapper.Map<UserForDetail>(userFromRepo),
                Token = CreateToken(userFromRepo)
            };

            return tokenDTO;
        }

        public bool Register(User user, string password)
        {
            CreatePasswordHash(password, out byte[] passwordhash, out byte[] passwordSalt);
            user.Role = Data.Enums.UserRoleEnum.User;
            user.PasswordHash = passwordhash;
            user.PasswordSalt = passwordSalt;
            _userRepository.Create(user);

            return true;
        }

        public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            return VerifyPasswordHash(password, passwordHash, passwordSalt);
        }

        public void CreateNewPassword(string newPassword, ref User user)
        {
            CreatePasswordHash(newPassword, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
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
