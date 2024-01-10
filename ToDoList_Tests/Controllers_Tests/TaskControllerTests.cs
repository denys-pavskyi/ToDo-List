using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_WebAPI.Controllers;

namespace ToDoList_Tests.Controllers_Tests
{
    [TestFixture]
    public class TaskControllerTests
    {

        private TaskController _controller;
        private Mock<ITaskService> _mockService;

        [SetUp]
        public void Setup()
        {
            _mockService = new Mock<ITaskService>();
            _controller = new TaskController(_mockService.Object);
        }

        //UpdateTaskStatusById Tests
        [Test]
        public async Task UpdateTaskStatusById_WithValidId_ReturnsOk()
        {
            // Arrange
            _mockService.Setup(x => x.UpdateTaskStatusByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                        .ReturnsAsync(true);

            // Act
            var result = await _controller.UpdateTaskStatusById(1, 2);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task UpdateTaskStatusById_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            _mockService.Setup(x => x.UpdateTaskStatusByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                        .ReturnsAsync(false);

            // Act
            var result = await _controller.UpdateTaskStatusById(1, 2); 

            // Assert
            Assert.IsInstanceOf<NotFoundObjectResult>(result);
            Assert.AreEqual("Task not found", ((NotFoundObjectResult)result).Value);
        }

        [Test]
        public async Task UpdateTaskStatusById_WhenServiceThrowsToDoListException_ReturnsBadRequest()
        {
            // Arrange
            _mockService.Setup(x => x.UpdateTaskStatusByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                        .ThrowsAsync(new ToDoListException("Wrong status id"));

            // Act
            var result = await _controller.UpdateTaskStatusById(1, 2); 

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            Assert.AreEqual("Wrong status id", ((BadRequestObjectResult)result).Value);
        }

        // GetById tests
        [Test]
        public async Task GetById_ExistingId_ReturnsTaskModel()
        {
            // Arrange
            int existingId = 1; 
            var mockTask = new TaskModel { Id = existingId, Name = "Test Task" };
            _mockService.Setup(x => x.GetTaskByIdAsync(existingId)).ReturnsAsync(mockTask);

            // Act
            var objectResult = await _controller.GetById(existingId);
            var result = ((ObjectResult)objectResult.Result);

            // Assert
            Assert.IsInstanceOf<ActionResult<TaskModel>>(objectResult);
            Assert.IsInstanceOf<ObjectResult>(result);
            Assert.IsAssignableFrom<TaskModel>(result.Value);
            var taskModel = (TaskModel)result.Value;
            Assert.AreEqual(existingId, taskModel.Id);
        }

        [Test]
        public async Task GetById_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            int nonExistingId = 100; // non-existing task ID
            _mockService.Setup(x => x.GetTaskByIdAsync(nonExistingId)).ReturnsAsync((TaskModel)null);

            // Act
            var result = await _controller.GetById(nonExistingId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result.Result);
        }

        // Delete Tests

        [Test]
        public async Task Delete_ExistingId_ReturnsOkWithTaskModel()
        {
            // Arrange
            int existingId = 1; // existing task ID
            var mockTask = new TaskModel { Id = existingId, Name = "Test Task" };
            _mockService.Setup(x => x.GetTaskByIdAsync(existingId)).ReturnsAsync(mockTask);

            // Act
            var objectResult = await _controller.Delete(existingId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(objectResult);
            var result = (OkObjectResult)objectResult;
            Assert.IsAssignableFrom<TaskModel>(result.Value);
            var taskModel = (TaskModel)result.Value;
            Assert.AreEqual(existingId, taskModel.Id);
            _mockService.Verify(x => x.DeleteTaskAsync(existingId), Times.Once);
        }

        [Test]
        public async Task Delete_NonExistingId_ReturnsNotFound()
        {
            // Arrange
            int nonExistingId = 100; // Provide a non-existing task ID
            _mockService.Setup(x => x.GetTaskByIdAsync(nonExistingId)).ReturnsAsync((TaskModel)null);

            // Act
            var result = await _controller.Delete(nonExistingId);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            _mockService.Verify(x => x.DeleteTaskAsync(nonExistingId), Times.Never); 
        }

        [Test]
        public async Task Delete_ServiceThrowsException_ReturnsBadRequest()
        {
            // Arrange
            int existingId = 1; // Provide an existing task ID
            var mockTask = new TaskModel { Id = existingId, Name = "Test Task" };
            _mockService.Setup(x => x.GetTaskByIdAsync(existingId)).ReturnsAsync(mockTask);
            _mockService.Setup(x => x.DeleteTaskAsync(existingId)).ThrowsAsync(new Exception()); 

            // Act
            var result = await _controller.Delete(existingId);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
            _mockService.Verify(x => x.DeleteTaskAsync(existingId), Times.Once);
        }

    }
}
