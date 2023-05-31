using MySqlConnector;
using System.Data.Common;
using System.Data;
using WebApiTutorialHE.UtilsService.Interface;
using Dapper;

namespace WebApiTutorialHE.UtilsService
{
    public class SharingDapper:ISharingDapper
    {
        private readonly IConfiguration _config;
        private string Connectionstring = "SharingConnection";

        public SharingDapper(IConfiguration config)
        {
            _config = config;
        }

        public DbConnection GetDbconnection()//Nhận kết nối cơ sở dữ liệu và trả về BDConnect
        {
            return new MySqlConnection(_config.GetConnectionString(Connectionstring));
        }

        public void Dispose()
        {

        }

        public int Execute(string sp, object parms)
        {
            throw new NotImplementedException();
        }

        public T QuerySingle<T>(string sp, object parms = null)
        {
            using IDbConnection db = new MySqlConnection(_config.GetConnectionString(Connectionstring));
            return db.QueryFirstOrDefault<T>(sp, parms);
        }

        public List<T> Query<T>(string sp, object parms = null)
        {
            using IDbConnection db = new MySqlConnection(_config.GetConnectionString(Connectionstring));
            return db.Query<T>(sp, parms).ToList();
        }

        public async Task<T> QuerySingleAsync<T>(string sp, object parms = null)
        {
            using IDbConnection db = new MySqlConnection(_config.GetConnectionString(Connectionstring));
            return await db.QueryFirstOrDefaultAsync<T>(sp, parms);
        }

        public async Task<List<T>> QueryAsync<T>(string sp, object parms = null)
        {
            using IDbConnection db = new MySqlConnection(_config.GetConnectionString(Connectionstring));
            return (await db.QueryAsync<T>(sp, parms)).ToList();
        }

        public T Insert<T>(string sp, object parms = null)
        {
            T result;
            using IDbConnection db = new MySqlConnection(_config.GetConnectionString(Connectionstring));

            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();

                try
                {
                    result = db.Query<T>(sp, parms, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }

        public T Update<T>(string sp, object parms = null)
        {
            T result;
            using IDbConnection db = new MySqlConnection(_config.GetConnectionString(Connectionstring));

            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();

                using var tran = db.BeginTransaction();
                try
                {
                    result = db.Query<T>(sp, parms, transaction: tran).FirstOrDefault();
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return result;
        }
    }
}
