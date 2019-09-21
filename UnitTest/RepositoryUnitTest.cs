using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoMvc.Models;
using TodoMvc.Repository;

namespace UnitTest
{
    [TestClass]
    public class RepositoryUnitTest
    {
        private static ITodoRepository _repository;
        private static TodoContext _context;

        [TestInitialize]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "memory_database")
                .Options;

            _context = new TodoContext(options);
            _repository = new TodoRepository(_context);

            SeedData();
        }
        [TestCleanup]
        public void CleanUp()
        {
            _context.TodoItems.RemoveRange(_context.TodoItems);
        }

        private void SeedData()
        {
            var items = new List<TodoItemModel>
            {
                new TodoItemModel { ItemName = "First Item", IsCompleted = false, Priority = 0},
                new TodoItemModel { ItemName = "Second Item", IsCompleted = false, Priority = 0},
                new TodoItemModel { ItemName = "Third Item", IsCompleted = false, Priority = 0}
            };

            items.ForEach(s => _context.TodoItems.Add(s));
            _context.SaveChanges();
        }

        [TestMethod]
        public async Task TestGetAll()
        {
            var result = await _repository.GetAllAsync();

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public async Task TestGet()
        {
            var result = await _repository.GetAsync(1);

            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        public async Task TestEdit()
        {
            var result = await _repository.GetAsync(1);

            Assert.AreEqual(result.IsCompleted, false);
            Assert.AreEqual(result.Priority, 0);

            result.IsCompleted = true;
            result.Priority = 10;

            var updatedStatus = await _repository.EditAsync(result.Id, result);
            Assert.IsTrue(updatedStatus);

            var updated = await _repository.GetAsync(1);
            Assert.AreEqual(updated.IsCompleted, true);
            Assert.AreEqual(updated.Priority, 10);
        }

        [TestMethod]
        public async Task TestAddDelete()
        {
            var item = new TodoItemModel() { IsCompleted = false, ItemName = "Test Item" };

            var Id = await _repository.CreateAsync(item);
            var newItem = await _repository.GetAsync(Id);

            var deletedStatus = await _repository.DeleteAsync(Id);

            var deleted = await _repository.GetAsync(Id);

            Assert.AreEqual(Id, newItem.Id);
            Assert.IsNull(deleted);
        }
    }
}
