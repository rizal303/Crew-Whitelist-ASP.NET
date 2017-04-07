using CrewWhitelistApps.Repository.Interface;
using CrewWhitelistApps.Models;
using System.Data;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CrewWhitelistApps.Repository.Implement
{
    public class ImplementCrewSchedule : InterfaceCrewSchedule
    {
        private Configuration config;
        private SqlConnection con;
        private DataTable dt;
        private SqlParameter[] dbParams;
        private string query;
        private string tb;
        private bool isValid;

        public ImplementCrewSchedule()
        {
            config = new Configuration();
            con = new SqlConnection(config.getConn());
        }

        public bool save(CrewScheduleModel o)
        {
            query = "ProcInsertCrewSchedule";
            dbParams = new SqlParameter[]
            {
                new SqlParameter("@idcrew", o.idcrew),
                new SqlParameter("@start_date", o.startdateConvrt),
                new SqlParameter("@end_date", o.enddateConvrt)
            };

            isValid = config.eksekusiQuery(query, dbParams, false);

            return isValid;
        }

        public bool edit(CrewScheduleModel o)
        {
            query = "ProcUpdateCrewSchedule";
            dbParams = new SqlParameter[]
            {
                new SqlParameter("@id", o.idcrewschedule),
                new SqlParameter("@start_date", o.startdateConvrt),
                new SqlParameter("@end_date", o.enddateConvrt)
            };

            isValid = config.eksekusiQuery(query, dbParams, false);

            return isValid;
        }

        public bool delete(CrewScheduleModel o)
        {
            query = "ProcDeleteCrewSchedule";
            dbParams = new SqlParameter[]
            {
                new SqlParameter("@id", o.idcrewschedule)
            };

            isValid = config.eksekusiQuery(query, dbParams, false);

            return isValid;
        }

        public List<CrewScheduleModel> getAllCrewSchedule()
        {
            List<CrewScheduleModel> list = new List<CrewScheduleModel>();
            SqlCommand com = new SqlCommand("ProcGetAllCrewSchedule", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            list = (from DataRow dr in dt.Rows
                    select new CrewScheduleModel()
                    {
                        idcrewschedule = Convert.ToInt32(dr["id_schedule"]),
                        idcrew = Convert.ToString(dr["id_crew"]),
                        name = Convert.ToString(dr["name"]),
                        startdate = Convert.ToString(dr["start_date"]),
                        enddate = Convert.ToString(dr["end_date"])
                    }).ToList();

            return list;
        }

        public List<CrewScheduleModel> getByDaygetAllCrewSchedule()
        {
            List<CrewScheduleModel> list = new List<CrewScheduleModel>();
            SqlCommand com = new SqlCommand("ProcGetByDayCrewSchedule", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            list = (from DataRow dr in dt.Rows
                    select new CrewScheduleModel()
                    {
                        idcrewschedule = Convert.ToInt32(dr["id_schedule"]),
                        idcrew = Convert.ToString(dr["id_crew"]),
                        name = Convert.ToString(dr["name"]),
                        status = Convert.ToString(dr["status"]),
                        startdate = Convert.ToString(dr["start_date"]),
                        enddate = Convert.ToString(dr["end_date"])
                    }).ToList();

            return list;
        }

        public List<CrewScheduleModel> getCrewByName()
        {
            List<CrewScheduleModel> list = new List<CrewScheduleModel>();
            SqlCommand com = new SqlCommand("ProcGetCrewByName", con);
            com.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            list = (from DataRow dr in dt.Rows
                    select new CrewScheduleModel()
                    {
                        idcrew = Convert.ToString(dr["id_crew"]),
                        name = Convert.ToString(dr["name"]),
                        status = Convert.ToString(dr["status"]),
                    }).ToList();

            return list;
        }
    }
}