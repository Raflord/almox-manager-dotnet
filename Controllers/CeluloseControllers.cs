using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlmoxManagerApi.Models.Entities;
using AlmoxManagerApi.Data;
using AlmoxManagerApi.Models.Dtos;

namespace AlmoxManagerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CeluloseController(ApplicationDbContext dbContext) : ControllerBase
    {
        private readonly ApplicationDbContext dbContext = dbContext;

        [HttpPost]
        public async Task<ActionResult<Load>> Create(AddLoadDto loadDto)
        {
            var parsedDate = DateTime.Parse(loadDto.CreatedAt);
            
            var loadEntity = new Load()
            {
                Material = loadDto.Material,
                AverageWeight = loadDto.AverageWeight,
                Unit = loadDto.Unit,
                CreatedAt = parsedDate,
                Operator = loadDto.Operator,
                Shift = loadDto.Shift
            };

            await dbContext.Loads.AddAsync(loadEntity);
            await dbContext.SaveChangesAsync();

            return Ok(loadEntity);
        }

        [HttpGet]
        [Route("latest")]
        public async Task<ActionResult<IEnumerable<Load>>> GetLatest()
        {
            return await dbContext.Loads.FromSql($"SELECT * FROM Loads ORDER BY CreatedAt DESC LIMIT 10").ToListAsync();
        }

        [HttpGet]
        [Route("day")]
        public async Task<ActionResult<IEnumerable<Load>>> GetDay()
        {
            DateTime now = DateTime.Now;

            int year = now.Year;
            int month = now.Month;
            int day = now.Day;

            string firstDate = $"{year}-{month}-{day}";
            string seccondDate = $"{year}-{month}-{day+1}";

            var result = await dbContext.LoadSummaries.FromSql($"SELECT Material, SUM(AverageWeight) AS TotalWeight FROM Loads WHERE CreatedAt >= {firstDate} AND CreatedAt < {seccondDate} GROUP BY Material").ToListAsync();

            return Ok(result);
        }

        [HttpPost]
        [Route("filtered")]
        public async Task<ActionResult<IEnumerable<Load>>> GetFiltered(LoadFilteredDto filteredDto)
        {
            // return await dbContext.Loads.FromSql($"SELECT * FROM Loads WHERE (Material = {filteredDto.Material} OR NULLIF({filteredDto.Material}, '') IS NULL) AND (DATE(CreatedAt) BETWEEN {filteredDto.FirstDate} AND {filteredDto.SeccondDate} OR ({filteredDto.FirstDate} IS NULL AND {filteredDto.SeccondDate} IS NULL)) ORDER BY CreatedAt ASC").ToListAsync();
            return await dbContext.Loads.FromSql($"SELECT * FROM Loads WHERE (Material = {filteredDto.Material} OR NULLIF({filteredDto.Material}, '') IS NULL) AND (({filteredDto.FirstDate} IS NOT NULL AND {filteredDto.SeccondDate} IS NOT NULL AND DATE(CreatedAt) BETWEEN {filteredDto.FirstDate} AND {filteredDto.SeccondDate}) OR ({filteredDto.FirstDate} IS NOT NULL AND {filteredDto.SeccondDate} IS NULL AND DATE(CreatedAt) >= {filteredDto.FirstDate}) OR ({filteredDto.FirstDate} IS NULL AND {filteredDto.SeccondDate} IS NULL))ORDER BY CreatedAt ASC;").ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(Guid id)
        {
            var load = await dbContext.Loads.FindAsync(id);
            if (load == null) return NotFound();

            dbContext.Loads.Remove(load);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateLoadDto updatedLoad)
        {
            var load = await dbContext.Loads.FindAsync(id);
            if (load == null) return NotFound();

            var parsedDate = DateTime.Parse(updatedLoad.CreatedAt);

            load.Material = updatedLoad.Material;
            load.CreatedAt = parsedDate;
            load.Operator = updatedLoad.Operator;
            load.Shift = updatedLoad.Shift;

            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
