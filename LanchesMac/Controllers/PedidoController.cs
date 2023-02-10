using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra)
        {
            _pedidoRepository = pedidoRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult CheckOut()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();

            }
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public IActionResult CheckOut(Pedido pedido)
        {
            if (User.Identity.IsAuthenticated)
            {
                int totalItensPedido = 0;
                decimal precoTotalPedido = 0.0m;

                List<CarrinhoCompraItem> itens = _carrinhoCompra.GetCarrinhoCompraItens();
                _carrinhoCompra.CarrinhoCompraItems = itens;

                if (_carrinhoCompra.CarrinhoCompraItems.Count() == 0)
                {
                    ModelState.AddModelError("", "Seu carrinho está vazio.");
                }

                foreach (var item in itens)
                {
                    totalItensPedido += item.Quantidade;
                    precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
                }

                pedido.TotalItensPedido = totalItensPedido;
                pedido.PedidoTotal = precoTotalPedido;

                if (ModelState.IsValid)
                {
                    _pedidoRepository.CriarPedido(pedido);

                    ViewBag.CheckoutCompletoMensagem = "Obrigado pelo pedido :)";
                    ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

                    _carrinhoCompra.LimparCarrinho();

                    return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
                }
                return View(pedido);
            }
            return RedirectToAction("Login", "Account");
        }
    }
}
