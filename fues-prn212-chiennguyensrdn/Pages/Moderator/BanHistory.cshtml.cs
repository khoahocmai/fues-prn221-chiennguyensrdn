using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories.IRepo;
using System.Security.Claims;
using System.Linq;

namespace fues_prn221_chiennguyensrdn.Pages.Moderator
{
    public class BanHistoryModel : PageModel
    {
        private readonly IBanRepository _banRepo;
        private readonly IUserRepository _userRepo;

        public BanHistoryModel(IBanRepository banRepo, IUserRepository userRepo)
        {
            _banRepo = banRepo;
            _userRepo = userRepo;
        }

        public IList<Ban> Bans { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst("UserId");
            int moderatorId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            var moderator = await _userRepo.GetUserById(moderatorId);
            if (moderator == null)
            {
                return NotFound();
            }

            Bans = await _banRepo.GetBanByModeratorId(moderatorId);

            return Page();
        }
    }
}
