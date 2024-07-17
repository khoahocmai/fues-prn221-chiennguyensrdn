using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using Repositories.IRepo;
using BusinessObjects;

namespace fues_prn221_chiennguyensrdn.Pages.Seller.ManageProduct
{
    public class EditProductModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        public EditProductModel(IProductRepository productRepository, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepository;
            _categoryRepo = categoryRepo;
        }

        [BindProperty]
        public Product Product { get; set; }

        public List<Category> Categories { get; set; }

        [BindProperty]
        public IFormFile ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _productRepo.GetProductById(id);

            if (Product == null)
            {
                return NotFound();
            }

            Categories = await _categoryRepo.GetCategories();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                Categories = await _categoryRepo.GetCategories();
                return Page();
            }

            var existingProduct = await _productRepo.GetProductById(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            UpdateProductValues(existingProduct, Product);

            await _productRepo.UpdateProduct(existingProduct);
            return RedirectToPage("./ViewOwnProduct");
        }

        private void UpdateProductValues(Product existingProduct, Product newProduct)
        {
            if (!string.IsNullOrEmpty(newProduct.Title) && newProduct.Title != existingProduct.Title)
            {
                existingProduct.Title = newProduct.Title;
            }
            if (!string.IsNullOrEmpty(newProduct.Description) && newProduct.Description != existingProduct.Description)
            {
                existingProduct.Description = newProduct.Description;
            }
            if (newProduct.Price != existingProduct.Price)
            {
                existingProduct.Price = newProduct.Price;
            }
            if (newProduct.CategoryId != existingProduct.CategoryId)
            {
                existingProduct.CategoryId = newProduct.CategoryId;
            }
            if (ImageFile != null && ImageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    ImageFile.CopyTo(memoryStream);
                    existingProduct.Image = memoryStream.ToArray();
                }
            }
        }
    }
}
