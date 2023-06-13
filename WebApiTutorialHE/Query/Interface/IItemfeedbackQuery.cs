using WebApiTutorialHE.Models.Itemfeedback;

namespace WebApiTutorialHE.Query.Interface
{
    public interface IItemfeedbackQuery
    {
        public Task<List<ReceivedListModel>> GetItemfeedback();
    }
}
