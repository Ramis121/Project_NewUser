using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project_NewUser.Data;
using Project_NewUser.Exceptions;
using Project_NewUser.Interface;
using Project_NewUser.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project_NewUser.Repository
{
    public class RepositoryUser : IRepositoryUser
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<RepositoryUser> _logger;
        public const string ThisName = nameof(RepositoryUser);
        public RepositoryUser(ApplicationDbContext dbContext,
            ILogger<RepositoryUser> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<User> AddUser(User user)
        {
            var logadduser = $"{ThisName}/{nameof(AddUser)}";
            _logger.LogInformation($"method {logadduser} started processing data");
            if (user is null)
            {
                _logger.LogError("data is not filled");
                return null;
            }
            if (string.IsNullOrWhiteSpace(user.Name) && string.IsNullOrWhiteSpace(user.Id.ToString()))
            {
                _logger.LogError($"Is Null Or White Space method {logadduser}");
                return null;
            }
            if (_dbContext.users.Any(a => a.Name == user.Name) || _dbContext.users.Any(a => a.Id == user.Id)) 
            {
                _logger.LogError("indexer or name already exists");
                return null;
            }
            try
            {
                await Insert(new User
                {
                    Name = user.Name,
                    age = user.age,
                    Id = user.Id,
                });
            }
            catch 
            {
                throw new MvcExceptions($"User not added, method {logadduser}");
            }
            return user;
        }

        public async Task<User> Deteils(int id)
        {
            var logdeteils = $"{ThisName}/{nameof(Deteils)}";
            try
            {
                var res = await GetUserId(id);
                return res;
            }
            catch
            {
                throw new MvcExceptions($"Not fould deteils, method {logdeteils}");
            }
        }

        public async Task<User> EditUser(User user)
        {
            var logedituser = $"{ThisName}/{nameof(EditUser)}";
            _logger.LogInformation($"method {logedituser} started processing data");
            if (user is null)
            {
                _logger.LogError("data is not filled");
                return null;
            }
            if (string.IsNullOrWhiteSpace(user.Name) && string.IsNullOrWhiteSpace(user.Id.ToString()))
            {
                _logger.LogError($"Is Null Or White Space method {logedituser}");
                return null;
            }
            if (_dbContext.users.Any(a => a.Name == user.Name) && _dbContext.users.Any(a => a.Id == user.Id))
            {
                _logger.LogError("indexer or name already exists");
                return null;
            }
            try
            {
                _dbContext.users.Update(user);
                await Insert(user);
            }
            catch
            {
                throw new MvcExceptions($"User not added, method {logedituser}");
            }
            return user;
        }

        public async Task<IEnumerable<User>> GetUser()
        {
            var loggetuser = $"{ThisName}/{nameof(GetUser)}";
            try
            {
                _logger.LogInformation($"data processing in progress, method {loggetuser}");
                return await _dbContext.users
                    .OrderBy(a => a.Id)
                    .ToListAsync();
            }
            catch
            {
                _logger.LogError($"data lost, method {loggetuser}");
                throw new MvcExceptions($"User data not found, method {loggetuser}");
            }
        }

        public async Task<User> GetUserId(int id)
        {
            var loguserid = $"{ThisName}/{nameof(GetUserId)}";
            _logger.LogInformation($"method {loguserid} started processing data");
            var resId = await _dbContext.users.FindAsync(id);
            if (resId is not null)
            {
                _logger.LogInformation($"user with indexer {resId} found");
                return resId;
            }
            _logger.LogError($"user with indexer {resId} not found");
            return null;
        }

        public async Task Insert(User user)
        {
            try
            {
                await _dbContext.users.AddAsync(user);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                throw new MvcExceptions($"User {user.Name} not added");
            }
        }

        public async Task<int> RemoveUser(int id)
        {
            var logremoveuser = $"{ThisName}/{nameof(RemoveUser)}";
            _logger.LogInformation($"method {logremoveuser} started processing data");
            if (!string.IsNullOrEmpty(id.ToString()))
            {
                var user = await GetUserId(id);
                _dbContext.users.Remove(user);
                await _dbContext.SaveChangesAsync();
                _logger.LogWarning($"User id {id} remove");
                return id;
            }
            else
            {
                _logger.LogError("user with this index does not exist");
                return 0;
            }
        }
    }
}
