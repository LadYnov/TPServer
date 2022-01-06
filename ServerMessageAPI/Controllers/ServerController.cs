using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerMessageAPI.ServerAggregate;
using ServerMessageAPI.ServerAggregate.Port;
using ServerMessageAPI.Models;

namespace ServerMessageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : Controller
    {
        private readonly ServerMessageContext _context;
        private readonly IServerRepository _repository;

        public ServerController(ServerMessageContext context, IServerRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: api/Server
        [HttpGet]
        public async Task<List<ServerDTO>> GetServer()
        {
            return await _repository.GetServerAsync();
        }

        // GET: api/Server/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ServerDTO>> GetServer(int id)
        {
            var server = await _repository.GetServerAsync(id);

            if (server == null)
            {
                return NotFound();
            }

            return server;
        }

        // PUT: api/Server/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Server>> PutServer(int id, Server Server)
        {
            try
            {
                if (id != Server.Id)
                {
                    return BadRequest("Server ID mismatch");
                }

                var serverToUpdate = await _repository.GetServerAsync(id);

                if (serverToUpdate == null)
                {
                    return NotFound($"Server with Id = {id} not found");
                }

                return await _repository.UpdateServer(Server);
            } 
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // POST: api/Server
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Server>> PostServer(ServerDTO ServerDTO)
        {
            try
            {
                if (ServerDTO == null)
                {
                    return BadRequest();
                }

                var chan = _repository.GetServerByName(ServerDTO.Name);
                if (chan != null)
                {
                    ModelState.AddModelError("name", "Server name already in use");
                    return BadRequest(ModelState);
                }

                var createdServer = await _repository.AddServerAsync(ServerDTO);
                return CreatedAtAction(nameof(GetServer), new { id = createdServer.Id },
                    createdServer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // DELETE: api/Server/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Server>> DeleteServer(int id)
        {
            try
            {
                var Server = await _repository.GetServerAsync(id);
                if (Server == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                return await _repository.DeleteServerAsync(id);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        private bool ServerExists(int id)
        {
            return _context.Server.Any(e => e.Id == id);
        }
    }
}
