using LibanonProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibanonProject.Repository
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        User Add(User item);
        
    }
}
