using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibanonProject.Models;

namespace LibanonProject.Controllers
{
    public class BorrowBooksController : Controller
    {
        private BookContext db = new BookContext();

        // GET: BorrowBooks
        public ActionResult Index()
        {
            var borrowBooks = db.BorrowBooks.Include(b => b.Currentbook).Include(b => b.CurrentUser);
            return View(borrowBooks.ToList());
        }

        // GET: BorrowBooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowBook borrowBook = db.BorrowBooks.Find(id);
            if (borrowBook == null)
            {
                return HttpNotFound();
            }
            return View(borrowBook);
        }

        // GET: BorrowBooks/Create
        public ActionResult Create()
        {
            ViewBag.CurrentBookId = new SelectList(db.books, "BookId", "Title");
            ViewBag.CurrentUserId = new SelectList(db.User, "UserId", "UserName");
            return View();
        }

        // POST: BorrowBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Status,CurrentBookId,CurrentUserId")] BorrowBook borrowBook)
        {
            if (ModelState.IsValid)
            {
                db.BorrowBooks.Add(borrowBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurrentBookId = new SelectList(db.books, "BookId", "Title", borrowBook.CurrentBookId);
            ViewBag.CurrentUserId = new SelectList(db.User, "UserId", "UserName", borrowBook.CurrentUserId);
            return View(borrowBook);
        }

        // GET: BorrowBooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowBook borrowBook = db.BorrowBooks.Find(id);
            if (borrowBook == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrentBookId = new SelectList(db.books, "BookId", "Title", borrowBook.CurrentBookId);
            ViewBag.CurrentUserId = new SelectList(db.User, "UserId", "UserName", borrowBook.CurrentUserId);
            return View(borrowBook);
        }

        // POST: BorrowBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Status,CurrentBookId,CurrentUserId")] BorrowBook borrowBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrowBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentBookId = new SelectList(db.books, "BookId", "Title", borrowBook.CurrentBookId);
            ViewBag.CurrentUserId = new SelectList(db.User, "UserId", "UserName", borrowBook.CurrentUserId);
            return View(borrowBook);
        }

        // GET: BorrowBooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowBook borrowBook = db.BorrowBooks.Find(id);
            if (borrowBook == null)
            {
                return HttpNotFound();
            }
            return View(borrowBook);
        }

        // POST: BorrowBooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BorrowBook borrowBook = db.BorrowBooks.Find(id);
            db.BorrowBooks.Remove(borrowBook);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
