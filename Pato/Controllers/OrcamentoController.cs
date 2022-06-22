using Pato.Models;
using Microsoft.AspNetCore.Mvc;
using Pato.Repositories;

namespace Pato.Controllers
{
    public class OrcamentoController : Controller
    {
        private IOrcamentoRepository repository;
        private IClienteRepository clienteRepository;

        public OrcamentoController(IOrcamentoRepository repository, IClienteRepository clienteRepository)
        {
            this.repository = repository;
            this.clienteRepository = clienteRepository;
        }

        public ActionResult Index()
        {
            int? idL = HttpContext.Session.GetInt32("id") as int?;
            if(idL == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            ViewBag.Clientes = clienteRepository.Read();
            List<Orcamento> orcamentos = repository.Read();
            return View(orcamentos);
        }

        public ActionResult Filter(int id)
        {
            int? idL = HttpContext.Session.GetInt32("id") as int?;
            if(idL == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            ViewBag.Clientes = clienteRepository.Read();
            List<Orcamento> orcamentos = repository.ReadByCliente(id);
            return View("Index", orcamentos);
        }

        [HttpGet]
        public ActionResult Create()
        {
            int? idL = HttpContext.Session.GetInt32("id") as int?;
            if(idL == null)
            {
                return RedirectToAction("Login", "Usuario");
            }
            ViewBag.Clientes = clienteRepository.Read();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Orcamento orcamento)
        {
            repository.Create(orcamento);
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
            ViewBag.Clientes = clienteRepository.Read();
            var orcamento = repository.Read(id);
            return View(orcamento);
        }

        [HttpPost]
        public ActionResult Update(int id, Orcamento orcamento)
        {
            repository.Update(id, orcamento);
            return RedirectToAction("Index");
        } 
    }
}