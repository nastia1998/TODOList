using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TODOList.BLL.DTO;
using TODOList.BLL.Interfaces;
using TODOList.WEB.Models;

namespace TODOList.WEB.Controllers
{
    public class HomeController : Controller
    {

        ITodoListService todoListService;

        public HomeController(ITodoListService serv)
        {
            todoListService = serv;
        }

        public ActionResult Index()
        {
            IEnumerable<TodoListDTO> listDTOs = todoListService.GetTodoLists();
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TodoListDTO, TodoListViewModel>()).CreateMapper();
            var lists = mapper.Map<IEnumerable<TodoListDTO>, List<TodoListViewModel>>(listDTOs);
            return View(lists);
        }

        public ActionResult MakeTodoList(int? id)
        {
            try
            {
                TodoListDTO listDTO = todoListService.GetTodoList(id);
                var list = new TodoListViewModel { Name = listDTO.Name, Description = listDTO.Description };
                return View(list);
            }
            catch(Exception ex)
            {
                return Content(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult MakeTodoList(TodoListViewModel list)
        {
            try
            {
                var listDTO = new TodoListDTO { Name = list.Name, Description = list.Description };
                todoListService.MakeTodoList(listDTO);
                return Content("<h2>Todo list успешно добавлен!</h2>");
            }
            catch (Exception ex)
            {
                //ModelState.AddModelError(ex.Property, ex.Message);
                Content(ex.Message);
            }
            return View(list);
        }

        protected override void Dispose(bool disposing)
        {
            todoListService.Dispose();
            base.Dispose(disposing);
        }

    }
}
