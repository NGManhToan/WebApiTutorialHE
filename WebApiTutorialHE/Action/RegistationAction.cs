using System.Linq;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Models.Registation;

namespace WebApiTutorialHE.Action
{
    public class RegistationAction:IRegistationAction
    {
        private readonly SharingContext _sharingContext;
        public RegistationAction(SharingContext sharingContext)
        {
            _sharingContext = sharingContext;
        }
        
    }
}
