using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiTutorialHE.UtilsService.Interface
{
    public interface ISharingDapper
    {
        DbConnection GetDbconnection();//Trả về đối tượng đại diện cho kết nối

        T QuerySingle<T>(string sp, object parms = null);//Thực hiện truy vấn trả về một đối tượng duy nhất

        List<T> Query<T>(string sp, object parms = null);//Trả về một danh sách

        Task<T> QuerySingleAsync<T>(string sp, object parms = null);//Trả về đối tượng duy nhất bất đồng bộ

        Task<List<T>> QueryAsync<T>(string sp, object parms = null);//Trả về một danh sách bất đồng bộ

        int Execute(string sp, object parms = null);//Không trả về giá trị

        T Insert<T>(string sp, object parms = null);//Chèn 1 bảng ghi dữ liệu mới 

        T Update<T>(string sp, object parms = null);//Update một bảng dữ liệu đã có
    }
}
