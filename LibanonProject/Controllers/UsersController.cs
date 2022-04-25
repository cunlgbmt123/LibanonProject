using LibanonProject.Models;
using LibanonProject.Repository;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace LibanonProject.Controllers
{
    public class UsersController : Controller
    {
       readonly IUserRepo userRepo;
        public UsersController(IUserRepo userRepo)
        {
            this.userRepo = userRepo;
        }
        // GET: Users
        public ActionResult Index()
        {
            var user = userRepo.GetAll();
            return View(user);
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            
            var user = userRepo.GetById(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( User user)
        {
            if (ModelState.IsValid)
            {
                userRepo.Add(user);
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        
    }
}
