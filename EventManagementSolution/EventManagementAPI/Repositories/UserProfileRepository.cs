using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagementAPI.Repositories
{
    public class UserProfileRepository : IRepository<int, UserProfile>
    {
        private readonly EventManagementContext _context;
        public UserProfileRepository(EventManagementContext context)
        {
            _context = context;
        }
        public async Task<UserProfile> Add(UserProfile item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<UserProfile> Delete(int key)
        {
            var user = await Get(key);
            if (user != null)
            {
                _context.Remove(user);
                _context.SaveChangesAsync(true);
                return user;
            }
            throw new NoSuchUserException();
        }

        public async Task<UserProfile> Get(int key)
        {
            var user = (await _context.UserProfiles.FirstOrDefaultAsync(u => u.Id == key));
            return user;
        }

        public async Task<IEnumerable<UserProfile>> GetAll()
        {
            var users = await _context.UserProfiles.ToListAsync();
            return users;
        }

        public async Task<UserProfile> Update(UserProfile item)
        {
            var user = await Get(item.Id);
            if (user != null)
            {
                _context.Update(item);
                _context.SaveChangesAsync(true);
                return user;
            }
            throw new NoSuchUserException();
        }
    }
}
