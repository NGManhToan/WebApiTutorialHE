using System.Linq;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Registation;
using WebApiTutorialHE.Models.UtilsProject;

namespace WebApiTutorialHE.Action
{
    public class RegistationAction:IRegistationAction
    {
        private readonly SharingContext _sharingContext;
        public RegistationAction(SharingContext sharingContext)
        {
            _sharingContext = sharingContext;
        }

        public async Task<Registration> updateRegistration(RegistationUpdateModel registationUpdate)
        {
            var update = await _sharingContext.Registrations.FindAsync(registationUpdate.Id);
            if (update != null)
            {
                update.Content = registationUpdate.Content;
                update.LastModifiedDate = Utils.DateNow();
                update.LastModifiedBy = registationUpdate.Userid;

                _sharingContext.Registrations.Update(update);
                await _sharingContext.SaveChangesAsync();
            }

            return update;
        }

        public async Task<string>DeleteRegistration(int id)
        {
            var delete= await _sharingContext.Registrations.FindAsync(id);
            delete.IsDeleted = true;
            await _sharingContext.SaveChangesAsync();
            return "Đã xóa";
            
        }

        public async Task<Registration>CreateRegistration(RegistationPostModel registationPost)
        {
            //var update = await _sharingContext.Registrations.FindAsync(registation);
            var registration = new Registration()
            {
                Content = registationPost.Content,
                PostId= registationPost.PostId,
                LastModifiedDate = Utils.DateNow(),
                CreatedBy=registationPost.CreatedBy,
                LastModifiedBy = registationPost.CreatedBy
            };
            _sharingContext.Add(registration);
                await _sharingContext.SaveChangesAsync(); 
            return registration;       
        }
        public async Task<List<Registration>> UpdateRegistrationStatus(UpdateStatus updateStatus)
        {
            var update = _sharingContext.Registrations.Select(x => x).Where(x => x.PostId == updateStatus.PostId);

            foreach (var registration in update)
            {
                if (registration.Id == updateStatus.Id)
                {
                    registration.Status = "Accepted";
                }
                else
                {
                    registration.Status = "Disapproved";
                }
            }

            _sharingContext.Registrations.UpdateRange(update);
            await _sharingContext.SaveChangesAsync();
            return update.ToList();
        }
        public async Task<List<Registration>> EdocRegistrationStatus(List<EdocUpdateStatus> updateStatusList)
        {
            var edocPostIds = updateStatusList.Where(u => u.CategoryId == 2).Select(u => u.PostId).Distinct().ToList();
            var registrationsToUpdate = _sharingContext.Registrations.Where(r => edocPostIds.Contains(r.PostId)).ToList();
            foreach (var updateStatus in updateStatusList)
            {
                if (updateStatus.CategoryId == 2)
                {
                    var registrations = registrationsToUpdate.Where(r => r.PostId == updateStatus.PostId);

                    foreach (var registration in registrations)
                    {
                        if (registration.Id == updateStatus.Id)
                        {
                            registration.Status = "Accepted";
                        }
                        
                    }
                }
            }
            _sharingContext.Registrations.UpdateRange(registrationsToUpdate);
            await _sharingContext.SaveChangesAsync();
            return registrationsToUpdate;
        }
    }
}
