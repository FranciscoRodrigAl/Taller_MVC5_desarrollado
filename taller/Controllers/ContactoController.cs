using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using taller.Models;
using Datos;

namespace taller.Controllers
{
    public class ContactoController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new Persona();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Persona model)
        {
            if (model == null)
                model = new Persona();

            if (ModelState.IsValid)
            {
                //IMplementacion de insert
                using (var bd = new ContactoEntities())
                {
                    var entity = bd.Table_2.Create();
                    entity.Comentarios = model.Comentario;
                    entity.Email = model.Email;
                    entity.Nombre = model.Nombre;
                    entity.Telefono = model.Telefono;

                    bd.Table_2.Add(entity);

                    var resultado = bd.SaveChanges() == 1 ? true : false;

                    bd.SaveChanges();
                }
            }
            return RedirectToAction("ListContacto");
        }

        public ActionResult ListContacto()
        {
            IList<Persona> model = new List<Persona>();
            //Conectamos la BD

            using (var bd = new ContactoEntities())
            {
                var entidades = bd.Table_2.ToList();
                foreach ( var item in entidades)
                {
                    var m = new Persona();
                    m.Comentario = item.Comentarios;
                    m.Email = item.Email;
                    m.Nombre = item.Nombre;
                    m.Telefono = item.Telefono;
                    model.Add(m);
                }
            }
            return View(model);
        }
        
    }
}