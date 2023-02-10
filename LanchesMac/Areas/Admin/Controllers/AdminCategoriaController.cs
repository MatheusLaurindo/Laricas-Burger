using LanchesMac.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using LanchesMac.Models;
using System.Reflection.Metadata;

namespace LanchesMac.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCategoriaController : Controller
    {
        private readonly AppDbContext _context;

        public AdminCategoriaController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var listaCategorias = _context.Categorias.ToList();
            return View(listaCategorias);
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Adicionar(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _context.Categorias.Add(categoria);
                _context.SaveChanges();
            }
            else
            {
                return BadRequest();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Editar(long id)
        {
            var categoria = _context.Categorias.FirstOrDefault(x => x.CategoriaId == id);

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Editar(Categoria categoria)
        {
            var categoriaExistente = _context.Categorias.FirstOrDefault(x => x.CategoriaId == categoria.CategoriaId);

            if (categoriaExistente != null)
            {
                _context.Entry(categoriaExistente).CurrentValues.SetValues(categoria);
                _context.Entry(categoriaExistente).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Deletar(long id)
        {
            var categoria = _context.Categorias.FirstOrDefault(x => x.CategoriaId == id);

            return View(categoria);
        }

        [HttpPost]
        public IActionResult Deletar(Categoria categoria)
        {
            if (categoria != null)
            {
                var categoriaExistente = _context.Categorias.FirstOrDefault(x => x.CategoriaId == categoria.CategoriaId);
                _context.Remove(categoriaExistente);
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}

