using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Order.PL.API.DTOs;
using Orders.Core.Models;

namespace Order.PL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

    
        [HttpPost("AddRole")]
        public async Task<IActionResult> Create(RoleDTO roledto)
        {
            var role = new IdentityRole()
            {
                Id = roledto.Id,
                Name = roledto.Name,
            };
                await _roleManager.CreateAsync(role);
    
            return Ok(role);
        }
        [HttpPut("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromRoute] string id, RoleDTO roledto)
        {
            if (id != roledto.Id) return BadRequest();
                    var role = await _roleManager.FindByIdAsync(id);
                    role.Name = roledto.Name;
                    await _roleManager.UpdateAsync(role);
             
            return Ok(roledto);
        }
       
        [HttpDelete("DeleteRole")]
        public async Task<IActionResult> Delete(string id, RoleDTO roledto)
        {
                var role = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(role);

                return Ok(roledto);
        }
    
        [HttpPut("{id}")]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleDTO> models, string RoleId)
        {
            var role = await _roleManager.FindByIdAsync(RoleId);
            if (role == null) return NotFound();
                foreach (var i in models)
                {
                    var user = await _userManager.FindByIdAsync(i.UserId);
                    if (user != null)
                    {
                        if (i.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                            await _userManager.AddToRoleAsync(user, role.Name);
                        else if (!i.IsSelected && (await _userManager.IsInRoleAsync(user, role.Name)))
                            await _userManager.RemoveFromRoleAsync(user, role.Name);
                    }

                }
            return Ok();
        }
    }
}

