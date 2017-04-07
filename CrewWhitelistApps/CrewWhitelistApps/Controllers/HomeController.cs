using CrewWhitelistApps.Models;
using CrewWhitelistApps.Repository.Implement;
using CrewWhitelistApps.Security;
using System.Web.Mvc;
using System.Web.Security;


namespace CrewWhitelistApps.Controllers
{
    public class HomeController : Controller
    {
        private ImplementLogin il = new ImplementLogin();
        private ImplementCrew ic = new ImplementCrew();
        private ImplementCrewSchedule ics = new ImplementCrewSchedule();
     
        public ActionResult Index()
        {
            var data = il.GetAllRoles();
            var model = new AdministartorModel();
            model.roles = il.GetSelectListItems(data);

            return View(model);

        }

        [HttpPost]
        public ActionResult Index(AdministartorModel obj)
        {
            var data = il.GetAllRoles();
            obj.roles = il.GetSelectListItems(data);

            if (ModelState.IsValid)
            {
                if (il.isValid(obj))
                {
                    if (obj.role.Equals("admin"))
                    {
                        FormsAuthentication.SetAuthCookie(obj.username, false);
                        SessionPersister.username = obj.username;

                        return RedirectToAction("DashboardAdmin", "Home");
                    }
                    else if (obj.role.Equals("admin whitelist"))
                    {
                        FormsAuthentication.SetAuthCookie(obj.username, false);
                        SessionPersister.username = obj.username;

                        return RedirectToAction("DashboardAdminWhitelist", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }
            }
            return View("Index", obj);

        }

        [HttpGet]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            SessionPersister.username = string.Empty;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult AccesDenied()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DashboardAdmin()
        {
            if (SessionPersister.username != "")
            {
                return View(ic.getAllCrew());
            }
            else
            {
                return RedirectToAction("AccesDenied", "Home");
            }
        }

        public ActionResult CreateCrew()
        {
            if (SessionPersister.username != "")
            {
                var data = ic.GetAllStatus();
                var model = new CrewModel();
                model.setStatus = ic.GetSelectListItems(data);

                return View(model);
            }
            else
            {
                return RedirectToAction("AccesDenied", "Home");
            }
        }

        [HttpPost]
        public ActionResult CreateCrew(CrewModel obj)
        {
            var data = ic.GetAllStatus();
            obj.setStatus = ic.GetSelectListItems(data);

            if (ic.save(obj))
            {
                return RedirectToAction("CreateCrew", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Saving a request does not save");
            }

            return View("CreateCrew",obj);
        }

        public ActionResult EditCrew(string id)
        {
            if (SessionPersister.username != "")
            {
                return View(ic.getAllCrew().Find(c => c.idcrew == id));
            }
            else
            {
                return RedirectToAction("AccesDenied", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditCrew(string id, CrewModel obj)
        {
            if (ic.edit(obj))
            {
                ViewBag.Message = "Crew edit successfully";
            }
            else
            {
                ModelState.AddModelError("", "Saving a request does not save");
            }

            return View(obj);
        }

        public ActionResult DeleteCrew(string id)
        {
            try
            {
                CrewModel obj = new CrewModel();
                obj.idcrew = id;

                ic.delete(obj);

                return RedirectToAction("DashboardAdmin", "Home");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult DashboardAdminWhitelist()
        {
            if (SessionPersister.username != "")
            {
                return View(ics.getAllCrewSchedule());
            }
            else
            {
                return RedirectToAction("AccesDenied", "Home");
            }
        }

        public ActionResult CreateCrewSchedule()
        {
            if (SessionPersister.username != "")
            {
                return View(ics.getCrewByName());
            }
            else
            {
                return RedirectToAction("AccesDenied", "Home");
            }
        }

        public ActionResult AddScheduleCrew(string id)
        {
            if (SessionPersister.username != "")
            {
                return View(ics.getCrewByName().Find(c => c.idcrew == id));
            }
            else
            {
                return RedirectToAction("AccesDenied", "Home");
            }
        }

        [HttpPost]
        public ActionResult AddScheduleCrew(string id, CrewScheduleModel obj)
        {
            if (ics.save(obj))
            {
                return RedirectToAction("CreateCrewSchedule");
            }
            else
            {
                ModelState.AddModelError("", "Saving a request does not save");
            }

            return View(obj);
        }

        public ActionResult ScheduleToday()
        {
            return View(ics.getByDaygetAllCrewSchedule());
        }

        public ActionResult EditScheduleCrew(int id)
        {
            if (SessionPersister.username != "")
            {
                return View(ics.getAllCrewSchedule().Find(sc => sc.idcrewschedule == id));
            }
            else
            {
                return RedirectToAction("AccesDenied", "Home");
            }
        }

        [HttpPost]
        public ActionResult EditScheduleCrew(int id, CrewScheduleModel obj)
        {
            if (ics.edit(obj))
            {
                ViewBag.Message = "Crew edit successfully";
            }
            else
            {
                ModelState.AddModelError("", "Saving a request does not save");
            }

            return View(obj);
        }

        public ActionResult DeleteScheduleCrew(int id)
        {
            try
            {
                CrewScheduleModel obj = new CrewScheduleModel();
                obj.idcrewschedule = id;

                ics.delete(obj);

                return RedirectToAction("DashboardAdminWhitelist", "Home");
            }
            catch
            {
                return View();
            }
        }

    }
}