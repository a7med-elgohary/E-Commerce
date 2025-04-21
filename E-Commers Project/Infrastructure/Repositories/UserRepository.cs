using E_Commers.Infrastructure.data;
using E_Commers.Infrastructure.Repositories;
using E_Commers.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using E_Commers_Project.Domain.Models;

public class UserRepository : GenericRepository<User> , IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {

    }

    public async Task<bool> IsFounded(LoginModel model)
    {
        var user = await _dbSet.Where(x=> EF.Functions.Collate(x.Email ,  "Latin1_General_CS_AS") == model.Email && 
        EF.Functions.Collate (x.Password , "Latin1_General_CS_AS") ==model.Password).FirstOrDefaultAsync();

        if (user != null)
        {
            return true;
        }
        return false;
    }

    //by email

    public async Task<User?> GetUserByEmailAsync(string email)
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

    public async Task<User> CreateUserAsync(User user)
    {
        _dbSet.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    
}
