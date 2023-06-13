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

        public async Task<string>DeleteRegistration(RegistationListModel registationDelete)
        {
            var delete= await _sharingContext.Registrations.FindAsync(registationDelete.Id);
            delete.IsDeleted = true;
            await _sharingContext.SaveChangesAsync();
            return "Đã xóa";
            
        }


    }
}
