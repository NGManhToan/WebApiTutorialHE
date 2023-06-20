using Microsoft.EntityFrameworkCore;
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
            var accepted = await _sharingContext.Registrations.FindAsync(updateStatus.Id);

            if (accepted != null)
            {
                var category = await _sharingContext.Posts.Where(x => x.Id == accepted.PostId).Select(x => x.CategoryId).FirstOrDefaultAsync();
                var update = _sharingContext.Registrations.Select(x => x).Where(x => x.PostId == accepted.PostId);

                if (category == 2)
                {
                    {
                        accepted.Status = "Accepted";
                        _sharingContext.Registrations.Update(accepted);
                    }
                }
                else
                {
                    foreach (var registration in update)
                    {
                        if (registration.Id == accepted.Id)
                        {
                            registration.Status = "Accepted";
                        }
                        else
                        {
                            registration.Status = "Disapproved";
                        }
                    }

                    _sharingContext.Registrations.UpdateRange(update);
                }

                await _sharingContext.SaveChangesAsync();
                return update.ToList();
            }

            return null;
        }
        
    }
}
