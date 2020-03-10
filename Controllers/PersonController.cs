using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using evolutionPrueba.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using testEvolution.Models.Entities;
using testEvolution.Services;
using Microsoft.AspNetCore.Authorization;

namespace testEvolution.Controllers
{

    [Authorize]
    [Route("api/person")]
    [ApiController]
    public class PersonController : Controller
    {
        public readonly PersonService _personService;
        public PersonController(){
            _personService = new PersonService();
        }

        [HttpGet("all")]
        public IEnumerable<Person> GetAll(){
            return _personService.GetAll();
        }

        [HttpGet("{id}")]
        public Person Get(int id){
            return _personService.Find(id);
        }

        // [Route("user")]
        [HttpGet("user/{id}")]
        public Person GetUser(int id){
            return _personService.FindUser(id);
        }

        [HttpPost("register")]
        public ActionResult<Person> Register(Person person)
        {
            // if (person. < 0) return BadRequest("user is empty ó no cumple");
            Person model = _personService.Add(person);
            if (model == null) return BadRequest(model);
            Console.WriteLine("bueno bueno que paso aqui vale");
            return Ok(model);
        }
    }
}