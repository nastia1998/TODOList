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
using TODOList.BLL.Interfaces;
using TODOList.DAL;
using TODOList.DAL.Entities;

namespace TODOList.BLL.Services
{
    public class UserService : IService<UserDTO>
    {
        Context Context { get; set; }

        public UserService()
        {
            Context = new Context();
        }
        
        public async Task<UserDTO> Authenticate(UserDTO obj, string secretKey)
        {
            var user = await Get(obj.Email, obj.Password);

            if (user is null)
            {
                return null;
            }

            //user.Token = GetToken()
            return user;
        }

        private string GetToken(int id, string secretKey)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = GetSecurityTokenDescriptor(id, key);
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private SecurityTokenDescriptor GetSecurityTokenDescriptor(int id, byte[] key)
        {
            return new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.PrimarySid, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        async public Task<UserDTO> Create(UserDTO userDTO)
        {
            var user = await Context.Users.AddAsync(new User {Login = userDTO.Login, Password = userDTO.Password });
            var res = await Context.SaveChangesAsync();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return res > 0 ? mapper.Map<User, UserDTO>(user.Entity) : null;
        }

        async public Task<UserDTO> Get(int id)
        {
            var user = await Context.Users.FindAsync(id);
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return user != null ? mapper.Map<User, UserDTO>(user) : null;
        }

        async public Task<UserDTO> Get(string email, string password)
        {
            var user = await Context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Verify(password));
            return user == null ? null : new UserDTO
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password
            };

        }

        public IEnumerable<UserDTO> GetAll()
        {
            var mapper =  new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, List<UserDTO>>(Context.Users.ToList());
        }

        async public Task<int> Update(int id, UserDTO userDTO)
        {
            var user = await Context.Users.FindAsync(id);
            user.Login = userDTO.Login;
            user.Password = userDTO.Password;
            return await Context.SaveChangesAsync();
        }

        async public Task<UserDTO> Delete(int id)
        {
            var user = await Context.Users.FindAsync(id);
            if (user == null)
                return null;
            Context.Users.Remove(user);
            int res = await Context.SaveChangesAsync();

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return res > 0 ? mapper.Map<User, UserDTO>(user) : null;

        }

    }
}
