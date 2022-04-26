﻿using System;
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
                .Where(a => a.BookStatus == true)
                .Where(a => a.IsBorrow == false);
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
                
                book.ActiveCode = Guid.NewGuid();
                _bookRepo.Add(book);
                VerificationEmail(book.OwnerEmail, book.ActiveCode.ToString());
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
                book.OTP = OTP;
                Session["OTP"] = OTP; 
                book.ActiveCode = Guid.NewGuid();
                book.BookStatus = false;
                _bookRepo.Update(book);                 
                VerificationEmail(book.OwnerEmail, book.OTP.ToString());
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
            if (book.OTP == sessionOTP)
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

            var book = _bookRepo.GetAll().Where(u => u.ActiveCode.ToString().Equals(id)).FirstOrDefault();

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
