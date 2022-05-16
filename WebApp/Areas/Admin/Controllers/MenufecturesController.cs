using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Admin.Controllers
{
    public class MenufecturesController : Controller
    {
        // GET: MenufecturesController
        public ActionResult Index()
        {
            return View();
        }

        // GET: MenufecturesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MenufecturesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenufecturesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenufecturesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MenufecturesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MenufecturesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MenufecturesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
