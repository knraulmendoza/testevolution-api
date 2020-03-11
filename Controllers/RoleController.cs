using System.Data;
using System.Collections.Generic;
using System;
using evolutionPrueba.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using testEvolution.Models.Entities;

namespace evolutionPrueba.Controllers
{
    
    [Authorize]
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;
        public RoleController()
        {
            _roleService = new RoleService();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("{id}")]
        public ActionResult<Role> Get(int id)
        {
            return _roleService.Find(id);
        }

        [Authorize(Roles = "admin")]
        [HttpPost("register")]
        public ActionResult<Role> Register([FromBody]Role role)
        {
            Console.WriteLine($"name {role.Name} description: {role.Description}");
            if (role.Name == null || role.Name.Length < 4) return BadRequest("role is empty ó no cumple");
            Role model = _roleService.Add(role);
            if (model == null) return BadRequest(model);
            return Ok(model);
        }
        [Authorize(Roles = "admin")]
        [Route("update")]
        [HttpPut("{id}")]
        public ActionResult<Role> Update(int id, [FromBody]Role role)
        {
            Console.WriteLine("aja que pasa vale");
            if (_roleService.Find(id) == null) return BadRequest("role is empty ó no cumple");
            Role model = _roleService.Edit(id, role);
            if (model == null) return BadRequest(model);
            return Ok(model);
        }
        [Authorize(Roles = "admin")]
        [Route("getAll")]
        [HttpGet]
        public IEnumerable<Role> GetAll()
        {
            return _roleService.GetAll();
        }
    }
}