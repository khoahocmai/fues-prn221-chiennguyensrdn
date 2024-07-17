using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Repo;

namespace fues_prn221_chiennguyensrdn.Pages.Account
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly ResetPasswordRepo _resetPasswordRepo;

        public ForgotPasswordModel(ResetPasswordRepo resetPasswordRepo)
        {
            _resetPasswordRepo = resetPasswordRepo;
        }

        [BindProperty]
        public string Email { get; set; }

        public bool EmailSent { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            EmailSent = await _resetPasswordRepo.SendPasswordResetEmail(Email);
            return Page();
        }
    }
}
