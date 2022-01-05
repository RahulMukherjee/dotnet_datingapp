using Microsoft.AspNetCore.Mvc;
using System.Linq;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    public class UsersController: BaseAPIController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context) { 
            _context = context;
        }

        //Endpoint : /api/users
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() { 
            return await _context.Users.ToListAsync();
        }

        //Endpoint : /api/users/{id}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUsers(int id)
        {
            return await _context.Users.FindAsync(id);
        }

    }
}
