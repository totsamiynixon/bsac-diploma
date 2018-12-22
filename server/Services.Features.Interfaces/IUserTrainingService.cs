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
        Task<UserTrainingGroupedDTO> GenerateAndGetUserTrainingAsync(int userId);
        Task<UserTrainingDTO> GetLastUserTrainingAsync(int userId);
        //Task<UserTrainingDTO> GetUserTraining(int trainingId);
        Task CompleteTrainingAsync(int userId, int trainingId);
        Task UpdateTrainingRatingAsync(int userId, int trainingId, int rating);
    }
}