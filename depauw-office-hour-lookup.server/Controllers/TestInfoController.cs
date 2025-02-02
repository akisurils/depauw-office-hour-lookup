using DePauwOfficeHourLookup.server.Data;
using DePauwOfficeHourLookup.server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DePauwOfficeHourLookup.server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestInfoController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public TestInfoController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<List<TestInfo>> Get()
        {
            return await _appDbContext.TestInfos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<TestInfo> GetById(int id)
        {
            return await _appDbContext.TestInfos.FirstOrDefaultAsync(ti => ti.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TestInfo testInfo)
        {
            if (string.IsNullOrEmpty(testInfo.Name) ||
                string.IsNullOrEmpty(testInfo.Description) ||
                string.IsNullOrEmpty(testInfo.Type))
            {
                return BadRequest("Missing information");
            }

            await _appDbContext.TestInfos.AddAsync(testInfo);
            await _appDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = testInfo.Id }, testInfo);
        }
    }
}
