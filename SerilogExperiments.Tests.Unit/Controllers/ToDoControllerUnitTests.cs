namespace SerilogExperiments.Tests.Unit.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using NUnit.Framework;
    using SerilogExperiments.Controllers;
    using SerilogExperiments.Models;
    using SerilogExperiments.Repositories;

    /// <summary>
    /// Unit tests for <see cref="ToDoController"/>
    /// </summary>
    [TestFixture]
    public class ToDoControllerUnitTests
    {
        public ToDoController Controller { get; set; }

        public Mock<IRepository<ToDoItem>> MockRepository { get; set; }

        [SetUp]
        public void Setup()
        {
            this.MockRepository = new Mock<IRepository<ToDoItem>>();
            this.Controller = new ToDoController(this.MockRepository.Object);
        }

        /// <summary>
        /// Given a <see cref="ToDoController"/> is correctly constructed
        /// When calling <see cref="ToDoController.GetById(int)"/> with an invalid id
        /// Then a return a <see cref="HttpStatusCode.BadRequest"/>
        /// </summary>
        /// <param name="id">Invalid todo list item id</param>
        [Test]
        [TestCase(null)]
        [TestCase(-1)]
        [TestCase(0)]
        public async Task GetById_InvalidId_ReturnsBadRequest(int id)
        {
            var response = await this.Controller.GetById(id) as BadRequestResult;
            Assert.IsNotNull(response, "Incorrect response object type");
        }

        /// <summary>
        /// Given no result will be found in the store
        /// When calling <see cref="ToDoController.GetById(int)"/> for a non-existent <see cref="ToDoItem"/>
        /// Then return <see cref="HttpStatusCode.NotFound"/>
        /// </summary>
        [Test]
        public async Task GetById_NoResultFoundInRepository_ReturnsNotFound()
        {
            this.MockRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(default(ToDoItem));

            var response = await this.Controller.GetById(1) as NotFoundObjectResult;
            Assert.IsNotNull(response);
        }

        /// <summary>
        /// Given an existing <see cref="ToDoItem"/> is in the <see cref="ToDoRepository"/>
        /// When calling <see cref="ToDoController.GetById(int)"/> with that item's Id
        /// Then return <see cref="HttpStatusCode.OK"/> with the body of the <see cref="ToDoItem"/>
        /// </summary>
        [Test]
        public async Task GetById_WithExsitingToDoItemId_ReturnsToDoItem()
        {
            var expectedItem = new ToDoItem();
            this.MockRepository
                .Setup(x => x.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(expectedItem);

            var response = await this.Controller.GetById(1) as OkObjectResult;
            Assert.IsNotNull(response);
            var item = response.Value as ToDoItem;
            Assert.AreEqual(expectedItem, item);
        }
    }
}
