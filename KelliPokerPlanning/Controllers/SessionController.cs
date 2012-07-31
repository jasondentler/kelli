﻿using System;
using System.Linq;
using System.Web.Mvc;
using KelliPokerPlanning.Models;
using Microsoft.Web.Mvc;
using MvcContrib.Filters;

namespace KelliPokerPlanning.Controllers
{
    public class SessionController : Controller
    {
        private readonly IAccountManager _accountManager;

        public SessionController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        [HttpGet, ModelStateToTempData]
        public ViewResult Index(string id)
        {

            var settings = _accountManager.GetAccountSettings(id);
            if (settings != null)
            {
                return View(new PokerSetup()
                                {
                                    UserName = settings.UserName,
                                    Values = string.Join(Environment.NewLine, settings.Values),
                                    IncludeQuestion = settings.IncludeQuestionMark,
                                    IncludeInfinity = settings.IncludeInfinity
                                });
            }

            var values = new[] { "XS", "S", "M", "L", "XL", "2X" };
            return View(new PokerSetup()
            {
                UserName = id,
                Values = string.Join(Environment.NewLine, values),
                IncludeQuestion = true,
                IncludeInfinity = true
            });
        }

        [HttpPost, ModelStateToTempData]
        public ActionResult Index(PokerSetup model)
        {
            if (!ModelState.IsValid)
                return this.RedirectToAction(c => c.Index(model.UserName));

            var values = (model.Values ?? "")
                .Split(Environment.NewLine.ToCharArray())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();

            var documentId = _accountManager.Create(model.UserName, values, model.IncludeQuestion, model.IncludeInfinity);

            return Content("OK");
        }

    }
}
