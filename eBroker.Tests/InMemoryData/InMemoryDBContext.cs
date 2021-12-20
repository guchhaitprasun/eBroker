using eBroker.Data.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eBroker.Tests.InMemoryData
{
    /// <summary>
    /// Class to use In memory database for partial testing 
    /// </summary>
    public class InMemoryDBContext : IDisposable
    {
        public DbContextOptions<EBrokerDbContext> Options { get { return options; } }
        private DbContextOptions<EBrokerDbContext> options;
        private EBrokerDbContext _context;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public InMemoryDBContext()
        {
            //options = new DbContextOptionsBuilder<EBrokerDbContext>().UseInMemoryDatabase(databaseName: "EBrokerInMemoryContext").Options;
            options = new DbContextOptionsBuilder<EBrokerDbContext>().UseInMemoryDatabase(databaseName: "EBrokerInMemoryContext" + Guid.NewGuid().ToString()).Options;

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

                _context.User.Add(new User
                {
                    UserId = 101,
                    EmailAddress = "visualStudioTestTwo@nagp.com",
                    FirstName = "Dummy",
                    LastName = "UserTwo",
                    Password = "Passw0rd"
                });

                _context.Stock.Add(new Stock
                {
                    StockId = 1, 
                    StockName = "ICIC Bank", 
                    Price = 709.55m, 
                    DayHigh = 720.20m, 
                    DayLow = 705.10m, 
                    IsActive = true
                });

                _context.Account.Add(new Account
                {
                    AccountId = 101,
                    UserId = 100,
                    DmatAccountNumber = "1111-2222-3333-4444",
                    AvailableBalance = 1000,
                    IsActive = true
                });

                _context.UserPortfolio.Add(new UserPortfolio
                {
                    RecordId = 1, 
                    UserId = 100, 
                    StockId = 1, 
                    StockQty = 10, 
                    InvestedAmount = 7095.50m, 
                    IsActive = true
                });

                _context.SaveChanges();
            }
        }
    
        /// <summary>
        /// Object disposal
        /// </summary>
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

   
}
