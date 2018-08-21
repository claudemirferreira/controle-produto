using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiPoc.Models;

namespace WebApiPoc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ProductContext _context;
        public ProductController(ProductContext context)
        {
            _context = context;

            if (_context.Products.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Products.Add(new Product { Nome = "Item1" });
                _context.SaveChanges();
            }
        }


        // GET: Product
        [HttpGet()]
        [Route("listarTodosProdutos")]
        public ActionResult<List<Product>> Get()
        {
            return _context.Products.ToList();
        }

        // GET: Product/Details/5
        [HttpGet("{id}")]
        [Route("obterProdutoId/{id}")]
        public ActionResult<Product> GetById(long id)
        {
            var item = _context.Products.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item; 
        }
        [HttpPost()]
        [Route("salvarProduto")]
        public IActionResult Create(Product item)
        {
            _context.Products.Add(item);
            _context.SaveChanges();

            return Ok(item);
        }

        [HttpPut("{id}")]
        [Route("alterarProduto/{id}")]
        public IActionResult Update(long id, Product item)
        {
            var todo = _context.Products.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Nome = item.Nome;
            todo.Valor = item.Valor;
            todo.Descricao = item.Descricao;

            _context.Products.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Route("excluirProduto/{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Products.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Products.Remove(todo);
            _context.SaveChanges();

            return NoContent();
        }
    }
}