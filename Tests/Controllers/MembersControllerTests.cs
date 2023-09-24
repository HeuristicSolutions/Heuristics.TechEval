using System.Web.Mvc;
using System.Data.SqlClient;
using Heuristics.TechEval.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Web.Models;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;

namespace Heuristics.TechEval.Tests.Controllers {

    /*
     * Integration Tests for Member Controller
     */
	[TestClass]
	public class MembersControllerTest {
        private MembersController _membersController;
        private DbConnection _connection;
        private DbContextTransaction _transaction;
        private DataContext _context;

		[TestInitialize]
		public void Init()
		{
			_context = new DataContext();
            _connection = _context.Database.Connection;
			_membersController = new MembersController(_context);
		}


		[TestMethod]
		public void ListTest() // Validates the Members/List page renders
		{
			var result = _membersController.List() as ViewResult;
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void TestNewMemberWithUniqueEmail() // Validates a member can be added to the database.
        {
            _connection.Open();
            /*
             Create transaction instance in order to rollback changes to an in-memory database.
             This prevents traffic in the actual database and ensures the data set is not too large.
            */
            _transaction = _context.Database.BeginTransaction(); 

			// Arrange
			var data = new NewMember
			{
				Name = "This Is A Test",
				Email = "test@test.com"
			};
			// Act
			var emailParameterForBeforeAddition = new SqlParameter("@Email", data.Email);
            var countBeforeAddition = _context.Database.SqlQuery<int>("SELECT COUNT(*) FROM Member WHERE Email = @Email", emailParameterForBeforeAddition).Single();
            Assert.AreEqual(0, countBeforeAddition); // Validates there are not any values in the DB with the email being used.

			_membersController.New(data);

			// Assert
            var emailParameterForAfterAddition = new SqlParameter("@Email", data.Email);
			var countAfterAddition = _context.Database.SqlQuery<int>("SELECT COUNT(*) FROM Member WHERE Email = @Email", emailParameterForAfterAddition).Single();
			Assert.AreEqual(1, countAfterAddition); // Verifies test data was successfully added.

			// Rollback the transaction to undo changes made during the test
			_transaction.Rollback();
		}

        [TestMethod]
        public void TestNewMemberWithExistingEmail() // Validates a member can be added to the database.
        {
            _connection.Open();
            _transaction = _context.Database.BeginTransaction(); // Create transaction instance in order to rollback changes to database.
			
            // Arrange
			var data = new NewMember
            {
                Name = "This Is A Test",
                Email = "test@test.com"
            };
            var emailForInsertQuery = new SqlParameter("@Email", data.Email);
            var nameForInsertQuery = new SqlParameter("@Name", data.Name);
			_context.Database.ExecuteSqlCommand("INSERT INTO Member(Name, Email, LastUpdated) VALUES(@Name, @Email, GETDATE())", nameForInsertQuery, emailForInsertQuery);

			// Act
            var result = _membersController.New(data) as HttpStatusCodeResult;

            // Assert
            Assert.AreEqual(409, result.StatusCode);
            Assert.AreEqual($"Email {data.Email} already in use", result.StatusDescription);

            var emailParameterForAfterAddition = new SqlParameter("@Email", data.Email);
            var countAfterAddition = _context.Database.SqlQuery<int>("SELECT COUNT(*) FROM Member WHERE Email = @Email", emailParameterForAfterAddition).Single();
            Assert.AreEqual(1, countAfterAddition); // Verifies test data was successfully added.

            // Rollback the transaction to undo changes made during the test.
            _transaction.Rollback();
        }

		[TestCleanup]
        public void CleanUp() // Ensures any open connections have been disposed of.
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }

            if (_transaction != null)
            {
                _transaction.Dispose();
            }
		}
	}
}
