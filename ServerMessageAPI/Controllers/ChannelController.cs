using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerMessageAPI.ChannelAggregate;
using ServerMessageAPI.ChannelAggregate.Port;
using ServerMessageAPI.Models;

namespace ServerMessageAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : Controller
    {
        private readonly ServerMessageContext _context;
        private readonly IChannelRepository _repository;

        public ChannelController(ServerMessageContext context, IChannelRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: api/Channel
        [HttpGet]
        public async Task<List<ChannelDTO>> GetChannel()
        {
            return await _repository.GetChannelAsync();
        }

        // GET: api/Channel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChannelDTO>> GetChannel(int id)
        {
            var channel = await _repository.GetChannelAsync(id);

            if (channel == null)
            {
                return NotFound();
            }

            return channel;
        }

        // PUT: api/Channel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Channel>> PutChannel(int id, Channel channel)
        {
            try
            {
                if (id != channel.Id)
                {
                    return BadRequest("Channel ID mismatch");
                }

                var channelToUpdate = await _repository.GetChannelAsync(id);

                if (channelToUpdate == null)
                {
                    return NotFound($"Channel with Id = {id} not found");
                }

                return await _repository.UpdateChannel(channel);
            } 
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        // POST: api/Channel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Channel>> PostChannel(ChannelDTO channelDTO)
        {
            try
            {
                if (channelDTO == null)
                {
                    return BadRequest();
                }

                var chan = _repository.GetChannelByName(channelDTO.Name);
                if (chan != null)
                {
                    ModelState.AddModelError("name", "Channel name already in use");
                    return BadRequest(ModelState);
                }

                var createdChannel = await _repository.AddChannelAsync(channelDTO);
                return CreatedAtAction(nameof(GetChannel), new { id = createdChannel.Id },
                    createdChannel);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // DELETE: api/Channel/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Channel>> DeleteChannel(int id)
        {
            try
            {
                var channel = await _repository.GetChannelAsync(id);
                if (channel == null)
                {
                    return NotFound($"Employee with Id = {id} not found");
                }

                return await _repository.DeleteChannelAsync(id);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        private bool ChannelExists(int id)
        {
            return _context.Channel.Any(e => e.Id == id);
        }
    }
}
