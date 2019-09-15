using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoMvc.Models;
using TodoMvc.Repository;

namespace TodoMvc.Controllers
{
    public class ItemController : Controller
    {
        private readonly ITodoRepository _repository;

        public ItemController(ITodoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _repository.GetAllAsync();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _repository.GetAsync(id);
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TodoItemModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _repository.CreateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _repository.GetAsync(id);
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TodoItemModel model)
        {
            var result = await _repository.EditAsync(id, model);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Complete(int id, bool status)
        {
            var result = await _repository.CompleteAsync(id, status);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.GetAsync(id);
            return View(result);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteOk(int id)
        {
            var result = await _repository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}