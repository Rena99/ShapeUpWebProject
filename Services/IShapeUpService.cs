using Comon;
using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IShapeUpService
    {
        Task<MembersDTO> AddMember(Members members);
        Task<MembersDTO> GetMember(string name, string password);
        MembersDTO EditMember(Members members);
        Task<ProjectsDTO> AddProject(Projects project);
        Task<ProjectsDTO> GetProject(int id);
        Task<ProjectsDTO> EditProjectTitle(Projects project);
        void DeleteProject(int id);
        List<ProjectsDTO> GetProjects(int id);
        Task<List<ShapesDTO>> GetShapes(int id);
        Task<ShapesDTO> AddShape(Shapes s, int pid);
        Task<ShapesDTO> GetShape(int i);
        Task<ShapesDTO> EditShape(Shapes shape, int pid);
        void DeleteShape(int id, int cpid);
        Task<List<PointDTO>> GetPoints(int id);
        Task<List<ResultsDTO>> GetResult(int id);
        void AddResult(double sx, double sy, double ax, double ay, int s, int p);
        Task<bool> Run(int id);
    }
}