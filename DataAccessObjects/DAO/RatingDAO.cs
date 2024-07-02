using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DAO
{
    public class RatingDAO
    {
        private readonly FUESManagementContext _context;

        public RatingDAO(FUESManagementContext context)
        {
            _context = context;
        }

        public Rating GetRating(int id)
        {
            Rating rating = _context.Ratings.Find(id);
            return rating;
        }

        public List<Rating> GetRatings()
        {
            List<Rating> ratings = _context.Ratings.ToList();
            return ratings;
        }
        public void CreateRating(Rating rating)
        {
             _context.Ratings.Add(rating);
        }
        public void UpdateRating(Rating rating) {
            Rating currentRating = _context.Ratings.Find(rating.Id);
            _context.Ratings.Update(currentRating);
        }
        public void DeleteRating(int id) {
            Rating rating = _context.Ratings.Find(id);
            _context.Ratings.Remove(rating);
        }

    }
}
