using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CrewWhitelistApps.Models
{
    public class CrewModel
    {
        [Display(Name = "Barcode")]
        [Required(ErrorMessage = "Id Crew is required")]
        public string idcrew { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Display(Name = "Date List")]
        public string datelist { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Status is required")]
        public string status { get; set; }

        public IEnumerable<SelectListItem> setStatus { get; set; }

        [Display(Name = "Airport")]
        [Required(ErrorMessage = "Airport is required")]
        public string airport { get; set; }

        [Display(Name = "Compnay Airways")]
        [Required(ErrorMessage = "Compnay Airways is required")]
        public string companyairways { get; set; }
    }
}