﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InformationService.DataModels;
using InformationService.Interfaces;
using InformationService.Models;
using InterfaceModels;

namespace TrainingManagingWorker
{
    public class RegistrantWorker
    {
        private ITrainingRepository _trainingRepository;
        private List<Athletes> _athletes;
        private IOrganizationRepository _organizationRepository;
        private IRefRepository _refRepository;

        public RegistrantWorker(ITrainingRepository trainingRepository, IOrganizationRepository organizationRepository)
        {
            _trainingRepository = trainingRepository;
            _organizationRepository = organizationRepository;
            var athletes = _organizationRepository.GetAllAthletes();
            _athletes = athletes.Result;
        }

        public RegistrantWorker(ITrainingRepository trainingRepository, IRefRepository refRepository)
        {
            _trainingRepository = trainingRepository;
            _refRepository = refRepository;
        }

        public async Task<List<RegistrantDto>> GetRegistrantsForSport(int sportId)
        {
            List<RegistrantDto> registrantsList = new List<RegistrantDto>();

            var registrants = await _trainingRepository.GetRegistrantsBySport(sportId);
            foreach (var registrant in registrants)
            {
                registrantsList.Add(PrepareRegistrantDataForClient(registrant));
            }
            return registrantsList;
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
                var selectedAthlete = _athletes.FirstOrDefault(a => a.Id == athlete.AthletesId);
                if (selectedAthlete != null)
                {
                    dto.BirthDate = selectedAthlete.BirthDate;
                    dto.MedicalExpirationDate = selectedAthlete.MedicalExpirationDate;
                    dto.Gender = selectedAthlete.MF;
                }
            }

            PreparePhones(registrant, dto);
            PrepareEmail(registrant, dto);

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

        private static void PrepareEmail(Registrant registrant, RegistrantDto dto)
        {
            var emailList = registrant.RegistrantEmail.ToList();
            foreach (var email in emailList)
            {
                if (dto.RegistrantEmail1Id == 0)
                {
                    dto.Email1 = email.Email;
                    dto.RegistrantEmail1Id = email.Id;
                }
                else
                {
                    if (dto.RegistrantEmail2Id == 0)
                    {
                        dto.Email2 = email.Email;
                        dto.RegistrantEmail2Id = email.Id;
                    }
                    else
                    {
                        dto.Email3 = email.Email;
                        dto.RegistrantEmail3Id = email.Id;
                    }
                }
            }
        }

        public async Task<NotificationEntity> AddNumberForEvent(EventTextDto dto)
        {
            var entity = await _refRepository.GetEventByName(dto.Message);
            if (entity != null)
            {
                EventInformation eventInformation = new EventInformation
                {
                    Message = dto.Message,
                    From = dto.From,
                    ProgramId = entity.ProgramId,
                    City = dto.City,
                    Zip = dto.Zip,
                    SportId = entity.SportId
                };
                await _trainingRepository.AddEvent(eventInformation);
            }
            else
            {
                entity = new NotificationEntity { Message = "You text " + dto.Message };
            }
            return entity;
        }
    }
}


