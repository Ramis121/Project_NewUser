using Project_NewUser.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project_NewUser.Interface
{
    public interface IGenericUser<T> where T : class
    {
        Task<IEnumerable<T>> GetUser();
        Task<T> AddUser(T user);
        Task<T> EditUser(T user);
        Task<int> RemoveUser(int id);
        Task Insert(T user);
        Task<T> Deteils(int id);
        Task<T> GetUserId(int id);
    }
}
