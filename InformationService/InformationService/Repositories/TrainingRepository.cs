using System;
using System.Collections.Generic;
using System.Text;
using InformationService.Interfaces;
using InformationService.Models;

namespace InformationService.Repositories
{
    public class TrainingRepository : ITrainingRepository
    {
        private PwsodbContext _context;

        public TrainingRepository(PwsodbContext context)
        {
            _context = context;
        }

    }
}
