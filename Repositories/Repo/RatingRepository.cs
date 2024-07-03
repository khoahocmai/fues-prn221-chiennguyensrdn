using BusinessObjects;
using DataAccessObjects.DAO;
using Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repo
{
    public class RatingRepository : IRatingRepository
    {
        public Task<List<Rating>> GetRatings()
            => RatingDAO.Instance.GetRatings();

        public Task<Rating> GetRatingById(int id)
            => RatingDAO.Instance.GetRatingById(id);

        public Task AddRating(Rating rating)
            => RatingDAO.Instance.AddRating(rating);

        public Task UpdateRating(Rating rating)
            => RatingDAO.Instance.UpdateRating(rating);

        public Task RemoveRating(int id)
            => RatingDAO.Instance.RemoveRating(id);
    }
}
