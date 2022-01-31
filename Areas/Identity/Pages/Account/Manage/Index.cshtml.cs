using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ActivitySystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ActivitySystem.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Display(Name = "帳號")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "用戶名稱")]
            public string FullName { get; set; }

            [Phone]
            [Display(Name = "電話號碼")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                FullName = user.FullName,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.FullName = Input.FullName;
            user.PhoneNumber = Input.PhoneNumber;

            var userUpdateResult = await _userManager.UpdateAsync(user);
            if (!userUpdateResult.Succeeded)
            {
                StatusMessage = "請確認輸入資訊是否正確";
                return RedirectToPage();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "已更新個人資料";
            return RedirectToPage();
        }
    }
}
