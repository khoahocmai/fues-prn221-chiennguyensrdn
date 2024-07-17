using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.Repo;

namespace fues_prn221_chiennguyensrdn.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly ResetPasswordRepo _resetPasswordRepo;

        public ResetPasswordModel(ResetPasswordRepo resetPasswordRepo)
        {
            _resetPasswordRepo = resetPasswordRepo;
        }

        [BindProperty]
        public string Token { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }

        public bool PasswordReset { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync(string token)
        {
            Token = token;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var resetSuccessful = await _resetPasswordRepo.ResetPassword(Token, NewPassword);
            if (resetSuccessful)
            {
                PasswordReset = true;
                return Page();
            }
            else
            {
                ErrorMessage = "Password reset failed. Token may have expired.";
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }
            return Page();
        }
            
    }
    
}
