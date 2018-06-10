using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Data.Implementations.Context;
using Data.Interfaces;
using Entity.Domain.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Features.Implementations2;
using Services.Features.Interfaces;

namespace Services.Features.Tests
{
    [TestClass]
    public class UserTrainingTest
    {
        private DataContext _dbContext;
        private IUnitOfWork _unitOfWork;
        private IUserTrainingService _userTrainingService;

        [TestInitialize]
        public void InitBeforeEachTest()
        {
            _dbContext = new DataContext();
            _unitOfWork = new UnitOfWork(_dbContext);
            _userTrainingService = new UserTrainingService(_unitOfWork);
        } 

        [TestMethod]
        public async Task CreateUserTrainingTestAsync()
        {
            var user = await _dbContext.Set<User>().FirstOrDefaultAsync(s => s.Settings.ProfessionId.HasValue);
            if(user == null)
            {
               Assert.Inconclusive();
               return;
            }
            var userTraining = await _userTrainingService.GenerateAndGetUserTrainingAsync(user.Id);
            Assert.IsTrue(userTraining.Exercises != null && userTraining.Exercises.Any());
        }
    }
}
