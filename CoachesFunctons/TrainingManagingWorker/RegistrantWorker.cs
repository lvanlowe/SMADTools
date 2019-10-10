﻿using System;
using System.Linq;
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
            dto.LastName = registrant.LastName;
            dto.NickName = registrant.NickName;
            dto.Id = registrant.Id;
            dto.SportId = registrant.SportId;
            dto.ProgramId = registrant.ProgramId;
            dto.SportTypeId = registrant.SportTypeId;
            dto.Size = registrant.Size;
            dto.IsVolunteer = registrant.IsVolunteer;
            dto.Selected = registrant.Selected;
            dto.TeamId = registrant.TeamId;
            var athlete = registrant.RegisteredAthlete.FirstOrDefault();
            if (athlete != null)
            {
                dto.RegisteredAthletesId = athlete.Id;
                dto.AthletesId = athlete.AthletesId;
            }

            PreparePhones(registrant, dto);

            return dto;
        }

        private static void PreparePhones(Registrant registrant, RegistrantDto dto)
        {
            var phoneList = registrant.RegistrantPhone.ToList();
            foreach (var phone in phoneList)
            {
                if (dto.RegistrantPhone1Id == 0)
                {
                    dto.Phone1 = phone.Phone;
                    dto.CanText1 = phone.CanText;
                    dto.PhoneType1 = phone.PhoneType;
                    dto.RegistrantPhone1Id = phone.Id;
                }
                else
                {
                    if (dto.RegistrantPhone2Id == 0)
                    {
                        dto.Phone2 = phone.Phone;
                        dto.CanText2 = phone.CanText;
                        dto.PhoneType2 = phone.PhoneType;
                        dto.RegistrantPhone2Id = phone.Id;
                    }
                    else
                    {
                        dto.Phone3 = phone.Phone;
                        dto.CanText3 = phone.CanText;
                        dto.PhoneType3 = phone.PhoneType;
                        dto.RegistrantPhone3Id = phone.Id;
                    }
                }
            }
        }
    }
}

