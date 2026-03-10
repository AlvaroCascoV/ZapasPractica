using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using ZapasPractica.Models;
using ZapasPractica.Repositories;
using static System.Net.Mime.MediaTypeNames;

namespace ZapasPractica.Controllers
{
    public class HomeController : Controller
    {
        private RepositoryZapas repo;

        public HomeController(RepositoryZapas repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Zapatilla> zapas = await this.repo.GetZapatillasAsync();
            return View(zapas);
        }

        public async Task<IActionResult> Details(int? posicion, int idzapatilla)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            int numeroRegistros = await this.repo.GetNumeroImagenesAsync(idzapatilla);
            int siguiente = posicion.Value + 1;

            if (siguiente > numeroRegistros)
            {
                siguiente = numeroRegistros;
            }

            int anterior = posicion.Value - 1;

            if (anterior < 1)
            {
                anterior = 1;
            }

            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["ULTIMO"] = numeroRegistros;
            ViewData["POSICION"] = posicion;

            Zapatilla zapatilla = await this.repo.FindZapatillaAsync(idzapatilla);

            return View(zapatilla);
        }
        public async Task<IActionResult> _ZapatillasDetails(int idzapatilla, int posicion)
        {
            ImagenZapatilla imagen = await this.repo.GetImagenPosicionAsync(posicion, idzapatilla);

            return PartialView("_ZapatillasDetailsView", imagen);
        }
    }
}
