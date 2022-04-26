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
using System.IO;
using System.Net.Mail;


namespace LibanonProject.Controllers
{
    public class BooksController : Controller
    {
        readonly IBookRepo _bookRepo;
       
        public BooksController(IBookRepo bookRepos)
        {
            this._bookRepo = bookRepos;            
        }        
        // GET: Books
        public ActionResult Index()
        {
            var book = _bookRepo.GetAll().OrderBy(a => a.BookId)
                .Where(a => a.BookStatus == false);
            return View(book);
        }
        // GET: Books/Details/5
        public ActionResult Details(int id)
        {
            
            var book = _bookRepo.GetById(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        public ActionResult BookIsBorrow()
        {
            var list = _bookRepo.GetBookIsBorrow();
            return View(list);
        }

        #region borrow book
        public ActionResult BorrowBook(int id)
        {
            Book book = _bookRepo.GetById(id);
            return View(book);
        }
        [HttpPost]
        public ActionResult BorrowBook(Book book)
        {
            TempData["borrower"] = book;
            string title = "Borrow Book Request Verification";
            string mailbody = "Hello " + book.BorrowerName;
            mailbody += $"<br /><br />You have sent the borrow book request, this is the book information:<br />BookName :{book.Title}<br />Author :{book.Author}<br />ISBN :{book.BookISBN.ISBNcode}";
            mailbody += "<br />Please click the following link to confirm your borrow book request:";
            mailbody += "<br /><a href = '" + string.Format($"{Request.Url.Scheme}://{Request.Url.Authority}/Books/ConfirmBorrow/{book.BookId}") + "'>Click here to request borrow book.</a>";
            _bookRepo.SendEmail(title, book.BorrowerEmail, mailbody);
            return RedirectToAction("ThongBao");
        }

        [HttpGet]
        public ActionResult ConfirmBorrow(int id)
        {
            //Save borrower information
            Book book = TempData["borrower"] as Book;
            _bookRepo.BorrowBook(id, book);

            //Send response mail
            string title = "Response the borrow book request";
            string mailbody = "Hello " + book.User.UserName;
            mailbody += $"<br /><br />The borrower {book.BorrowerName} want to borrow your book, this is the book information:<br />BookName :{book.Title}<br />Author :{book.Author}<br />ISBN :{book.BookISBN.ISBNcode}";
            mailbody += "<br />Please click the following link to response to the borrow request:";
            mailbody += "<br /><a href = '" + string.Format($"{Request.Url.Scheme}://{Request.Url.Authority}/Books/AcceptBorrow/{book.BookId}") + "'>Accept borrow book request.</a>";
            mailbody += "<br /><a href = '" + string.Format($"{Request.Url.Scheme}://{Request.Url.Authority}/Books/CancelBorrow/{book.BookId}") + "'>Cancell borrow book request.</a>";
            _bookRepo.SendEmail(title, book.User.UserEmail, mailbody);


            return RedirectToAction("ThongBao");
        }

        [HttpGet]
        public ActionResult AcceptBorrow(int id)
        {
            Book book = _bookRepo.GetById(id);

            string title = "Borrowed Book Verification";
            //gui maill cho chu so huu
            string mailbodyOwn = $"Hello {book.User.UserName},";
            mailbodyOwn += $"<br /><br />You have accepted the borrow book request of: {book.BorrowerEmail}, This is your book information:";
            mailbodyOwn += $"<br />BookName :{book.Title}<br />Author :{book.Author}<br />Published: {book.Publisher}<br />ISBN :{book.BookISBN.ISBNcode}";
            mailbodyOwn += "<br />Please click the following link to confirm the book is lent: ";
            mailbodyOwn += "<a href = '" + string.Format($"{Request.Url.Scheme}://{Request.Url.Authority}/Books/ReceiveBook?id={book.BookId}&ConfirmLend={true}") + "'>Book is lent</a>";
            _bookRepo.SendEmail(title, book.User.UserEmail, mailbodyOwn);

            //gui maill cho nhuoi muon
            string mailbodyBrw = "Hello borrower: " + book.BorrowerName;
            mailbodyBrw += $"<br /><br />Your book borrow request is accepted by : {book.User.UserEmail}, This is your book information:";
            mailbodyBrw += $"<br />BookName :{book.Title}<br />Author :{book.Author}<br />Published: {book.Publisher}<br />ISBN :{book.BookISBN.ISBNcode}";
            mailbodyBrw += "<br />Please click the following link to confirm the book is borrowed: ";
            mailbodyBrw += "<a href = '" + string.Format($"{Request.Url.Scheme}://{Request.Url.Authority}/Books/ReceiveBook?id={book.BookId}&StateBorrow={true}") + "'>Book is borrowed</a>";
            _bookRepo.SendEmail(title, book.BorrowerEmail, mailbodyBrw);    
            return RedirectToAction("ThongBao");
        }

        [HttpGet]
        public ActionResult ReceiveBook(int id, bool? stateIsBorrow, bool? stateBorrow)
        {
            Book book = _bookRepo.GetById(id);
            _bookRepo.StateOfBook(book, stateIsBorrow, stateBorrow);
            return RedirectToAction("ThongBao");
        }

        [HttpGet]
        public ActionResult CancelBorrow(int id)
        {
            Book book = _bookRepo.GetById(id);

            string title = "Borrow Book Request is canceled";
            //Owner mail
            string mailbodyOwn = $"Hello {book.User.UserName} Owner of {book.Title}";
            mailbodyOwn += $"<br /><br />You have canceled the borrow book request of: " + book.BorrowerEmail;
            _bookRepo.SendEmail(title, book.User.UserEmail, mailbodyOwn);

            //Borrower mail
            string mailbodyBrw = "Hello borrower: " + book.BorrowerName;
            mailbodyBrw += $"<br /><br />Your book borrow ({book.Title}) request is cancel by the owner: " + book.User.UserEmail;
            _bookRepo.SendEmail(title, book.BorrowerEmail, mailbodyBrw);
            _bookRepo.DeleteBorrower(book.BookId);
            return RedirectToAction("ThongBao");
        }
        #endregion

        #region Return book
        [HttpGet]
        public ActionResult Return(int id)
        {
            Book book = _bookRepo.GetById(id);
            return View(book);
        }

        [HttpPost]
        public ActionResult Return(Book book)
        {
            var email = _bookRepo.GetById(book.BookId).BorrowerEmail;
            if (book.BorrowerEmail == email)
            {
                string title = "Return book request verification";
                string mailbody = "Hello " + book.BorrowerName;
                mailbody += "<br /><br />Please click the following link to make sure that you want to return the book:";
                mailbody += "<br /><a href = '" + string.Format($"{Request.Url.Scheme}://{Request.Url.Authority}/Books/ConfirmReturn/{book.BookId}") + "'>Click here to return book.</a>";
                _bookRepo.SendEmail(title, book.BorrowerEmail, mailbody);

                object notification = "Your return book request is stored, please check your email to verification";
                return View("Notification", model: notification);
            }
            return View("ReturnBook");
        }

        [HttpGet]
        public ActionResult ConfirmReturn(int id)
        {
            Book book = _bookRepo.GetById(id);
            string title = "Received book verification";
            //Owner mail
            string mailbodyOwn = $"Hello {book.User.UserName} Owner of {book.Title}";
            mailbodyOwn += "<br /><br />Your book is return by : " + book.BorrowerName;
            mailbodyOwn += $"<br />Please contact {book.BorrowerEmail} to receive book. ";
            mailbodyOwn += $"<br /><br />Please click the following link if the book is return to you. ";
            mailbodyOwn += "<a href = '" + string.Format($"{Request.Url.Scheme}://{Request.Url.Authority}/Books/ReceiveBookBack?id={book.BookId}&ConfirmLend={false}") + "'>Book is received back</a>";
            _bookRepo.SendEmail(title, book.User.UserName, mailbodyOwn);

            //Borrower mail
            string mailbodyBrw = "Hello borrower: " + book.User.UserName;
            mailbodyBrw += $"<br /><br />Your book return ({book.Title}) request is sent to system. " + book.User.UserEmail;
            mailbodyBrw += "<br />Here is the book owner information: ";
            mailbodyBrw += $"<br />Owner name :{book.User.UserName} <br />Owner Mail :{book.User.UserEmail} ";
            mailbodyBrw += $"<br />Please click the following link if you has returned the book to the owner: ";
            mailbodyBrw += "<a href = '" + string.Format($"{Request.Url.Scheme}://{Request.Url.Authority}/Books/ReceiveBookBack?id={book.BookId}&StateBorrow={false}") + "'>Book is returned back</a>";
            _bookRepo.SendEmail(title, book.BorrowerEmail, mailbodyBrw);

            object notification = "The book is cofirm return back ";
            return View("Notification", model: notification);
        }

        [HttpGet]
        public ActionResult ReceiveBookBack(int id, bool? stateIsBorrow, bool? stateBorrow)
        {
            Book book = _bookRepo.GetById(id);
            _bookRepo.StateOfBook(book, stateIsBorrow, stateBorrow);
            _bookRepo.DeleteBorrower(book.BookId);

            object notification = "The book owner has receive the book back";
            return View("Notification", model: notification);
        }
        #endregion

        #region Tạo mới sách
        // GET: Books/Create
        public ActionResult Create()
        {   
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book book, HttpPostedFileBase ImageURL)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;
           
                if (ImageURL != null)
                {

                    if (ImageURL.ContentLength > 0)
                    {
                        
                        var getFileName = Path.GetFileName(ImageURL.FileName);
                        var getFilePath = Path.Combine(Server.MapPath("~/Content/IMG/"), getFileName);
                        if (System.IO.File.Exists(getFilePath))
                        {
                            System.IO.File.Delete(getFilePath);
                        }
                        ImageURL.SaveAs(getFilePath);
                        book.Image = getFileName;
                        book.BookStatus = false;
                        
                }    
                book.Code = Guid.NewGuid();
                _bookRepo.Add(book);
                VerificationEmail(book.User.UserEmail, book.Code.ToString());
                messageRegistration = "Sách của bạn được tạo thành công. Vui lòng kiểm tra email của bạn để xác thực email";
                statusRegistration = true;
                }
               
                else
                {
                    messageRegistration = "Có lỗi đang xảy ra!";
                }
                ViewBag.Message = messageRegistration;
                ViewBag.Status = statusRegistration;

                return RedirectToAction("ThongBao");
        }
        #endregion

        #region Update với OTP


        // GET: Books/Edit/5
        public ActionResult Edit(int id)
        {
            var book = _bookRepo.GetById(id);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book, HttpPostedFileBase ImageURL)
        {
            bool statusRegistration = false;
            string messageRegistration = string.Empty;   
            if (ImageURL != null)
            {
                if(ImageURL.ContentLength>0)
                {
                    var getFileName = Path.GetFileName(ImageURL.FileName);
                    var getFilePath = Path.Combine(Server.MapPath("~/Content/IMG/"), getFileName);
                    if (System.IO.File.Exists(getFilePath))
                    {
                        System.IO.File.Delete(getFilePath);
                    }
                    ImageURL.SaveAs(getFilePath);
                    book.Image = getFileName;
                    book.BookStatus = false;
                }
                Random ran = new Random();

                var OTP = ran.Next(100000, 999999).ToString();
                book.User.OTP = OTP;
                Session["OTP"] = OTP; 
                book.User.ActiveCode = Guid.NewGuid();
                book.BookStatus = false;
                _bookRepo.Update(book);                 
                VerificationEmail(book.User.UserEmail, book.User.OTP.ToString());
                messageRegistration = "Vui lòng xác thực OTP để tạo sách";
                statusRegistration = true;
            }

            else
            {
                messageRegistration = "Có lỗi đang xảy ra!";
            }
            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;

            return RedirectToAction("ActivationOTP");
        }
        

        public ActionResult ThongBao()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ActivationOTP()
        {
            
            return View();
            
        }

        [HttpPost]
        public ActionResult ActivationOTP(Book book)
        {
            
            string sessionOTP = Session["OTP"].ToString();
            if (book.User.OTP == sessionOTP)
            {
                return RedirectToAction("Index");
            }
            else
            {
                book.BookStatus = false;
                return View();
            }
            
            
        }
        #endregion

        #region Gửi xác thực email sau khi add book 
        [HttpGet]
        public ActionResult ActivationBook(string id)
        {
            bool statusBook = false;

            var book = _bookRepo.GetAll().Where(u => u.Code.ToString().Equals(id)).FirstOrDefault();

            if (book != null)
            {
                book.BookStatus = true;
                _bookRepo.Update(book);
                statusBook = true;
            }
            else
            {
                ViewBag.Message = "Có lỗi gì đó !!";
            }
            ViewBag.Status = statusBook;
            return View();
        }

        [NonAction]
        public void VerificationEmail(string email, string activationCode)
        {
            
            var url = string.Format("/Books/ActivationBook/{0}", activationCode);
           
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);

            var fromEmail = new MailAddress("mhoangd2000@gmail.com", "Xác thực Email");
            var toEmail = new MailAddress(email);

            var fromEmailPassword = "minhhoangdm185050247";
            string subject = " Add A Book !";

            string body = "<br/> Truy cập link này để xác nhận đăng kí sách" + activationCode + "<br/><a href='" + link + "'> Xác thực đăng ký sách! </a>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true

            })

                smtp.Send(message);
        }
        #endregion
    }
}
