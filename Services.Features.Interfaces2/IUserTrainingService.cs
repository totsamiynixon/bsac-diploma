
using Services.Features.DTO.UserTraining;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Features.Interfaces
{
    public interface IUserTrainingService
    {
        Task<UserTrainingDTO> GenerateAndGetUserTrainingAsync(int userId);
        //Task<UserTrainingDTO> GetUserTraining(int trainingId);
        Task CompleteTraining(int userId, int trainingId);
    }
}
