using EventManagementAPI.Contexts;
using EventManagementAPI.Exceptions;
using EventManagementAPI.Interfaces;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventManagementAPI.Repositories
{
    public class UserRepository : IRepository<int, User>
    {
        private readonly EventManagementContext _context;
        public UserRepository(EventManagementContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<User> Delete(int key)
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

        public async Task<User> Get(int key)
        {
            var user = (await _context.Users.FirstOrDefaultAsync(u => u.UserProfileId == key)) ?? throw new Exception("No user with the given ID");
            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<User> Update(User item)
        {
            var user = await Get(item.UserProfileId);
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
