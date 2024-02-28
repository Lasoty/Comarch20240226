using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMICalculator.Model.Data;
using BMICalculator.Model.DTO;
using BMICalculator.Model.Model;
using BMICalculator.Model.Repositories;
using BMICalculator.Services;
using BMICalculator.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;

namespace BMICalculator.Integration.Tests
{
    public class MeasurementFeatureTests
    {
        private ApplicationDbContext dbContext;
        private IResultRepository resultRepository;
        private IBmiCalculatorFacade bmiCalculatorFacade;

        [SetUp]
        public void Setup()
        {
            DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("BmiDb")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            dbContext = new ApplicationDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            resultRepository = new ResultRepository(dbContext);

            var bmiDeterminatorMock = new Mock<IBmiDeterminator>();
            var bmiCalculatorFactoryMock = new Mock<IBmiCalculatorFactory>();

            bmiCalculatorFacade = new BmiCalculatorFacade(bmiDeterminatorMock.Object, bmiCalculatorFactoryMock.Object,
                resultRepository);
        }

        [Test]
        public async Task SaveResult_Should_SaveBmiRecordInDb()
        {
            //Arrange 

            BmiMeasurement bmiRecord = new BmiMeasurement()
            {
                Bmi = 15,
                BmiClassification = BmiClassification.Normal,
                Summary = "Jest OK"
            };

            // Act 
            bool result = await bmiCalculatorFacade.SaveResult(bmiRecord);

            // Assert
            Assert.IsTrue(result);
            Assert.IsTrue(dbContext.BmiMeasurements.Any(x => x.Id == bmiRecord.Id && x.BmiClassification == BmiClassification.Normal));
        }
    }
}
