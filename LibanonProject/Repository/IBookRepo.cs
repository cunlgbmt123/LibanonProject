using LibanonProject.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibanonProject.Repository
{
    public interface IBookRepo
    {
        
        IEnumerable<Book> GetAll();
        Book GetById(int id);
        
        bool  Add(Book item);
        bool Update(Book item);
        
    }
}
