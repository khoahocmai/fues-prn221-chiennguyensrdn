using BusinessObjects;
using Microsoft.EntityFrameworkCore;
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
            return await _context.Ratings.ToListAsync();
        }

        public async Task<Rating> GetRatingById(int id)
        {
            return await _context.Ratings.SingleOrDefaultAsync(r => r.Id == id);
        }

        public async Task AddRating(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRating(Rating rating)
        {
            var existingRating = await _context.Ratings.FindAsync(rating.Id);
            if (existingRating != null)
            {
                existingRating.ProductId = rating.ProductId;
                existingRating.UserId = rating.UserId;
                existingRating.Rating1 = rating.Rating1;

                _context.Ratings.Update(existingRating);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveRating(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            if (rating != null)
            {
                _context.Ratings.Remove(rating);
                await _context.SaveChangesAsync();
            }
        }
    }
}
