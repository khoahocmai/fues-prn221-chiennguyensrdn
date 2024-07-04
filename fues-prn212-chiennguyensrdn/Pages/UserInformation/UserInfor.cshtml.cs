using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepo;

namespace fues_prn221_chiennguyensrdn.Pages.CustomerInfor
{
    public class CustomerInforModel : PageModel
    {
        private readonly IUserRepository _userRepo;

        public CustomerInforModel(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        [BindProperty]
        public int UserId { get; set; }

        [BindProperty]
        public string? UserName { get; set; }

        [BindProperty]
        public string Email { get; set; } = null!;

        [BindProperty]
        public string? Password { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst("UserId");
            int id = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            var user = await _userRepo.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            UserId = user.Id;
            UserName = user.Name;
            Email = user.Email;
            Password = user.Password;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userIdClaim = User.FindFirst("UserId");
            int id = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            var user = await _userRepo.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            UserId = user.Id;
            UserName = user.Name;
            Email = user.Email;
            if (!string.IsNullOrWhiteSpace(Password) && Password != user.Password)
            {
                user.Password = Password;
            }
            else
            {
                user.Password = user.Password;
            }

            await _userRepo.UpdateUser(user);

            return RedirectToPage("/UserInformation/UserInfor");
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var userIdClaim = User.FindFirst("UserId");
            int id = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            var user = await _userRepo.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            await _userRepo.RemoveUser(user);

            await HttpContext.SignOutAsync();

            return RedirectToPage("/Account/Login");
        }
    }
}
