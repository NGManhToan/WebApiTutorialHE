using WebApiTutorialHE.Models.Registation;

namespace WebApiTutorialHE.Query.Interface
{
    public interface IRegistrationQuery
    {
        Task<List<RegistationListModel>>QueryGetListRegistation(int id);
    }
}
