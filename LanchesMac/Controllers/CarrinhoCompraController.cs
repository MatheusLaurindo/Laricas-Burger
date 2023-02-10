using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using LanchesMac.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancheRepository, 
            CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var itens = _carrinhoCompra.GetCarrinhoCompraItens();
                _carrinhoCompra.CarrinhoCompraItems = itens;

                var carrinhoCompraVM = new CarrinhoCompraViewModel
                {
                    CarrinhoCompra = _carrinhoCompra,
                    CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
                };

                return View(carrinhoCompraVM);
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AdicionarItemNoCarrinhoCompra(int lancheId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var lancheSelecionado = _lancheRepository.Lanches
                                    .FirstOrDefault(p => p.LancheId == lancheId);

                if (lancheSelecionado != null)
                {
                    _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account");
        }

        public IActionResult RemoverItemDoCarrinhoCompra(int lancheId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var lancheSelecionado = _lancheRepository.Lanches
                                    .FirstOrDefault(p => p.LancheId == lancheId);

                if (lancheSelecionado != null)
                {
                    _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
                }
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login", "Account");
        }
    }
}