using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Areas.Sales.Controllers
{
    [Authorize]
    [Area("Sales")]
    public class ShipmentsController : Controller
    {
        // GET: ShipmentsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ShipmentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShipmentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShipmentsController/Create
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

        // GET: ShipmentsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShipmentsController/Edit/5
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

        // GET: ShipmentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShipmentsController/Delete/5
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
