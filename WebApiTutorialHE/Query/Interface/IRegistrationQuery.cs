using WebApiTutorialHE.Models.Registation;

namespace WebApiTutorialHE.Query.Interface
{
    public interface IRegistrationQuery
    {
        Task<List<RegistationListModel>>QueryGetListRegistation(int id);
        Task <int>QueryNumRegistation(int id,int createdBy);
        Task<List<RegistrationProserModel>> GetListRegistrationHaveProposer(int PostId,int id);
    }
}
