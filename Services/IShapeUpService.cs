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
        Task<Members> AddMember(Members member);
        Task<Members> GetMember(string name, string password);
        Projects AddProject(Projects p);
        Projects GetProject(int id);
        Projects EditProjectTitle(Projects p);
        void DeleteProject(int id);
        Shapes AddShape(Shapes s, int pid);
        Shapes GetShape(int i);
        Shapes EditShape(Shapes s, int pid);
        void DeleteShape(int id, int cpid);
        bool Run(int id);
        List<Result> GetResult(int id);
        void AddResult(double sx, double sy, double ax, double ay, int s, int p);
        List<Shape> GetShapes(int id);
        List<Projects> GetProjects(int id);
        Task<Members> EditMember(Members members);
    }
}