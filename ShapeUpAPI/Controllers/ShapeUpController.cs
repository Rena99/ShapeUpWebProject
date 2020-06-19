using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Comon;
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
        public async Task<MembersDTO> GetMembers(string name, string password) => await service.GetMember(name, password);

        [HttpPost("Members")]
        public async Task<MembersDTO> AddMember([FromBody] Members members) => await service.AddMember(members);

        [HttpPost("Member")]
        public MembersDTO EditMember([FromBody] Members members) => service.EditMember(members);


        [HttpPost("Projects")]
        public async Task<ProjectsDTO> AddProject([FromBody] Projects projects) => await service.AddProject(projects);

        [HttpGet("Projects/{id}")]
        public List<ProjectsDTO> GetProjects(int id) => service.GetProjects(id);

        [HttpGet("Project/{id}")]
        public async Task<ProjectsDTO> GetProject(int id) => await service.GetProject(id);

        [HttpPost("Projects")]
        public async Task<ProjectsDTO> EditProjectTitle([FromBody] Projects p) => await service.EditProjectTitle(p);

        [HttpDelete("Projects/{id}")]
        public void DeleteProject(int id) => service.DeleteProject(id);


        [HttpPost("Shape/{pid}")]
        public async Task<ShapesDTO> AddShape([FromBody] Shapes s, int pid)=> await service.AddShape(s, pid);

        [HttpGet("Shape/{id}")]
        public async Task<ShapesDTO> GetShape(int id) => await service.GetShape(id);

        [HttpGet("Shapes/{pid}")]
        public async Task<List<ShapesDTO>> GetShapes(int pid) => await service.GetShapes(pid);

        [HttpPost("Shape/{pid}")]
        public async Task<ShapesDTO> EditShape([FromBody] Shapes s, int pid) => await service.EditShape(s, pid);

        [HttpDelete("Shape/{id}/{cpid}")]
        public void DeleteShape(int id, int cpid) => service.DeleteShape(id, cpid);


        [HttpGet("{id}")]
        public async Task<bool> Run(int id) => await service.Run(id);

        [HttpGet("Result/{pid}/{id}")]
        public async Task<ResultsDTO> GetResults(int pid, int id) => await service.GetResult(pid, id);

        [HttpGet("points/{id}")]
        public async Task<List<PointDTO>> GetPoints(int id) => await service.GetPoints(id);
    }
}
