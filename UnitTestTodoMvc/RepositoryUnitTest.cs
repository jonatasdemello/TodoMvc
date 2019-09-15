using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TodoMvc.Models;
using TodoMvc.Repository;

namespace UnitTestTodoMvc
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
        public void TestGetAll()
        {
            var result = _repository.GetAll();

            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void TestGet()
        {
            var result = _repository.Get(1);

            Assert.AreEqual(result.Id, 1);
        }

        [TestMethod]
        public void TestEdit()
        {
            var result = _repository.Get(1);

            Assert.AreEqual(result.IsCompleted, false);
            Assert.AreEqual(result.Priority, 0);

            result.IsCompleted = true;
            result.Priority = 10;

            var updatedStatus = _repository.Edit(result.Id, result);
            Assert.IsTrue(updatedStatus);

            var updated = _repository.Get(1);
            Assert.AreEqual(updated.IsCompleted, true);
            Assert.AreEqual(updated.Priority, 10);
        }

        [TestMethod]
        public void TestAddDelete()
        {
            var item = new TodoItemModel() { IsCompleted = false, ItemName = "Test Item" };

            var Id = _repository.Create(item);
            var newItem = _repository.Get(Id);

            var deletedStatus = _repository.Delete(Id);

            var deleted = _repository.Get(Id);

            Assert.AreEqual(Id, newItem.Id);
            Assert.IsNull(deleted);
        }
    }
}
