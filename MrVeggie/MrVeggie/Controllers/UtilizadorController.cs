using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Models;
using MrVeggie.Shared;

namespace Teste.Controllers { 

    [Route("api/[controller]")]
    public class UtilizadorController : Controller {


        private UtilizadorHandling utilizador_handling;

        public UtilizadorController(UtilizadorContext context) {
            //_context = context;
            utilizador_handling = new UtilizadorHandling(context);
        }

        [Authorize]
        [HttpGet]
        public Utilizador[] Get() {
            return utilizador_handling.getUtilizadores();
        }



        /*
        private readonly UtilizadorContext _context;

        public UtilizadorController(UtilizadorContext context)
        {
            _context = context;
        }


        [HttpGet]
        public Utilizador[] Get() {
            return _context.utilizador.ToArray();
        }


        [HttpGet("{id}")]
        public ActionResult Get(int id) {
            var utilizador = _context.utilizador.Find(id);

            if (utilizador == null) {
                return NotFound();
            }

            return Ok(utilizador);
        }


        [HttpPost]
        public IActionResult Add([FromBody] Utilizador utilizador) {
            _context.utilizador.Add(utilizador);
            _context.SaveChanges();
            return new CreatedResult($"/api/user/{utilizador.id}", utilizador);
        }

        [HttpDelete]
        public IActionResult Delete([FromQuery] int id){
            var utilizador = _context.utilizador.Find(id);

            if (utilizador == null) {
                return NotFound();
            }
            _context.utilizador.Remove(utilizador);
            _context.SaveChanges();

            return NoContent();
        }
        */
    }
}