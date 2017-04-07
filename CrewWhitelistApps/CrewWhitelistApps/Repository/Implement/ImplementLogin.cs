using CrewWhitelistApps.Repository.Interface;
using CrewWhitelistApps.Models;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CrewWhitelistApps.Repository.Implement
{
    public class ImplementLogin : InterfaceLogin
    {
        private Configuration config;
        private DataTable dt;
        private string query;
        private string tb;
        private bool stt;

        public ImplementLogin()
        {
            config = new Configuration();
            config.getConn();
        }

        public bool isValid(AdministartorModel obj)
        {
            query = "ProcLogin";
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@user", obj.username),
                new SqlParameter("@pass", obj.password),
                new SqlParameter("@role", obj.role)
            };
            tb = "administrator";
            dt = new DataTable();

            bool condition = config.eksekusiQuery(query, param, false);
            if (condition)
            {
                config.viewTable(tb).Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    stt = true;              
                }
                else
                {
                    stt = false;
                }
            }
            return stt;
        }

        public IEnumerable<string> GetAllRoles()
        {
            return new List<string>
            {
                "admin",
                "admin whitelist",
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
    }
}