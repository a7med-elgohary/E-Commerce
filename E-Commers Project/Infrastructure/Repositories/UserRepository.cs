using E_Commers_Project.Models;
using E_Commers.Infrastructure.data;
using E_Commers.Infrastructure.Repositories;
using E_Commers.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

public class UserRepository :  GenericRepository<User> ,IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {

    }
    //by email
    public async Task<User?> FindUserByEmailAsync(string email)
    {
        if (email == null) { return null; }
        return await _dbSet.FirstOrDefaultAsync(x => x.Email == email);

    }
    //active users
    public async Task<IEnumerable<User>?> GetActiveUsersAsync()
    {
        return await _dbSet.Where(x => x.IsActive).ToListAsync();
    }

    // activate user account
    public async Task ActivateUserAccount(int userId)
    {
        var user = await GetByIdAsync(userId);
        if (user != null)
        {
            user.IsActive = true;
            await _context.SaveChangesAsync();
        }           
    }
}
