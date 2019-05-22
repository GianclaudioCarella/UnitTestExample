using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestExample.Business.Data;
using UnitTestExample.Business.Models;
using UnitTestExample.Business.Services;

namespace UnitTestExample.Tests
{
    [TestClass]
    public class BillTest
    {

        [TestMethod]
        public void SplitBill_GetTotalUsers()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;

            // Arrange

            // New User
            User user = new User() { UserId = Guid.NewGuid(), Name = "Gian", Email = "gian@gmail.com", Total = 0 };
            User user2 = new User() { UserId = Guid.NewGuid(), Name = "Rocio", Email = "rocio@gmail.com", Total = 0 };
            User user3 = new User() { UserId = Guid.NewGuid(), Name = "Jose", Email = "jose@gmail.com", Total = 0 };

            // New Bill
            Bill bill = new Bill()
            {
                BillId = Guid.NewGuid(),
                Debit = 1955.66,
                Users = new List<User>() { user, user2, user3 }
            };

            // New Trip
            Trip trip = new Trip() { 
                TripId = Guid.NewGuid(), 
                Location = "Paris", 
                Bills = new List<Bill>() { bill } };

            // Add Trip
            using (var context = new ApplicationDbContext(options))
            {
                var service = new TripService(context);
                service.Add(trip);
            }

            // Act
            // Run the test against one instance of the context
            using (var context = new ApplicationDbContext(options))
            {
                var service = new BillService(context);
                service.SplitBill(bill);
            }

            // Assert
            double expected = 1955.66 / 3;

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new ApplicationDbContext(options))
            {
                var service = new UserService(context);
                var total = service.GetTotal(user);
                Assert.AreEqual(expected, total, "Split not divided correctly");
            }

        }
    }
}
