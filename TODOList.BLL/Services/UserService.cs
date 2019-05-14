using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TODOList.BLL.DTO;
using TODOList.BLL.Helpers;
using TODOList.BLL.Interfaces;
using TODOList.DAL;
using TODOList.DAL.Entities;

namespace TODOList.BLL.Services
{
    public class UserService : IUserService
    {
        Context Context { get; set; }

        public UserService()
        {
            Context = new Context();
        }

        public UserDTO Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;
            var user = Context.Users.SingleOrDefault(x => x.Login == username);
            if (user == null)
                return null;
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, UserDTO>()).CreateMapper();
            return mapper.Map<AppUser, UserDTO>(user);
        }

        public string GetToken(string secret, UserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
            
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<AppUser>, List<UserDTO>>(Context.Users.ToList());
        }

        async public Task<UserDTO> GetById(int id)
        {
            var user = await Context.Users.FindAsync(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, UserDTO>()).CreateMapper();
            return user != null ? mapper.Map<AppUser, UserDTO>(user) : null;
        }

        async public Task<UserDTO> Create(UserDTO userDTO, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Password is required");
            if (Context.Users.Any(x => x.Login == userDTO.Login))
                throw new AppException("Login \"" + userDTO.Login + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = await Context.Users.AddAsync(new AppUser { Login = userDTO.Login, Email = userDTO.Email, PasswordHash = passwordHash, PasswordSalt = passwordSalt });
            var res = await Context.SaveChangesAsync();

            user.Entity.PasswordHash = passwordHash;
            user.Entity.PasswordSalt = passwordSalt;

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, UserDTO>()).CreateMapper();
            return res > 0 ? mapper.Map<AppUser, UserDTO>(user.Entity) : null;

        }

        public void Update(UserDTO userParam, string password = null)
        {
            var user = Context.Users.Find(userParam.Id);

            if (user == null)
                throw new AppException("User not found");

            if (userParam.Login != user.Login)
            {
                // username has changed so check if the new username is already taken
                if (Context.Users.Any(x => x.Login == userParam.Login))
                    throw new AppException("Username " + userParam.Login + " is already taken");
            }

            // update user properties
            user.Login = userParam.Login;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            Context.Users.Update(user);
            Context.SaveChangesAsync();
        }

        async public Task<UserDTO> Delete(int id)
        {
            var user = await Context.Users.FindAsync(id);
            if (user == null)
                return null;
            Context.Users.Remove(user);
            int res = await Context.SaveChangesAsync();
           
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<AppUser, UserDTO>()).CreateMapper();
            return res > 0 ? mapper.Map<AppUser, UserDTO>(user) : null;
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

    }
}
