using System;
using System.Collections.Generic;
using InformationService.Interfaces;
using InformationService.Models;
using InterfaceModels;
using Moq;
using TrainingManagingWorker;
using Xunit;

namespace TrainingManagingWorkerTest
{
    public class RegistrantWorkerTest
    {
        private Mock<ITrainingRepository> _mockTrainingRepository;
        private RegistrantWorker _worker;

        public RegistrantWorkerTest()
        {
            _mockTrainingRepository = new Mock<ITrainingRepository>();
            _worker = new RegistrantWorker(_mockTrainingRepository.Object);
        }

        [Theory]
        [InlineData(1, 1)]
        public void PerpareRegistrantDataForClient_When_executed_return_dto(int sportId, int expected)

        {
            Registrant registrant = new Registrant
            {
                FirstName = "Clark",
                LastName = "Kent",
                NickName = "Superman",
                Id = 1,
                ProgramId = 10,
                SportId = 4,
                Size = "small",
                SportTypeId = 2
            };
            RegistrantDto actual = _worker.PrepareRegistrantDataForClient(registrant);
            Assert.Equal(registrant.FirstName, actual.FirstName);

        }
    }
}
