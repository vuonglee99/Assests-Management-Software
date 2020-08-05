using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Dapper;
using Selenium.Test.Helper.DTO;
namespace Selenium.Test.Helper
{
    class DataProvider
    {
        private static DataProvider instance;

        private DataProvider() { }
        internal static DataProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataProvider();
                }
                return instance;
            }

            private set => instance = value;
        }//Cap Instance

        string conection = "Data Source=k21.phanmemquanlytaisan.com;Initial Catalog = db_uit_k21; Persist Security Info=True;User ID = k21; Password=4198Vq/M";




        private List<ProcedureParamInfo> GetParamInfos(IDbConnection conn, string procedureName)
        {
            var rr = conn.Query<ProcedureParamInfo>($"select PARAMETER_NAME, PARAMETER_MODE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH from information_schema.parameters where specific_name = '{procedureName}'");
            return rr.ToList();
        }

        public List<T> GetData<T>(string procedureName, object parameter)
        {
            List<T> result;

            using (IDbConnection con = new SqlConnection(conection))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var paramsInfo = GetParamInfos(con, procedureName);

                DynamicParameters parameters = new DynamicParameters();

                var properties = parameter.GetType().GetProperties();

                foreach (var param in paramsInfo)
                {
                    var property = properties // nsx_code == nsx_code
                                    .Where(x => x.Name.ToLower() == param.PARAMETER_NAME.Replace("@", "").ToLower())
                                    .FirstOrDefault();
                    if (property == null)
                    {
                        continue;
                    }
                    var debug1 = property.GetValue(parameter);
                    parameters.Add(param.PARAMETER_NAME, property.GetValue(parameter));
                }

                result = con.Query<T>(procedureName, parameters, null, true, null, System.Data.CommandType.StoredProcedure).ToList();
            }

            return result;
        }
    }
}
