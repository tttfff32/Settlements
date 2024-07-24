using Settlements.Models;
using Settlements.Services.Interfaces;
using Settlements.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Settlements.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettlementController : ControllerBase
    {
        private ISettlementService _settlementService;

        public SettlementController(ISettlementService settlementService)
        {
            _settlementService = settlementService;
        }

        [HttpGet(Name = "GetSettlements")]
        public ActionResult<IEnumerable<SettlementDTO>> GetSettlements()
        {
            var settlements = _settlementService.GetSettlements();
            return Ok(settlements);
        }

        [HttpGet("{id}", Name = "GetSettlementById")]
        public IActionResult GetSettlementById(int id)
        {
            SettlementDTO settlement = _settlementService.GetSettlementById(id);
            if (settlement == null)
                return NotFound();
            return Ok(settlement);
        }

        [HttpDelete("{id}", Name = "DeleteSettlementById")]
        public IActionResult DeleteSettlementById(int id)
        {
            _settlementService.DeleteSettlement(id);
            return NoContent();
        }

        [HttpPost(Name = "AddSettlement")]
        public IActionResult AddSettlement([FromBody] SettlementDTO newSettlement)
        {
            _settlementService.AddSettlement(newSettlement);
            return CreatedAtRoute("GetSettlements", new { id = newSettlement.Id }, newSettlement);
        }

        [HttpPut("{id}", Name = "UpdateSettlement")]
        public IActionResult UpdateSettlement(int id, [FromBody] SettlementDTO updateSettlement)
        {
            var existingSettlement = _settlementService.GetSettlementById(id);
            if (existingSettlement == null)
                return NotFound();

            updateSettlement.Id = id;
            _settlementService.UpdateSettlement(updateSettlement,id);
            return NoContent();
        }
    }
}
