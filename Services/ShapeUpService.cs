using Comon;
using Repositories;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ShapeUpService : IShapeUpService
    {
        private IShapeUpRepository repository;

        public ShapeUpService(IShapeUpRepository repository) => this.repository = repository;

        public async Task<Members> AddMember(Members member) => await repository.AddMember(member);

        public async Task<Members> GetMember(string name, string password) => await repository.GetMember(name, password);

        public Projects AddProject(Projects p) => repository.AddProject(p);

        public Projects GetProject(int id) => repository.GetProject(id);

        public Projects EditProjectTitle(Projects p) => repository.EditProjectTitle(p);

        public void DeleteProject(int id) => repository.DeleteProject(id);

        public Shapes AddShape(Shapes s, int pid) => repository.AddShape(s, pid);

        public Shapes GetShape(int i) => repository.GetShape(i);

        public Shapes EditShape(Shapes s, int pid) => repository.EditShape(s, pid);

        public void DeleteShape(int id, int cpid)=>repository.DeleteShape(id, cpid);

        public bool Run(int id)
        {
            Projects p = GetProject(id);
            if (p.Result.Count > 0) return true;
            List<MyShapes> myShapes = new List<MyShapes>();
            MyShapes area = new MyShapes();
            foreach (var item in p.ProjectShapeConn)
            {
                Shapes s = GetShape(item.ShapeId);
                if (item.Shape.Area==true) area = new MyShapes(s.Id, s.Unit, s.Point);
                else myShapes.Add(new MyShapes(s.Id, s.Unit, s.Point));
            }
            Algorithm algorithm = new Algorithm(myShapes, area);
            if (algorithm.Succeeded == true)
            {
                foreach (var item in myShapes)
                {
                    AddResult(item.IndexOfPoint.X + item.MinX, item.IndexOfPoint.Y + item.MinY, item.PointOnArea.X, item.PointOnArea.Y, item.Id, p.Id);
                }
            }
            return algorithm.Succeeded;
        }

        public List<Result> GetResult(int id) => repository.GetResult(id);

        public void AddResult(double sx, double sy, double ax, double ay, int s, int p)=>repository.AddResult(sx, sy, ax, ay, s, p);

        public List<Shape> GetShapes(int id)
        {
            return repository.GetShapes(id);
        }

        public List<Projects> GetProjects(int id)
        {
            return repository.GetProjects(id);
        }

        public async Task<Members> EditMember(Members members)
        {
            return await repository.EditMember(members);
        }
    }
}
