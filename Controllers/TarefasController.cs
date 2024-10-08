using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_Task.DBContext;
using API_Task.Models;
using API_Task.DTO;

namespace API_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly AppDBContext _context;

        public TarefasController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Tarefas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> Gettarefas()
        {
            if (_context.tarefas == null)
            {
                return NotFound();
            }
            return await _context.tarefas.ToListAsync();
        }

        // GET: api/Tarefas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetTarefa(int id)
        {
            if (_context.tarefas == null)
            {
                return NotFound();
            }
            var tarefa = await _context.tarefas.FindAsync(id);

            if (tarefa == null)
            {
                return NotFound();
            }

            return tarefa;
        }

        [HttpGet("usuario/{id}")]
        public async Task<ActionResult<IEnumerable<TarefaDTO>>> GetTarafesPorUsuario(int id)
        {
            var tarefas = _context.tarefas.Where(t => t.fk_usuario == id).ToList();

            if (tarefas == null || tarefas.Count == 0)
            {
                return NotFound(new { message = "Nenhuma tarefa encontrada para este usuário." });
            }

            List<TarefaDTO> tarefasDTO = new List<TarefaDTO>();
            foreach (var item in tarefas)
            {
                tarefasDTO.Add(
                    new TarefaDTO
                    {
                        id_tarefa = item.id_tarefa,
                        data_conclusao = item.data_conclusao,
                        data_prevista = item.data_prevista,
                        descricao_tarefa = item.descricao_tarefa,
                        fk_usuario = item.fk_usuario
                    });
            }
            return Ok(tarefasDTO);
        }

        // PUT: api/Tarefas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, TarefaDTO tarefaDTO)
        {
            if (id != tarefaDTO.id_tarefa)
            {
                return BadRequest();
            }

            try
            {
                var tarefa = new Tarefa
                {
                    id_tarefa = tarefaDTO.id_tarefa,
                    data_conclusao = tarefaDTO.data_conclusao,
                    data_prevista = tarefaDTO.data_prevista,
                    descricao_tarefa = tarefaDTO.descricao_tarefa,
                    fk_usuario = tarefaDTO.fk_usuario
                };
                _context.tarefas.Update(tarefa);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tarefas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TarefaDTO>> PostTarefa(TarefaDTO tarefaDTO)
        {
            if (_context.tarefas == null)
            {
                return Problem("Entity set 'AppDBContext.Tarefas'  is null.");
            }

            var tarefa = new Tarefa
            {
                id_tarefa = tarefaDTO.id_tarefa,
                descricao_tarefa = tarefaDTO.descricao_tarefa,
                data_conclusao = tarefaDTO.data_conclusao,
                data_prevista = tarefaDTO.data_prevista,
                fk_usuario = tarefaDTO.fk_usuario
            };

            _context.tarefas.Add(tarefa);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarefa", new { id = tarefa.id_tarefa }, tarefa);
        }

        // DELETE: api/Tarefas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            if (_context.tarefas == null)
            {
                return NotFound();
            }
            var tarefa = await _context.tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _context.tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarefaExists(int id)
        {
            return (_context.tarefas?.Any(e => e.id_tarefa == id)).GetValueOrDefault();
        }
    }
}
