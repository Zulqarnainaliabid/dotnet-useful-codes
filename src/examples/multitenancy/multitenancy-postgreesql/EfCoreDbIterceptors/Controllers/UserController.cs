using EfCoreDbIterceptors.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCoreDbIterceptors.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly EntitiesContext _context;

        public UserController(EntitiesContext contxt)
        {
            _context = contxt;
        }

        [HttpGet]
        public async Task<List<User>> GetAsync()
        {
            return await _context.Users.Where(x=>x.Username== "user1").ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<User> GetAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.ID == id);
        }

        [HttpPost]
        public async Task<Response> PostAsync(User request)
        {
            try
            {
                await _context.AddAsync(request);
                await _context.SaveChangesAsync();
                return new Response { Message = "Success", Status = "Ok" };
            }
            catch (System.Exception ex)
            {
                return new Response { Message = ex.Message.ToString(), Status = "Error" };
            }
        }

        [HttpPut("{id}")]
        public async Task<Response> PutAsync(int id, User request)
        {
            try
            {
                var model = await _context.Users.FirstOrDefaultAsync(user => user.ID == id);
                if (model != null)
                {
                    model.Email = request.Email;
                    model.Username = request.Username;
                }
                await _context.SaveChangesAsync();
                return new Response { Message = "Success", Status = "Ok" };
            }
            catch (System.Exception ex)
            {
                return new Response { Message = ex.Message, Status = "Error" };
            }

        }

        [HttpDelete("{id}")]
        public async Task<Response> Delete(int id)
        {
            try
            {
                var model = await _context.Users.FirstOrDefaultAsync(user => user.ID == id);
                if (model != null)
                {
                    _context.Remove(model);
                    await _context.SaveChangesAsync();
                }
                return new Response { Message = "Success", Status = "Ok" };
            }
            catch (System.Exception ex)
            {
                return new Response { Message = ex.Message, Status = "Error" };
            }

        }
    }
}
