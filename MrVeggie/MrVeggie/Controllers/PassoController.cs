using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrVeggie.Models;

namespace MrVeggie.Controllers
{
    public class PassoController : Controller
    {

        private readonly ReceitaContext _context;

        public PassoController(ReceitaContext context)
        {
            _context = context;
        }
    }
}