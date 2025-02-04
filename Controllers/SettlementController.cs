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

        [HttpGet("page/{pageNumber}")]
        public IActionResult GetSettlements(int pageNumber, [FromQuery] int pageSize = 5)
        {
            var settlements = _settlementService.GetSettlementsForPage(pageNumber, pageSize);
            var totalPages = _settlementService.GetTotalPages(pageSize);

            return Ok(new
            {
                Settlements = settlements,
                TotalPages = totalPages,
                CurrentPage = pageNumber
            });
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
            if (_settlementService.AddSettlement(newSettlement))
                return CreatedAtRoute("GetSettlements", new { id = newSettlement.Id }, newSettlement);
            else
                return Conflict(new { message = "Settlement already exists." });
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

        [HttpGet("filter/{search?}", Name ="FilterSettlements")]
        public async Task<IActionResult> FilterSettlements(string search = "")
        {
            var result = await _settlementService.FilterSettlements(search);
            return Ok(result);
        }
    }
}
