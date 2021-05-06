using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SNSW_API.Data;
using SNSW_API.Models;
using Microsoft.AspNetCore.Cors;

namespace SNSW_API.Controllers
{
    [Route("snsw/api/v1/[controller]")]
    [ApiController]
    [EnableCors("SNSW_API_Policy")]
    public class RegistrationsController : ControllerBase
    {
        private readonly SNSWContext _context;

        public RegistrationsController(SNSWContext context)      // inject DB context
        {
            _context = context;
        }

        // GET: api/registrations            //  retrieve the root node (registrations)
        [HttpGet]
        public IEnumerable<Registration> GetRegistrations()
        {
            return _context.Registrations
            .Include(r => r.RegistrationDetails)                // attach "registration" details child node
            .Include(v => v.Vehicles)                           // attach vehicle child node
            .Include(i => i.Insurers);                          // attach insurer child node
        }

        // GET: api/registration/[plate_number]          // get each plate_number node and it's child nodes
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistration([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registration = await _context.Registrations
              .Include(r => r.RegistrationDetails)
              .Include(v => v.Vehicles)
              .Include(i => i.Insurers)
              .FirstOrDefaultAsync(i => i.Plate_number == id);

            if (registration == null)
            {
                return NotFound();
            }

            return Ok(registration);
        }

        // PUT: api/registration/[plate_number]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRegistration([FromRoute] string id, [FromBody] Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registration.Plate_number)
            {
                return BadRequest();
            }

            _context.Entry(registration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RegistrationExists(id))
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

        // POST: api/registrations
        [HttpPost]
        public async Task<IActionResult> PostRegistration([FromBody] Registration registration)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRegistration", new { id = registration.Plate_number }, registration);
        }

        // DELETE: api/registrations/[plate_number]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registration = await _context.Registrations.FindAsync(id);
            if (registration == null)
            {
                return NotFound();
            }

            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();

            return Ok(registration);
        }

        private bool RegistrationExists(string id)
        {
            return _context.Registrations.Any(e => e.Plate_number == id);
        }

        // GET api/registrations/[plate_number]/vehicle
        [HttpGet("{id:int}/vehicle")]
        public async Task<IActionResult> GetVehicles(string id)
        {
            var registration = await _context.Registrations
              .Include(m => m.Vehicles)
              .FirstOrDefaultAsync(i => i.Plate_number == id);

            if (registration == null)
                return NotFound();

            return Ok(registration.Vehicles);
        }

        // GET api/registrations/[plate_number]/insurer
        [HttpGet("{id:int}/insurer")]
        public async Task<IActionResult> GetInsurers(string id)
        {
            var registration = await _context.Registrations
              .Include(m => m.Insurers)
              .FirstOrDefaultAsync(i => i.Plate_number == id);

            if (registration == null)
                return NotFound();

            return Ok(registration.Insurers);
        }

        // GET api/registrations/[plate_number]/registration
        [HttpGet("{id:int}/registration")]
        public async Task<IActionResult> GetRegistrationDetails(string id)
        {
            var registration = await _context.Registrations
              .Include(r => r.RegistrationDetails)
              .FirstOrDefaultAsync(i => i.Plate_number == id);

            if (registration == null)
                return NotFound();

            return Ok(registration.RegistrationDetails);
        }

    }
}