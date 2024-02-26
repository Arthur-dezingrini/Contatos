using Contatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Contatos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiController _apiController;
        static List<IndexModel> ListaContatos = new List<IndexModel>();

        public HomeController(ILogger<HomeController> logger, ApiController apiController)
        {
            _logger = logger;
            _apiController = apiController;
        }

        public async Task<IActionResult> Index()
        {
            List<Estado> estados = await _apiController.GetEstadosAsync();
            ViewBag.Estados = new SelectList(estados, "Sigla", "Nome");
            ViewBag.Municipios = new SelectList(new List<Municipio>(), "Nome");

            return View(ListaContatos);
        }

        [HttpPost]
        public async Task<JsonResult> GetMunicipios(string estado)
        {
            List<Municipio> municipios = await _apiController.GetMunicipiosPorEstadoAsync(estado);
            return Json(municipios);
        }

        [HttpPost]
        public ActionResult AdicionarContato(IndexModel novoContato)
        {
            ListaContatos.Add(novoContato);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }

            IndexModel contato = ListaContatos.FirstOrDefault(x => x.Id == id);


            if (contato == null)
            {
                return NotFound();
            }

            return View(contato);
        }

        [HttpPost]
        public IActionResult Editar(IndexModel contato)
        {
            if (ModelState.IsValid)
            {
                int index = ListaContatos.FindIndex(x => x.Id == contato.Id);
                if (index != -1)
                {
                    ListaContatos[index] = contato;
                    return RedirectToAction("Index", ListaContatos);
                }
                else
                {
                    return NotFound();
                }
            }
            return View(contato);
        }


        [HttpPost]
        public ActionResult ExcluirContato(int id)
        {
            var contatoParaExcluir = ListaContatos.FirstOrDefault(c => c.Id == id);
            if (contatoParaExcluir != null)
            {
                ListaContatos.Remove(contatoParaExcluir);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

