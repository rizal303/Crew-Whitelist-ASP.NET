using CrewWhitelistApps.Repository.Interface;
using CrewWhitelistApps.Models;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;
using System.Web.Mvc;

namespace CrewWhitelistApps.Repository.Implement
{
    public class ImplementCrew : InterfaceCrew
    {
        private Configuration config;
        private SqlConnection con;
        private SqlParameter[] dbParams;
        private string query;
        private string tb;
        private bool isValid;

        public ImplementCrew()
        {
            config = new Configuration();
            con =  new SqlConnection(config.getConn());
        }

        public bool save(CrewModel o)
        {
            query = "ProcInsertCrew";
            dbParams = new SqlParameter[]
            {
                new SqlParameter("@name", o.name),
                new SqlParameter("@status", o.status),
                new SqlParameter("@airport", o.airport),
                new SqlParameter("@company", o.companyairways)
            };

            isValid = config.eksekusiQuery(query, dbParams, false);

            return isValid;
        }

        public bool edit(CrewModel o)
        {
            query = "ProcUpdateCrew";
            dbParams = new SqlParameter[]
            {
                new SqlParameter("id", o.idcrew),
                new SqlParameter("@name", o.name),
                new SqlParameter("@status", o.status),
                new SqlParameter("@airport", o.airport),
                new SqlParameter("@company", o.companyairways)
            };

            isValid = config.eksekusiQuery(query, dbParams, false);

            return isValid;
        }

        public bool delete(CrewModel o)
        {
            query = "ProcDeleteCrew";
            dbParams = new SqlParameter[]
            {
                new SqlParameter("@id", o.idcrew)
            };

            isValid = config.eksekusiQuery(query, dbParams, false);

            return isValid;
        }

        public IEnumerable<string> GetAllStatus()
        {
            return new List<string>
            {
                "Pilot",
                "Pramugari",
            };
        }

        public IEnumerable<SelectListItem> GetSelectListItems(IEnumerable<string> elements)
        {
            var selectList = new List<SelectListItem>();

            foreach (var element in elements)
            {
                selectList.Add(new SelectListItem
                {
                    Value = element,
                    Text = element
                });
            }

            return selectList;
        }

        public List<CrewModel> getAllCrew()
        {
            List<CrewModel> list = new List<CrewModel>();
            SqlCommand com = new SqlCommand("ProcGetAllCrew", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            list = (from DataRow dr in dt.Rows
                       select new CrewModel()
                       {
                           idcrew = Convert.ToString(dr["id_crew"]),
                           name = Convert.ToString(dr["name"]),
                           datelist = Convert.ToString(dr["date_list"]),
                           status = Convert.ToString(dr["status"]),
                           airport = Convert.ToString(dr["airport"]),
                           companyairways = Convert.ToString(dr["company_airways"])
                       }).ToList();

            return list;
        }
    }
}