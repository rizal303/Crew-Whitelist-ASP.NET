using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace CrewWhitelistApps.Repository
{
    public class Configuration
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataAdapter da;
        private DataTable dt;

        public string getConn()
        {
            string strconn = ConfigurationManager.ConnectionStrings["Connection"].ToString();

            return strconn;
        }

        public bool eksekusiQuery(string query, SqlParameter[] p, bool condition)
        {
            conn = new SqlConnection(getConn());
            try
            {
                conn.Open();
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                if (!condition)
                {
                    int count = p.GetUpperBound(0) + 1;

                    for (int i = 0; i < count; i++)

                        cmd.Parameters.AddWithValue(p[i].ParameterName, p[i].Value);

                   int affect = cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public SqlDataAdapter viewTable(string table)
        {
            dt = new DataTable(table);
            da.Fill(dt);
            return da;
        }
    }
}