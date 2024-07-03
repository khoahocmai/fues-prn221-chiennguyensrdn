using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepo;
using System.ComponentModel.DataAnnotations;

namespace fues_prn221_chiennguyensrdn.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IUserRepository _userRepository;

        public RegisterModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = null!;

        [BindProperty]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; } = null!;

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [BindProperty]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = null!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = new User
            {
                Email = Email,
                Username = Username,
                Password = Password,
                Name = Name,
                Role = "Guest"
            };

            await _userRepository.Register(user);

            return RedirectToPage("./RegisterSuccess");
        }
    }
}
