using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EquipmentRentalApplication.Dal;
using EquipmentRentalApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EquipmentRentalApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly EquipmentContext _context;

        public EquipmentController(EquipmentContext context)
        {
            _context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<List<Equipment>> Get()
        {
            //_context.Equipments.Load();
            var equipments = await _context.Equipments.ToListAsync();
            return equipments;
        }
    }
}
