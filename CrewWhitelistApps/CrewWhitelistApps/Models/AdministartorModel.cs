using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace CrewWhitelistApps.Models
{
    public class AdministartorModel
    {
        [Display(Name = "Username")]
        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        public string password { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "Role is required")]
        public string role { get; set; }

        public IEnumerable<SelectListItem> roles { get; set; }

    }
}