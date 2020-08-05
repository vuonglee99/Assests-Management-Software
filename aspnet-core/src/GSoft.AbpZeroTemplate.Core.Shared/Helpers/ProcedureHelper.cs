using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using GSoft.AbpZeroTemplate.Helpers.Dto;
using System.Linq;

namespace GSoft.AbpZeroTemplate.Helpers
{
    public interface IProcedureHelper
    {
        List<T> GetData<T>(string procedureName, object parameter);
        DataSet DataFromStore(string storedProcName, object parameter);
    }

    public class ProcedureHelper : IProcedureHelper
    {
        private readonly string connectionString;
        public ProcedureHelper()
        {
            connectionString = "Data Source=k21.phanmemquanlytaisan.com;Initial Catalog=db_uit_k21;Persist Security Info=True;User ID=k21;Password=4198Vq/M";
            //connectionString = "Data Source=DESKTOP-ACOOQ0G\\SQLEXPRESS;Initial Catalog=DbPratice;Integrated Security=True";
        }

        private List<ProcedureParamInfo> GetParamInfos(IDbConnection conn, string procedureName)
        {
            var rr = conn.Query<ProcedureParamInfo>($"select PARAMETER_NAME, PARAMETER_MODE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH from information_schema.parameters where specific_name = '{procedureName}'");
            return rr.ToList();
        }

        public List<T> GetData<T>(string procedureName, object parameter)
        {
            List<T> result;

            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var paramsInfo = GetParamInfos(con, procedureName);

                DynamicParameters parameters = new DynamicParameters();

                var properties = parameter.GetType().GetProperties();

                foreach (var param in paramsInfo)
                {
                    var property = properties
                                    .Where(x => x.Name.ToLower() == param.PARAMETER_NAME.Replace("@", "").ToLower())
                                    .FirstOrDefault();
                    if (property == null)
                    {
                        continue;
                    }

                    parameters.Add(param.PARAMETER_NAME, property.GetValue(parameter));
                }

                result = con.Query<T>(procedureName, parameters, null, true, null, System.Data.CommandType.StoredProcedure).ToList();

            }

            return result;
        }
        public DataSet DataFromStore(string storedProcName, object parameter)
        {
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                var paramsInfo = GetParamInfos(con, storedProcName);

                DynamicParameters parameters = new DynamicParameters();

                var properties = parameter.GetType().GetProperties();

                foreach (var param in paramsInfo)
                {
                    var property = properties
                                    .Where(x => x.Name.ToLower() == param.PARAMETER_NAME.Replace("@", "").ToLower())
                                    .FirstOrDefault();
                    if (property == null)
                    {
                        continue;
                    }
                    parameters.Add(param.PARAMETER_NAME, property.GetValue(parameter));
                }

                var da = new SqlDataAdapter(storedProcName, (SqlConnection)con);
                var ds = new DataSet();

                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                foreach (var item in parameters.ParameterNames)
                {
                    da.SelectCommand.Parameters.Add(new SqlParameter(item, parameters.Get<object>(item)));
                }
                da.Fill(ds);

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    ds.Tables[i].TableName = "table" + i;
                }

                return ds;

            }
        }
    }
}
