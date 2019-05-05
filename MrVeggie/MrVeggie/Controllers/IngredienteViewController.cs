using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Models;
using MrVeggie.Shared;

namespace MrVeggie.Controllers {

    [Route("[controller]/[action]")]
    public class IngredienteViewController : Controller {

        private IngredienteHandling ingrediente_handling;

        public IngredienteViewController(IngredienteContext context) {
            ingrediente_handling = new IngredienteHandling(context);
        }


        [HttpGet]
        public IActionResult showIngredientes() {
            Ingrediente[] ingredientes = ingrediente_handling.getIngredientes();

            return View(ingredientes);
        }
    }
}