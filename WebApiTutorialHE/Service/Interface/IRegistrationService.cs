﻿using System.Threading.Tasks;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Service.Interface
{
    public interface IRegistrationService
    {
        Task<List<RegistationListModel>> GetListRegistation(int id);
        Task<ObjectResponse> UpdateRegistation(RegistationUpdateModel registationUpdate);
        Task<string> DeleteRegistation(int id);
        Task<Registration> CreateRegistation(RegistationPostModel registationPost, ForceInfo forceInfo);
        Task<List<Registration>>UpdateStatus(UpdateStatus updateStatus);
        Task<int> NumRegistation(int postId, int createdBy);
        Task<List<RegistrationProserModel>> GetListRegistrationHaveProposer(int postId);
    }
}
