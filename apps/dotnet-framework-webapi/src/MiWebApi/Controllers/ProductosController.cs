using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MiWebApi.Models;

namespace MiWebApi.Controllers
{
    [RoutePrefix("api/productos")]
    public class ProductosController : ApiController
    {
        // Datos de ejemplo en memoria
        private static readonly List<Producto> _productos = new List<Producto>
        {
            new Producto { Id = 1, Nombre = "Laptop",   Precio = 1200.00m, Stock = 10 },
            new Producto { Id = 2, Nombre = "Monitor",  Precio = 350.00m,  Stock = 25 },
            new Producto { Id = 3, Nombre = "Teclado",  Precio = 45.00m,   Stock = 100 },
            new Producto { Id = 4, Nombre = "Mouse",    Precio = 20.00m,   Stock = 150 },
        };

        private static readonly object _lock = new object();

        // GET api/productos
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            lock (_lock)
            {
                return Ok(_productos.ToList());
            }
        }

        // GET api/productos/1
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            lock (_lock)
            {
                var producto = _productos.FirstOrDefault(p => p.Id == id);
                if (producto == null)
                    return NotFound();

                return Ok(producto);
            }
        }

        // POST api/productos
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] Producto producto)
        {
            if (producto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            lock (_lock)
            {
                producto.Id = _productos.Any() ? _productos.Max(p => p.Id) + 1 : 1;
                _productos.Add(producto);
            }
            return Created($"api/productos/{producto.Id}", producto);
        }

        // PUT api/productos/1
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, [FromBody] Producto producto)
        {
            if (producto == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            lock (_lock)
            {
                var existente = _productos.FirstOrDefault(p => p.Id == id);
                if (existente == null)
                    return NotFound();

                existente.Nombre = producto.Nombre;
                existente.Precio = producto.Precio;
                existente.Stock  = producto.Stock;
                return Ok(existente);
            }
        }

        // DELETE api/productos/1
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            lock (_lock)
            {
                var producto = _productos.FirstOrDefault(p => p.Id == id);
                if (producto == null)
                    return NotFound();

                _productos.Remove(producto);
            }
            return Ok(new { mensaje = $"Producto {id} eliminado correctamente." });
        }
    }
}
