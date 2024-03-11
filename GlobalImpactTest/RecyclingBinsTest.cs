﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalImpact.Controllers;
using GlobalImpact.Data;
using GlobalImpact.Models;
using GlobalImpact.Utils;
using GlobalImpact.ViewModels.Account;
using GlobalImpactTestProject;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace GlobalImpactTest
{
	public class RecyclingBinsTest
	{
		private Mock<FakeUserManager> userManagerMock;
		private ApplicationDbContext dbContext;
		private RecyclingBinsController controller;

		public RecyclingBinsTest ()
		{
			var options = new DbContextOptionsBuilder<ApplicationDbContext>()
				.UseInMemoryDatabase(databaseName: "GlobalIMPBD2")
				.Options;

			dbContext = new ApplicationDbContext(options);

			var users = new List<AppUser>
			{
				new AppUser
				{
					UniqueCode = Guid.NewGuid().ToString(),
					FirstName = "Test",
					LastName = "User",
					Age = 20,
					Points = 0,
					Email = "234@gmail.com",
					NIF = 123456789
				},
				new AppUser
				{
					UniqueCode = Guid.NewGuid().ToString(),
					FirstName = "Test2",
					LastName = "User2",
					Age = 30,
					Points = 0,
					Email = "123@gmail.com",
					NIF = 987654321
				},
				new AppUser()
				{
					UniqueCode = Guid.NewGuid().ToString(),
					FirstName = "Test3",
					LastName = "User3",
					Age = 40,
					Points = 0,
					Email = "345@gmail.com",
					NIF = 987654321
				}
			}.AsQueryable();

			userManagerMock = new Mock<FakeUserManager>();
			userManagerMock.Setup(x => x.Users)
				.Returns(users);
			userManagerMock.Setup(x => x.DeleteAsync(It.IsAny<AppUser>()))
				.ReturnsAsync(IdentityResult.Success);
			userManagerMock.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
				.ReturnsAsync(IdentityResult.Success);
			userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<AppUser>()))
				.ReturnsAsync(IdentityResult.Success);
			userManagerMock.Setup(x =>
					x.ChangeEmailAsync(It.IsAny<AppUser>(), It.IsAny<string>(), It.IsAny<string>()))
				.ReturnsAsync(IdentityResult.Success);

			controller = new RecyclingBinsController(dbContext, userManagerMock.Object);
			userManagerMock.Setup(u => u.GetUserAsync(controller.User)).ReturnsAsync(users.First());
		}

		[Fact]
		public async void RecyclingBins_CanGetCreatePage()
		{
			string option = null;
			var result =  controller.Create(option);
			var redirectResult = Assert.IsType<ViewResult>(result);
			var model = Assert.IsAssignableFrom<RecyclingBin>(redirectResult.ViewData.Model);
			Assert.NotNull(model);
		}

		[Fact]
		public async void RecyclingBins_CanCreateEco()
		{
			string option = null;
			RecyclingBin recyclingBin = new RecyclingBin()
			{
				Id = Guid.NewGuid(),
				Type = "plastic",
				Latitude = 38.1,
				Longitude = -9.01,
				Description = "BLABLA",
				Capacity = 500,
				CurrentCapacity = 0,
				Status = true,
				RecyclingBinTypeId = "7c09a9ac-39ba-42ae-a25e-f5b4057c38b9"
			};

			var result = controller.Create(recyclingBin);

			var redirectResult = Assert.IsType<RedirectToActionResult>(result.Result);
			Assert.Equal("Index", redirectResult.ActionName);
			//Assert.Equal("RecyclingBins", );
		}
	}

	
}
