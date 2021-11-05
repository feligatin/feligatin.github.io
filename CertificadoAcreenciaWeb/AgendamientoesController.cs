using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CertificadoAcreenciaWeb.Models;

namespace CertificadoAcreenciaWeb
{
    public class AgendamientoesController : Controller
    {
        private CertificadoAcreenciaAgendamientoContext db = new CertificadoAcreenciaAgendamientoContext();

        // GET: Agendamientoes
        public ActionResult Index()
        {
            return View(db.Agendamientoes.ToList());
        }

        // GET: Agendamientoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamiento agendamiento = db.Agendamientoes.Find(id);
            if (agendamiento == null)
            {
                return HttpNotFound();
            }
            return View(agendamiento);
        }

        // GET: Agendamientoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agendamientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Mes,Dia,Hora,Clie,Esta,Prac")] Agendamiento agendamiento)
        {
            if (ModelState.IsValid)
            {
                db.Agendamientoes.Add(agendamiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agendamiento);
        }

        // GET: Agendamientoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamiento agendamiento = db.Agendamientoes.Find(id);
            if (agendamiento == null)
            {
                return HttpNotFound();
            }
            return View(agendamiento);
        }

        // POST: Agendamientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Mes,Dia,Hora,Clie,Esta,Prac")] Agendamiento agendamiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(agendamiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agendamiento);
        }

        // GET: Agendamientoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agendamiento agendamiento = db.Agendamientoes.Find(id);
            if (agendamiento == null)
            {
                return HttpNotFound();
            }
            return View(agendamiento);
        }

        // POST: Agendamientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Agendamiento agendamiento = db.Agendamientoes.Find(id);
            db.Agendamientoes.Remove(agendamiento);
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
