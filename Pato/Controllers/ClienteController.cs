using Pato.Models;
using Microsoft.AspNetCore.Mvc;
using Pato.Repositories;


namespace Pato.Controllers
{
    public class ClienteController : Controller
    {
        private IClienteRepository repository;

        public ClienteController(IClienteRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            int? idL = HttpContext.Session.GetInt32("id") as int?;
            if(idL == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            List<Cliente> clientes = repository.Read();
            return View(clientes);
        }
        public ActionResult Detalhe(int id)
        {
            int? idL = HttpContext.Session.GetInt32("id") as int?;
            if(idL == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            var cliente = repository.Read(id);
            return View(cliente);
        }

        [HttpGet]
        public ActionResult Create()
        {
            int? idL = HttpContext.Session.GetInt32("id") as int?;
            if(idL == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            repository.Create(cliente);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            int? idL = HttpContext.Session.GetInt32("id") as int?;
            if(idL == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            repository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            int? idL = HttpContext.Session.GetInt32("id") as int?;
            if(idL == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            var cliente = repository.Read(id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Update(int id, Cliente cliente)
        {
            repository.Update(id, cliente);
            return RedirectToAction("Index");
        } 
    }
}