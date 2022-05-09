using Xunit;
using EventManagerAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using EventManagerAPI.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

namespace EventManagerTests
{
    public class EventsControllerTests
    {
        /// <summary>
        /// Tests the GetEvents controller method.
        /// </summary>
        [Fact]
        public async void TestGetEventsMethod()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;
            var context = new EventContext(options);


            context.Events.Add(new Event { Name = "Test One", EventDate = System.DateTime.Now, EventLocation = "Room 204", 
                Summary = " First Test Summary" });
            context.Events.Add(new Event { Name = "Test Two", EventDate = System.DateTime.Now, EventLocation = "Room 205"
                , Summary = "Second Test Summary" });
            context.SaveChanges();
            var controller = new EventsController(context);

            // Act
            var results = await controller.GetEvents();

            // Assert
            int resultCount = results.Value.Count();
            Assert.Equal(2, resultCount);
        }

        /// <summary>
        /// Tests the GetEvent controller method.
        /// </summary>
        [Fact]
        public async void TestGetEventMethod()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;
            var context = new EventContext(options);


            context.Events.Add(new Event { Name = "Test", EventDate = System.DateTime.Now, EventLocation = "Room 204",
                Summary = " First Test Summary" });
            context.SaveChanges();
            var controller = new EventsController(context);

            // Act
            var results = await controller.GetEvent(1);

            // Assert
            Assert.Equal(1, results.Value.Id);
        }

        /// <summary>
        /// Tests the DeleteEvent controller method.
        /// </summary>
        [Fact]
        public async void TestDeleteEventControllerMethod()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;
            var context = new EventContext(options);


            context.Events.Add(new Event { Name = "Test", EventDate = System.DateTime.Now, EventLocation = "Room 204",
                Summary = " First Test Summary" });
            context.SaveChanges();
            var controller = new EventsController(context);

            // Act
            await controller.DeleteEvent(1);
            var results = await controller.GetEvents();

            // Assert
            int resultCount = results.Value.Count();
            Assert.Equal(0, resultCount);
        }

        /// <summary>
        /// Tests the PostEvent controller method.
        /// </summary>
        [Fact]
        public async void TestPostEventsMethod()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<EventContext>()
                        .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                        .Options;
            var context = new EventContext(options);

            var controller = new EventsController(context);

            // Act
            Event newEvent = new Event{ Name = "Test", 
                EventDate = System.DateTime.Now,EventLocation="Room 304", Summary="This is a test value"};

            await controller.PostEvent(newEvent);
            var results = await controller.GetEvents();

            // Assert
            int resultCount = results.Value.Count();
            Assert.Equal(1, resultCount);
        }
    }
}