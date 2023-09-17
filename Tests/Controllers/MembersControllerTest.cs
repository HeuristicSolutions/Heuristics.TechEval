using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Heuristics.TechEval.Core;
using Heuristics.TechEval.Core.Models;
using Heuristics.TechEval.Core.Repositories;
using Heuristics.TechEval.Web.Controllers;
using Heuristics.TechEval.Web.Models;
using Heuristics.TechEval.Web.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rhino;
using Rhino.Mocks;
using Rhino.Mocks.Expectations;

namespace Heuristics.TechEval.Tests.Controllers {

	[TestClass]
	public class MembersControllerTest {

        [TestMethod]
		public void AddNewMember_DuplicateEmails_ReturnError() {
            // Arrange
            var testMember = new Member()
            {
                Id = 1,
                Name = "Test",
                Email = "test@gmail.com",
                CategoryId = 1,
            };

            var mockMemberRepository = MockRepository.GenerateStub<IMemberRepository>();
            var mockCategoryRepository = MockRepository.GenerateStub<ICategoryRepository>();
            var mockResponseService = MockRepository.GenerateStub<IResponseService>();
            mockMemberRepository.Stub(mr => mr.AddMember(testMember)).Return(testMember);
            mockMemberRepository.Stub(mr => mr.IsExistingEmail(testMember.Email)).Return(true);
            mockResponseService.Stub(rs => rs.SetStatusCode(Arg<HttpResponseBase>.Is.Anything, Arg<int>.Is.Anything));

            MembersController controller = new MembersController(mockMemberRepository, mockCategoryRepository, mockResponseService);

            var testNewMember = new NewMember()
            {
                Name = "Test",
                Email = "test@gmail.com",
                CategoryId = 1,
            };

            // Act
            var res = controller.New(testNewMember);
            var json = JsonConvert.SerializeObject(res);
            var parsedJObject = JObject.Parse(json);
            var data = parsedJObject["Data"];
            var key = (string)data[0]["Key"];
            var message = (string)data[0]["Message"];

            // Assert
            Assert.IsNotNull(res);
            Assert.AreEqual("Email", key);
            Assert.AreEqual("This email is already in the system.", message);
        }

        [TestMethod]
        public void EditNewMember_DuplicateEmails_ReturnError()
        {
            // Arrange
            var testMember = new Member()
            {
                Id = 1,
                Name = "Test",
                Email = "test@gmail.com",
                CategoryId = 1,
            };

            var testModel = new EditMember()
            {
                Id = 1,
                Name = "Test",
                Email = "test@hotmail.com",
                CategoryId = 1,
            };

            var mockMemberRepository = MockRepository.GenerateStub<IMemberRepository>();
            var mockCategoryRepository = MockRepository.GenerateStub<ICategoryRepository>();
            var mockResponseService = MockRepository.GenerateStub<IResponseService>();
            mockMemberRepository.Stub(mr => mr.GetMember(testModel.Id)).Return(testMember);
            mockMemberRepository.Stub(mr => mr.IsExistingEmail(testModel.Email)).Return(true);
            mockResponseService.Stub(rs => rs.SetStatusCode(Arg<HttpResponseBase>.Is.Anything, Arg<int>.Is.Anything));

            MembersController controller = new MembersController(mockMemberRepository, mockCategoryRepository, mockResponseService);

            // Act
            var res = controller.Edit(testModel);
            var json = JsonConvert.SerializeObject(res);
            var parsedJObject = JObject.Parse(json);
            var data = parsedJObject["Data"];
            var key = (string)data[0]["Key"];
            var message = (string)data[0]["Message"];

            // Assert
            Assert.IsNotNull(res);
            Assert.AreEqual("Email", key);
            Assert.AreEqual("This email is already in the system.", message);
        }

    }
}
