using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibanonProject.Models;
using LibanonProject.Repository;

namespace LibanonProject.Controllers
{
    public class BorrowBooksController : Controller
    {
        readonly IBookRepo _bookRepo;
        readonly IBorrowRepo _borrowRepo;

        public BorrowBooksController(IBookRepo bookRepos, IBorrowRepo borrowRepo)
        {
            this._borrowRepo = borrowRepo;
            this._bookRepo = bookRepos;

        }
        // GET: Book on shelf
        public ActionResult Index()
        {
            var book = _bookRepo.GetAll().OrderBy(a => a.BookId)
                .Where(a => a.BookStatus == true)
                .Where(a => a.IsBorrow == false);
            return View(book);
        }

        // GET: BorrowBooks/Details/5
        public ActionResult Details(int id)
        {
            var book = _bookRepo.GetById(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: BorrowBooks
        public ActionResult Create()
        {
            /*ViewBag.CategoryId = new SelectList(categoryRepository.GetAll().ToList(), "CategoryId", "CategoryName");*/
            ViewBag.CurrentBookId = new SelectList(_borrowRepo.GetAll().ToList(), "BookId", "Title");
            return View();
        }

        // POST: BorrowBooks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BorrowBook borrowBook)
        {
            if (ModelState.IsValid)
            {
                db.BorrowBooks.Add(borrowBook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CurrentBookId = new SelectList(db.books, "BookId", "Title", borrowBook.CurrentBookId);
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
            ViewBag.CurrentBookId = new SelectList(, "BookId", "Title", borrowBook.CurrentBookId);
            return View(borrowBook);
        }

        // POST: BorrowBooks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Status,BUser,BEmail,BPhone,ActiveCode,OTP,CurrentBookId")] BorrowBook borrowBook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrowBook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CurrentBookId = new SelectList(db.books, "BookId", "Title", borrowBook.CurrentBookId);
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
        }*/
    }
}
