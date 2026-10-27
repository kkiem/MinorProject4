using Microsoft.AspNetCore.Mvc;

namespace MinorProject4.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataAccess _db;
        private readonly AppSettings _settings;

        public HomeController(DataAccess db, AppSettings settings)
        {
            _db = db;
            _settings = settings;
        }

        // 1. DRIVERS PAGE (Search + Full CRUD)
        public IActionResult Index(string? search)
        {
            ViewBag.Search = search;
            ViewBag.Teams = _db.GetTeams();
            var drivers = _db.GetDrivers(search);
            return View(drivers);
        }

        [HttpPost]
        public IActionResult AddDriver(string driverName, string nationality, int carNumber, int? teamId)
        {
            _db.AddDriver(driverName, nationality, carNumber, teamId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateDriver(int driverId, string driverName, string nationality, int carNumber, int? teamId)
        {
            _db.UpdateDriver(driverId, driverName, nationality, carNumber, teamId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteDriver(int driverId)
        {
            _db.DeleteDriver(driverId);
            return RedirectToAction("Index");
        }

        // 2. PRINCIPALS PAGE (Full CRUD)
        public IActionResult Principals()
        {
            ViewBag.Teams = _db.GetTeams();
            var list = _db.GetPrincipals();
            return View(list);
        }

        [HttpPost]
        public IActionResult AddPrincipal(string principalName, string roleTitle, int yearsExperience, int? teamId)
        {
            _db.AddPrincipal(principalName, roleTitle, yearsExperience, teamId);
            return RedirectToAction("Principals");
        }

        [HttpPost]
        public IActionResult UpdatePrincipal(int principalId, string principalName, string roleTitle, int yearsExperience, int? teamId)
        {
            _db.UpdatePrincipal(principalId, principalName, roleTitle, yearsExperience, teamId);
            return RedirectToAction("Principals");
        }

        [HttpPost]
        public IActionResult DeletePrincipal(int principalId)
        {
            _db.DeletePrincipal(principalId);
            return RedirectToAction("Principals");
        }

        // 3. ADMIN PAGE - TEAMS (Full CRUD)
        public IActionResult Admin()
        {
            var teams = _db.GetTeams();
            return View(teams);
        }

        [HttpPost]
        public IActionResult AddTeam(string teamName, string baseCountry, string engineSupplier)
        {
            _db.AddTeam(teamName, baseCountry, engineSupplier);
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public IActionResult UpdateTeam(int teamId, string teamName, string baseCountry, string engineSupplier)
        {
            _db.UpdateTeam(teamId, teamName, baseCountry, engineSupplier);
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public IActionResult DeleteTeam(int teamId)
        {
            _db.DeleteTeam(teamId);
            return RedirectToAction("Admin");
        }

        // 4. SETTINGS PAGE (Dark Mode)
        public IActionResult Settings()
        {
            return View(_settings);
        }

        [HttpPost]
        public IActionResult ToggleTheme(bool darkMode)
        {
            _settings.DarkMode = darkMode;
            return RedirectToAction("Settings");
        }
    }
}