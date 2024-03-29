﻿using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Action.Interface
{
    public interface IRegistationAction
    {
        Task<bool> UpdateRegistration(RegistationUpdateModel registationUpdate);
        Task<string> DeleteRegistration(int id);
        Task<Registration> CreateRegistration(RegistationPostModel registationPost, ForceInfo forceInfo);
        Task<List<Registration>> UpdateRegistrationStatus(UpdateStatus updateStatus);
        
    }
}
