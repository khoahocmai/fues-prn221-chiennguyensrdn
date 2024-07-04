using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepo;

namespace fues_prn221_chiennguyensrdn.Pages.Seller
{
    public class ViewProductDetailModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly ICommentRepository _commentRepo;
        private readonly IRatingRepository _ratingRepo;

        public ViewProductDetailModel(IProductRepository productRepo, ICommentRepository commentRepo, IRatingRepository ratingRepo)
        {
            _productRepo = productRepo;
            _commentRepo = commentRepo;
            _ratingRepo = ratingRepo;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public Product Product { get; set; }
        public List<Comment> Comments { get; set; }
        public double AverageRating { get; set; }

        [BindProperty]
        public string NewComment { get; set; }
        [BindProperty]
        public int NewRating { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Product = await _productRepo.GetProductById(Id);
            if (Product == null)
            {
                return NotFound();
            }

            Comments = await _commentRepo.GetCommentsByProductId(Id);
            AverageRating = await _ratingRepo.GetAverageRatingByProductId(Id);

            return Page();
        }

        public async Task<IActionResult> OnPostCommentAsync()
        {
            // Clear model state errors for NewRating
            ModelState.Remove(nameof(NewRating));

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userIdClaim = User.FindFirst("UserId");
            int userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            var product = await _productRepo.GetProductById(Id);
            if (product == null)
            {
                return NotFound();
            }

            var newComment = new Comment
            {
                ProductId = product.Id,
                UserId = userId,
                Comment1 = NewComment,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            await _commentRepo.AddComment(newComment);

            return RedirectToPage("/Buyer/ViewProductDetail", new { id = product.Id });
        }
    }
}
