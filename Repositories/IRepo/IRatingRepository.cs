using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repositories.IRepo
{
    public interface IRatingRepository
    {
        Task<List<Rating>> GetRatings();
        Task<Rating> GetRatingById(int id);
        Task AddRating(Rating rating);
        Task UpdateRating(Rating rating);
        Task RemoveRating(int id);
        Task<Rating> GetRatingsByProductIdAndUserId(int productId, int userId);
        Task<double> GetAverageRatingByProductId(int productId);
    }
}