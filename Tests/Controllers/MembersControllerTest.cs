using System.Web.Mvc;
using Heuristics.TechEval.Web.Controllers;
using Heuristics.TechEval.Web.Services;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Core.Models;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Heuristics.TechEval.Tests.Controllers
{
    [TestFixture]
    public class MembersControllerTest
    {
		private Mock<IMemberService> memberServiceMock;
		private MembersController membersController;

		[SetUp]
		public void SetUp()
        {
			memberServiceMock = new Mock<IMemberService>(MockBehavior.Loose);
			membersController = new MembersController(memberServiceMock.Object);
        }

		[Test]
		public void List_HasData()
		{
			// Arrange
			memberServiceMock.Setup(x => x.GetMembers()).Returns(new List<Member> 
				{
					new Member{Id = 1, Name = "Test User", Email = "testuser@test.com"},
					new Member{Id = 2, Name = "Test User2", Email = "testuser2@test.com"},
					new Member{Id = 3, Name = "Test User3", Email = "testuser3@test.com"}
				});

			// Act
			ViewResult result = membersController.List() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.That(result, Is.InstanceOf<ViewResult>());
			Assert.That(result.Model, Is.InstanceOf<List<Member>>());

			var model = (List<Member>)result.ViewData.Model;
			Assert.That(model.Count, Is.EqualTo(3));
		}

		[Test]
		public void List_HasNoData()
		{
			// Arrange
			memberServiceMock.Setup(x => x.GetMembers()).Returns(new List<Member>{});

			// Act
			ViewResult result = membersController.List() as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.That(result, Is.InstanceOf<ViewResult>());
			Assert.That(result.Model, Is.InstanceOf<List<Member>>());

			var model = (List<Member>)result.ViewData.Model;
			Assert.That(model.Count, Is.EqualTo(0));
		}

		[Test]
		[TestCase(1, "Test User", "testuser@gmail.com")]
		[TestCase(2, "Test User2", "testuser2@gmail.com")]
		[TestCase(3, "Test User3", "testuser3@gmail.com")]
		public void New_Success(int id, string name, string email)
		{
			// Arrange
			memberServiceMock.Setup(x => x.AddMember(It.IsAny<MemberModel>())).Returns(new Member { Id = id, Name = name, Email = email });

			var member = new MemberModel { Name = name, Email = email };

			// Act
			JsonResult result = membersController.New(member) as JsonResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.That(result, Is.InstanceOf<JsonResult>());

			var model = JsonConvert.DeserializeObject<MemberModel>(result.Data.ToString());
			Assert.That(model.Name, Is.EqualTo(name));
			Assert.That(model.Email, Is.EqualTo(email));
		}

		// TODO: Debug why ModelState is valid when it shouldn't be
		[Test]
		[TestCase(0, null, "testuser@gmail.com")]
		[TestCase(0, "Test User2", null)]
		[TestCase(0, null, null)]
		public void New_Failure(int id, string name, string email)
		{
			// Arrange
			membersController.ViewData.ModelState.Clear();
			memberServiceMock.Setup(x => x.AddMember(It.IsAny<MemberModel>())).Returns(new Member { Id = id, Name = name, Email = email });

			var member = new MemberModel { Name = name, Email = email };

			// Act
			ViewResult result = membersController.New(member) as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.IsTrue(membersController.ModelState.Count > 0);
		}

		[Test]
		[TestCase(1, "Test User", "testuser@gmail.com")]
		public void New_Failure_EmailAlreadyExists(int id, string name, string email)
		{
			// Arrange
			memberServiceMock.Setup(x => x.AddMember(It.IsAny<MemberModel>())).Returns(new Member { Id = id, Name = name, Email = email });
			memberServiceMock.Setup(x => x.GetMemberByEmail(It.IsAny<string>())).Returns(new Member { Id = id, Name = name, Email = email });

			var member = new MemberModel { Name = name, Email = email };

			// Act
			ViewResult result = membersController.New(member) as ViewResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.That(result, Is.InstanceOf<ViewResult>());
			Assert.IsTrue(membersController.ModelState.Count > 0);
		}

		[Test]
		[TestCase(1, "Test User", "testuser@gmail.com")]
		[TestCase(2, "Test User2", "testuser2@gmail.com")]
		[TestCase(3, "Test User3", "testuser3@gmail.com")]
		public void Edit_Success(int id, string name, string email)
		{
			// Arrange
			memberServiceMock.Setup(x => x.UpdateMember(It.IsAny<MemberModel>())).Returns(new Member { Id = id, Name = name, Email = email });

			var member = new MemberModel { Id = id, Name = name, Email = email };

			// Act
			JsonResult result = membersController.Edit(member) as JsonResult;

			// Assert
			Assert.IsNotNull(result);
			Assert.That(result, Is.InstanceOf<JsonResult>());

			var model = JsonConvert.DeserializeObject<MemberModel>(result.Data.ToString());
			Assert.That(model.Name, Is.EqualTo(name));
			Assert.That(model.Email, Is.EqualTo(email));
		}

	}
}
