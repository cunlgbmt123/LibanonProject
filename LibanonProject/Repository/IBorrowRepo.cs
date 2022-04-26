using LibanonProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibanonProject.Repository
{
    public interface IBorrowRepo
    {
        IEnumerable<BorrowBook> GetAll();
        BorrowBook GetById(int id);

        bool Borrow(BorrowBook item);
        bool GiveBack(BorrowBook item);
        
    }
}
