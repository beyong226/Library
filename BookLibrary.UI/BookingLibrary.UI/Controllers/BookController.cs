﻿using BookingLibrary.UI.DTOs;
using BookingLibrary.UI.Utilities;
using BookingLibrary.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingLibrary.UI.Controllers
{
    [Authorize]
    public class BookController : Controller
    {
        private string _repositoryApiBaseUrl => ConfigurationManager.AppSettings["repositoryApiUrl"];

        [HttpGet]
        public ActionResult List()
        {
            var data = ApiRequestWithFormUrlEncodedContent.Get<List<BookViewModel>>($"{_repositoryApiBaseUrl}/api/Books");
            return View(data);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var data = ApiRequestWithFormUrlEncodedContent.Get<EditBookDTO>($"{_repositoryApiBaseUrl}/api/Books/{id}");

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Guid id, EditBookDTO dto)
        {
            var data = new NameValueCollection();

            data.Add("BookId", dto.BookId.ToString());
            data.Add("BookName", dto.BookName);
            data.Add("ISBN", dto.ISBN);
            data.Add("IssueDate", dto.DateIssued.ToString("yyyy-MM-dd"));
            data.Add("Description", dto.Description);

            var commandId = ApiRequestWithFormUrlEncodedContent.Put<Guid>($"{_repositoryApiBaseUrl}/api/Books/{dto.BookId}", data);

            if (commandId != Guid.Empty)
            {
                return RedirectToAction("List");
            }
            else
            {
                return View(dto);
            }
        }

        [HttpPost]
        public ActionResult Add(AddBookDTO dto)
        {
            var data = new NameValueCollection();

            data.Add("BookName", dto.BookName);
            data.Add("ISBN", dto.ISBN);
            data.Add("IssueDate", dto.IssueDate.ToString("yyyy-MM-dd"));
            data.Add("Description", dto.Description);

            var commandUnqiueId = ApiRequestWithFormUrlEncodedContent.Post<Guid>($"{_repositoryApiBaseUrl}/api/Books", data);

            return Json(new { commandUnqiueId = commandUnqiueId });
        }

        [HttpGet]
        public ActionResult _AjaxGetAvailableBooks()
        {
            var data = ApiRequestWithFormUrlEncodedContent.Get<List<AvailableBookModel>>($"{_repositoryApiBaseUrl}/api/available_books");
            return Json(data, JsonRequestBehavior.AllowGet);
        }

       
    }
}