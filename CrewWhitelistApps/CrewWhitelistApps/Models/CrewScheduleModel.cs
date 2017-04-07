using System;
using System.ComponentModel.DataAnnotations;

namespace CrewWhitelistApps.Models
{
    public class CrewScheduleModel : CrewModel
    {
        [Key]
        [Display(Name = "Id Crew Schedule")]
        public int idcrewschedule { get; set; }

        [Display(Name = "Start Date")]
        public string startdate { get; set; }

        [Display(Name = "End Date")]
        public string enddate { get; set; }

        [Display(Name = "New Start Date")]
        public DateTime? startdateConvrt { get; set; }

        [Display(Name = "New End Date")]
        public DateTime? enddateConvrt { get; set; }

    }
}