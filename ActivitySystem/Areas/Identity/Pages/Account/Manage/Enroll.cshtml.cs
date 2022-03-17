using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ActivitySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ActivitySystem.Areas.Identity.Pages.Account.Manage
{
    public class EnrollModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<EnrollModel> _logger;
        private readonly IEnrollRepository _enrollRepository;

        public IEnumerable<Enroll> Enrolls { get; set; }
        public EnrollModel(UserManager<ApplicationUser> userManager, ILogger<EnrollModel> logger, IEnrollRepository enrollRepository)
        {
            _userManager = userManager;
            _logger = logger;
            _enrollRepository = enrollRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = _userManager.GetUserId(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userId}'.");
            }

            Enrolls = _enrollRepository.GetEnrollsByUserId(userId);

            return Page();
        }

        public async Task<IActionResult> OnPost(int activityId)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null || activityId == 0)
            {
                return NotFound();
            }

            var count = _enrollRepository.DeleteEnrollByActivityIdAndUserId(activityId, userId);
            if (!count)
            {
                return BadRequest();
            }

            TempData["Message"] = "§R°£¦¨¥\";
            Enrolls = _enrollRepository.GetEnrollsByUserId(userId);

            return Page();
        }
    }
}
