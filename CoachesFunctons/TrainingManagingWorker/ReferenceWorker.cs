﻿using System;
using System.Collections.Generic;
using System.Text;
using InformationService.Interfaces;
using InformationService.Models;
using InterfaceModels;

namespace TrainingManagingWorker
{
    public class ReferenceWorker
    {

        private IReferenceRepository _referenceRepository_;

        public ReferenceWorker(IReferenceRepository referenceRepository)
        {
            _referenceRepository_ = referenceRepository;
        }

        public SportLocationDto PrepareSportsDataForClient(Programs location)
        {
            SportLocationDto dto = new SportLocationDto
            {
                Email = location.SportNavigation.Email,
                SportName = location.SportNavigation.Name,
                SportId = location.SportNavigation.Id,
                ProgramId = location.Id,
                ProgramName = location.Name
            };
            return dto;
        }
    }
}
