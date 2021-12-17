using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiMonedas.Models;

namespace ApiMonedas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriptomonedasController : ControllerBase
    {
        private readonly CriptomonedasContext _context;

        public CriptomonedasController(CriptomonedasContext context)
        {
            _context = context;
        }

        // GET: api/Criptomonedas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Criptomonedas>>> GetCriptomonedas()
        {
            var ran = new Random();
            
            foreach (var m in _context.Criptomonedas)
            {
                // Añadir un valor random al valor Ultimo
                m.Ultimo = (decimal)ran.NextDouble() * 100;
                // Actualizar el valor Maximo, si Ultimo > Maximo
                if (m.Maximo < m.Ultimo)
                {
                    m.Maximo = m.Ultimo;
                }
            }
            _context.SaveChanges();

            return await _context.Criptomonedas.ToListAsync();
        }

        // GET: api/Criptomonedas/5
        // Filtro por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Criptomonedas>> GetCriptomonedas(long id)
        {
            var criptomonedas = await _context.Criptomonedas.FindAsync(id);

            if (criptomonedas == null)
            {
                return NotFound();
            }

            var ran = new Random();

            // Añadir un valor random al valor Ultimo
            criptomonedas.Ultimo = (decimal)ran.NextDouble() * 100;
            // Actualizar el valor Maximo, si Ultimo > Maximo
            if (criptomonedas.Maximo < criptomonedas.Ultimo)
            {
                criptomonedas.Maximo = criptomonedas.Ultimo;
            }

            _context.SaveChanges();

            return criptomonedas;
        }

        // GET: api/Criptomonedas/1234.7
        // Filtro por numero 
        [HttpGet("filtro/{numero}")]
        public async Task<IEnumerable<Criptomonedas>> GetCriptomonedasFiltro(decimal numero)
        {

            return await _context.Criptomonedas.Where(t => t.Maximo > numero ).ToListAsync();

        }

        // POST: api/Criptomonedas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Criptomonedas>> PostCriptomonedas(Criptomonedas criptomonedas)
        {
            _context.Criptomonedas.Add(criptomonedas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCriptomonedas", new { id = criptomonedas.Id }, criptomonedas);
        }

    }
}
