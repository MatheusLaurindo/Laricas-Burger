using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace LanchesMac.Controllers
{
    [Authorize]
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        [AllowAnonymous]
        public IActionResult List(string categoria)
        {
            IEnumerable<Lanche> lanches;
            string categorialAtual = string.Empty;

            if (string.IsNullOrEmpty(categoria))
            {
                lanches = _lancheRepository.Lanches.OrderBy(x => x.LancheId);
                categorialAtual = "Todos os lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches
                    .Where(x => x.Categoria.CategoriaNome.Equals(categoria))
                    .OrderBy(x => x.Nome);

                categorialAtual = categoria;
            }

            var lanchesListViewModel = new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categorialAtual
            };

            return View(lanchesListViewModel);
        }

        [AllowAnonymous]
        public IActionResult Details(int lancheId)
        {
            var lanches = _lancheRepository.Lanches.FirstOrDefault(x => x.LancheId == lancheId);
            return View(lanches);
        }

        [AllowAnonymous]
        public ViewResult Search(string searchString)
        {
            IEnumerable<Lanche> lanches;
            string categoriaAtual = string.Empty;

            if (string.IsNullOrEmpty(searchString))
            {
                lanches = _lancheRepository.Lanches.OrderBy(x => x.LancheId);
                categoriaAtual = "Todos os lanches";
            }
            else
            {
                lanches = _lancheRepository.Lanches
                    .Where(x => x.Nome.ToLower().Contains(searchString.ToLower()));

                if (lanches.Any())
                {
                    categoriaAtual = "Lanches";
                }
                else
                {
                    categoriaAtual = "Nenhum lanche foi encontrado";
                }
            }

            return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
            {
                Lanches = lanches,
                CategoriaAtual = categoriaAtual
            });
        }
    }
}
