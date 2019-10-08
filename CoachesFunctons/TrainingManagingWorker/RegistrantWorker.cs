using System;
using InformationService.Interfaces;
using InformationService.Models;
using InterfaceModels;

namespace TrainingManagingWorker
{
    public class RegistrantWorker
    {
        private ITrainingRepository _trainingRepository;

        public RegistrantWorker(ITrainingRepository trainingRepository)
        {
            _trainingRepository = trainingRepository;
        }

        public RegistrantDto PrepareRegistrantDataForClient(Registrant registrant)
        {
            RegistrantDto dto = new RegistrantDto();
            dto.FirstName = registrant.FirstName;
            return dto;
        }
    }
}
