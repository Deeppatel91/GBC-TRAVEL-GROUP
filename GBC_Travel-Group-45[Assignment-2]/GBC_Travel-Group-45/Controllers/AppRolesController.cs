using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging; // Add this namespace for ILogger
using System.Threading.Tasks;

namespace GBC_Travel_Group_45.Controllers
{
    [Authorize]
    public class AppRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AppRolesController> _logger; // Add ILogger

        public AppRolesController(RoleManager<IdentityRole> roleManager, ILogger<AppRolesController> logger) // Inject ILogger
        {
            _roleManager = roleManager;
            _logger = logger; // Initialize logger
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (!_roleManager.RoleExistsAsync(model.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(model.Name)).GetAwaiter().GetResult();

                // Log the creation of a new role
                _logger.LogInformation($"Role '{model.Name}' created successfully.");
            }

            return RedirectToAction("Index");
        }
    }
}