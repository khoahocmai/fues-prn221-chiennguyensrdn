using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessObjects.DAO
{
    public class RatingDAO
    {
        private static RatingDAO instance = null;
        public static readonly object Lock = new object();
        private RatingDAO() { }
        public static RatingDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new RatingDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Rating>> GetRatings()
        {
            using var db = new FUESManagementContext();
            return await db.Ratings.ToListAsync();
        }

        public async Task<Rating> GetRatingById(int id)
        {
            using var db = new FUESManagementContext();
            return await db.Ratings.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddRating(Rating rating)
        {
            using var db = new FUESManagementContext();
            await db.Ratings.AddAsync(rating);
            await db.SaveChangesAsync();
        }

        public async Task UpdateRating(Rating rating)
        {
            using var db = new FUESManagementContext();
            var existingRating = await db.Ratings.FindAsync(rating.Id);
            if (existingRating != null)
            {
                existingRating.ProductId = rating.ProductId;
                existingRating.UserId = rating.UserId;
                existingRating.Rating1 = rating.Rating1;

                db.Ratings.Update(existingRating);
                await db.SaveChangesAsync();
            }
        }

        public async Task RemoveRating(int id)
        {
            using var db = new FUESManagementContext();
            var rating = await db.Ratings.FindAsync(id);
            if (rating != null)
            {
                db.Ratings.Remove(rating);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Rating> GetRatingsByProductIdAndUserId(int productId, int userId)
        {
            using var db = new FUESManagementContext();
            return await db.Ratings.SingleOrDefaultAsync(r => r.ProductId == productId && r.UserId == userId);
        }

        public async Task<double> GetAverageRatingByProductId(int productId)
        {
            using var db = new FUESManagementContext();

            // Check if there are any ratings for the product
            var ratings = await db.Ratings
                     .Where(r => r.ProductId == productId)
                     .ToListAsync();

            // If there are no ratings, return 0
            if (ratings == null || ratings.Count == 0)
            {
                return 0;
            }

            double averageRating = ratings.Average(r => r.Rating1.Value);
            return averageRating;
        }
    }
}
