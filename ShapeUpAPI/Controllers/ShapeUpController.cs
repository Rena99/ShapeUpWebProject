using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Models;
using Services;

////flip points-0,0 top left corner
//post functions
////set width and height
//how to set number of coordinates for canvas
////how to move 0,0 point to bottom-left
//errors
//attach js file to app
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShapeUpAPI.Controllers
{
    [EnableCors("shapeup")]
    [Route("api/[controller]")]
    public class ShapeUpController : ControllerBase
    {
        IShapeUpService service;
        public ShapeUpController(IShapeUpService service) => this.service = service;

        [HttpGet("Members/{name}/{password}")]
        public async Task<Members> GetMembers(string name, string password) => await service.GetMember(name, password);
        [HttpPost("Members")]
        public async Task<Members> AddMember([FromBody]Members members) => await service.AddMember(members);
        [HttpPost("Member")]
        public async Task<Members> EditMember([FromBody] Members members) => await service.EditMember(members);
        [HttpPost("Projects")]
        public Projects AddProject([FromBody] Projects projects) => service.AddProject(projects);
        [HttpGet("Projects/{id}")]
        public List<Projects> GetProjects(int id) => service.GetProjects(id);
        [HttpGet("Project/{id}")]
        public Projects GetProject(int id) => service.GetProject(id);
        [HttpPost("Projects/{id}/{name}/{o}/{d}/{s}")]
        public Projects EditProjectTitle([FromBody] Projects p) => service.EditProjectTitle(p);
        [HttpDelete("Projects/{id}")]
        public void DeleteProject(int id) => service.DeleteProject(id);
        [HttpPost("Shape/{pid}")]
        public Shapes AddShape([FromBody] Shapes s, int pid)=>service.AddShape(s, pid);
        [HttpGet("Shape/{id}")]
        public Shapes GetShape(int id) => service.GetShape(id);
        [HttpGet("Shapes/{id}")]
        public List<Shape> GetShapes(int id) => service.GetShapes(id);
        [HttpPost("Shape/{pid}")]
        public Shapes EditShape([FromBody] Shapes s, int pid) => service.EditShape(s, pid);
        [HttpDelete("Shape/{id}/{cpid}")]
        public void DeleteShape(int id, int cpid) => service.DeleteShape(id, cpid);
        [HttpGet("{id}")]
        public bool Run(int id) => service.Run(id);
        [HttpGet("Result/{id}")]
        public List<Result> GetResults(int id) => service.GetResult(id);
    }
}
