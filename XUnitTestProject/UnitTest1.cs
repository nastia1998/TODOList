using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using TODOList.BLL.DTO;
using TODOList.DAL;
using TODOList.DAL.Entities;
using TODOList.WEB.Controllers;

using Xunit;


namespace XUnitTestProject
{
    public class UnitTest1
    {
        //private readonly Context _context;
        private UsersController usersController;
        Context c;

        public UnitTest1()
        {
            c = new Context();
            c.Users.Add(new AppUser { Login = "nastya", Email = "lao@yandex.ru", PasswordHash = new byte[] { byte.Parse("sdfdsf") }, PasswordSalt = new byte[] { byte.Parse("sdfjhkhhf") } });
            c.Users.Add(new AppUser { Login = "alena", Email = "alena@yandex.ru", PasswordHash = new byte[] { byte.Parse("sasdfdsfsf") }, PasswordSalt = new byte[] { byte.Parse("sd455454f") } });
            c.Users.Add(new AppUser { Login = "max", Email = "max@yandex.ru", PasswordHash = new byte[] { byte.Parse("sdfsadfdf") }, PasswordSalt = new byte[] { byte.Parse("sdf2154hhf") } });
            c.SaveChanges();

            usersController = new UsersController(
        }

        

        [Fact]
        public void Test1()
        {

            //_context.Users.A
            
            // Act
            //IActionResult result = usersController.GetAll();
            int res = 5 + 3;
            // Assert
            //List<UserDTO> items = Assert.IsType<List<UserDTO>>(result.ToString());
            Assert.Equal(8, res);
        }

        [Fact]
        public void Test2()
        {
            int res = 5 + 8;
            
            Assert.Equal(13, res);
        }

        [Fact]
        public void Test3()
        {
            int res = 5 + 8;

            Assert.Equal(13, res);
        }

        [Fact]
        public void Test4()
        {
            int res = 5 + 8;

            Assert.Equal(13, res);
        }

        [Fact]
        public void Test5()
        {
            int res = 5 + 8;

            Assert.Equal(13, res);
        }
    }
}
