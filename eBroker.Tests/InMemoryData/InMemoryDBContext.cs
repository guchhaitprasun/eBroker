using eBroker.Data.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBroker.Tests.InMemoryData
{
    public class InMemoryDBContext
    {
        public DbContextOptions<EBrokerDbContext> Options { get { return options; } }
        private DbContextOptions<EBrokerDbContext> options;
        private EBrokerDbContext _context;
        public InMemoryDBContext()
        {
            options = new DbContextOptionsBuilder<EBrokerDbContext>().UseInMemoryDatabase(databaseName: "EBrokerInMemoryContext").Options;
            //options = new DbContextOptionsBuilder<EBrokerDbContext>().UseInMemoryDatabase(databaseName: "EBrokerInMemoryContext" + Guid.NewGuid().ToString()).Options;

            using (_context = new EBrokerDbContext(options))
            {
                // Delete existing db before creating a new one
                _context.Database.EnsureDeleted();
                _context.Database.EnsureCreated();


                _context.User.Add(new User
                {
                    UserId = 100, 
                    EmailAddress = "visualStudioTest@nagp.com", 
                    FirstName = "Dummy", 
                    LastName = "User", 
                    Password = "Passw0rd"
                });

                _context.Account.Add(new Account
                {
                    AccountId = 101,
                    UserId = 100,
                    DmatAccountNumber = "1111-2222-3333-4444",
                    AvailableBalance = 501,
                    IsActive = true
                });

                _context.SaveChanges();
            }
        }
    }

   
}
