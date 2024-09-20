using StarFleetBuilder.Data;
using StarFleetBuilder.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace StarFleetBuilder.Services
{
    public class UserService
    {
        private readonly StarFleetBuilderContext _context;

        public UserService(StarFleetBuilderContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync()
        {
            var user = await _context.User.FirstOrDefaultAsync();
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }
            return user;
        }

        public async Task<bool> SubtractCreditsAsync(int cost)
        {
            var user = await GetUserAsync();

            if (user.Credits >= cost)
            {
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        user.Credits -= cost;
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return true;
                    }
                    catch
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            return false;
        }

        public async Task AddCreditsAsync(int credits)
        {
            var user = await GetUserAsync();

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    user.Credits += credits;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
