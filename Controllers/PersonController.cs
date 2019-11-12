using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NV2.Model;


namespace NV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public PersonController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOwners()
        {
            try
            {
                var person = await _repository.Owner.GetAllOwnersAsync();
                _logger.LogInfo($"Returned all owners from database.");

                var ownersResult = _mapper.Map<IEnumerable<PersonDto>>(person);
                return Ok(ownersResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllOwners action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOwnerById(int id)
        {
            try
            {
                var owner = await _repository.Owner.GetOwnerByIdAsync(id);
                if (owner == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned owner with id: {id}");

                    var ownerResult = _mapper.Map<PersonDto>(owner);
                    return Ok(ownerResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

    //    [HttpGet("{id}/account")]
    //    public async Task<IActionResult> GetOwnerWithDetails(Guid id)
    //    {
    //        try
    //        {
    //            var owner = await _repository.Owner.GetOwnerWithDetailsAsync(id);
    //            if (owner == null)
    //            {
    ////                _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
    //                return NotFound();
    //            }
    //            else
    //            {
    ////                _logger.LogInfo($"Returned owner with details for id: {id}");

    //                var personResult = _mapper.Map<PersonDto>(owner);
    //                return Ok(personResult);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    ////            _logger.LogError($"Something went wrong inside GetOwnerWithDetails action: {ex.Message}");
    //            return StatusCode(500, "Internal server error");
    //        }
    //    }

        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromBody]PersonDto owner)
        {
            try
            {
                if (owner == null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var ownerEntity = _mapper.Map<Person>(owner);

                _repository.Owner.CreateOwner(ownerEntity);
                await _repository.SaveAsync();

                var createdOwner = _mapper.Map<PersonDto>(ownerEntity);

                return NoContent();

     //           return CreatedAtRoute("OwnerById", new { id = createdOwner.BusinessEntityId }, createdOwner);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOwner(int id, [FromBody]PersonDto owner)
        {
            try
            {
                if (owner == null)
                {
                    _logger.LogError("Owner object sent from client is null.");
                    return BadRequest("Owner object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid owner object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var ownerEntity = await _repository.Owner.GetOwnerByIdAsync(id);
                if (ownerEntity == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _mapper.Map(owner, ownerEntity);

                _repository.Owner.UpdateOwner(ownerEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            try
            {
                var owner = await _repository.Owner.GetOwnerByIdAsync(id);
                if (owner == null)
                {
                    _logger.LogError($"Owner with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                //if (_repository.Account.AccountsByOwner(id).Any())
                //{
                //    _logger.LogError($"Cannot delete owner with id: {id}. It has related accounts. Delete those accounts first");
                //    return BadRequest("Cannot delete owner. It has related accounts. Delete those accounts first");
                //}

                _repository.Owner.DeleteOwner(owner);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteOwner action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }


    //[Route("api/[controller]")]
    //[ApiController]
    //public class PersonController : ControllerBase
    //{
    //    private GenericUnitOfWork uow = null;
    //    public PersonController()
    //    {
    //        uow = new GenericUnitOfWork();
    //    }
    //    public PersonController(GenericUnitOfWork _uow)
    //    {
    //        this.uow = _uow;
    //    }

    //    // GET: api/Person
    //    [HttpGet]
    //    public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
    //    {
    //        return await uow.Repository<Person>().GetOverview().ToListAsync();
    //    }

    //    // GET: api/Person/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<Person>> GetPerson(int id)
    //    {
    //        var person = await _context.Person.FindAsync(id);

    //        if (person == null)
    //        {
    //            return NotFound();
    //        }

    //        return person;
    //    }

    //    // PUT: api/Person/5
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutPerson(int id, Person person)
    //    {
    //        if (id != person.BusinessEntityId)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(person).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!PersonExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return NoContent();
    //    }

    //    // POST: api/Person
    //    [HttpPost]
    //    public async Task<ActionResult<Person>> PostPerson(Person person)
    //    {
    //        _context.Person.Add(person);
    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateException)
    //        {
    //            if (PersonExists(person.BusinessEntityId))
    //            {
    //                return Conflict();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return CreatedAtAction("GetPerson", new { id = person.BusinessEntityId }, person);
    //    }

    //    // DELETE: api/Person/5
    //    [HttpDelete("{id}")]
    //    public async Task<ActionResult<Person>> DeletePerson(int id)
    //    {
    //        var person = await _context.Person.FindAsync(id);
    //        if (person == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Person.Remove(person);
    //        await _context.SaveChangesAsync();

    //        return person;
    //    }

    //    private bool PersonExists(int id)
    //    {
    //        return _context.Person.Any(e => e.BusinessEntityId == id);
    //    }
}

