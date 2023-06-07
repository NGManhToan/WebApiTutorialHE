using System.Data;
using WebApiTutorialHE.Action.Interface;
using WebApiTutorialHE.Database;
using WebApiTutorialHE.Database.SharingModels;
using WebApiTutorialHE.Models.Account;
using WebApiTutorialHE.Query.Interface;
using WebApiTutorialHE.Service.Interface;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace WebApiTutorialHE.Service
{
    public class AccountService:IAccountService
    {
        private readonly IAccountQuery _accountQuery;
        private readonly IAccountAction _accountAction;
        private readonly SharingContext _sharingContext;
        public AccountService(IAccountQuery accountQuery,IAccountAction accountAction,SharingContext sharingContext)
        {
            _accountQuery = accountQuery;
            _accountAction = accountAction;
            _sharingContext = sharingContext;
        }
        public async Task<List<AccountListModel>> GetAccountListModels()
        {
            return await _accountQuery.QueryListAccount();
        }
        public async Task<Account> PutAccountUpdateModel(AccountUpdateModel model)
        {
            return await _accountAction.AccountUpdateModels(model);
        }
        public async Task<Account> PostAccountModel(AccountListModel model)
        {
            return await _accountAction.ActionCreateAccount(model);
        }
        public async Task<string>DeleteAccountModel(int id)
        {
            return await _accountAction.ActionDeleteAccount(id);
        }
        public async Task<Account> FillterAccountModel(int id/*,string email*/)
        {
            return await _accountAction.ActionFillterAccount(id/*,email*/);
        }
        public async Task<List<AccountListModel>> FindAccountModel(string search)
        {
            return await _accountQuery.QueryFindAccount(search);
        }

        //Lấy thông tin admin hiện tại
        public async Task<List<AccountListModel>> GetAccountListAdminModel()
        {
            return await _accountQuery.QueryListAdminAccount();
        }
        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.TableName = "Empdata";
            dt.Columns.Add("account_id", typeof(int));
            dt.Columns.Add("email", typeof(string));
            dt.Columns.Add("password", typeof(string));
            dt.Columns.Add("type", typeof(int));
            var _list = this._sharingContext.Accounts.ToList();
            if (_list.Count > 0)
            {
                _list.ForEach(item =>
                {
                    dt.Rows.Add(item.AccountId, item.Email, item.Password, item.Type);
                });
            }
            return dt;
        }

        public void ExportDataTableToPdf(DataTable dataTable, string filePath)
        {
            // Tạo một tài liệu PDF mới
            Document document = new Document();

            // Tạo một writer để ghi dữ liệu vào tài liệu
            PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.CreateNew, FileAccess.Write));


            // Mở tài liệu để bắt đầu viết
            document.Open();

            // Tạo một bảng để hiển thị dữ liệu
            PdfPTable table = new PdfPTable(dataTable.Columns.Count);

            // Đặt tiêu đề cho các cột
            for (int i = 0; i < dataTable.Columns.Count; i++)
            {
                table.AddCell(new Phrase(dataTable.Columns[i].ColumnName));
            }

            // Đặt dữ liệu cho từng dòng
            for (int rowIndex = 0; rowIndex < dataTable.Rows.Count; rowIndex++)
            {
                for (int colIndex = 0; colIndex < dataTable.Columns.Count; colIndex++)
                {
                    table.AddCell(new Phrase(dataTable.Rows[rowIndex][colIndex].ToString()));
                }
            }

            // Thêm bảng vào tài liệu
            document.Add(table);

            // Đóng tài liệu
            document.Close();
        }
    }
}
