using AutoMapper;
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
        private IMapper mapper;

        public ShapeUpService(IShapeUpRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        public async Task<MembersDTO> AddMember(Members members)
        {
            return await repository.AddMember(members);
        }
        public async Task<MembersDTO> GetMember(string name, string password)
        {
            return await repository.GetMember(name, password);
        }
        public MembersDTO EditMember(Members members)
        {
            return repository.EditMember(members);
        }
        public async Task<ProjectsDTO> AddProject(Projects project)
        {
            return await repository.AddProject(project);
        }
        public async Task<ProjectsDTO> GetProject(int id)
        {
            return await repository.GetProject(id);
        }
        public async Task<ProjectsDTO> EditProjectTitle(Projects project)
        {
            return await repository.EditProjectTitle(project);
        }
        public void DeleteProject(int id)
        {
              repository.DeleteProject(id);
        }
        public List<ProjectsDTO> GetProjects(int id)
        {
            return repository.GetProjects(id);
        }
        public async Task<List<ShapesDTO>> GetShapes(int id)
        {
            return await repository.GetShapes(id);
        }

        public async Task<CompleteShape> AddShape(Shapes s, int pid)
        {
            return await repository.AddShape(s, pid);
        }
        public async Task<ShapesDTO> GetShape(int i)
        {
            return await repository.GetShape(i);
        }
        public async Task<CompleteShape> EditShape(Shapes shape, int pid)
        {
            return await repository.EditShape(shape, pid);
        }
        public void DeleteShape(int id, int cpid)
        {
            repository.DeleteShape(id, cpid);
        }
        public async Task<List<PointDTO>> GetPoints(int id)
        {
            return await repository.GetPoints(id);
        }
        public async Task<ResultsDTO> GetResult(int pid, int id)
        {
            return await repository.GetResult(pid, id);
        }

        public void AddResult(double sx, double sy, double ax, double ay, int s, int p)
        {
            repository.AddResult(sx, sy , ax, ay, s, p);
        }
        public async Task<bool> Run(int id)
        {
            Projects p = await repository.GetProjectR(id);
            if (p.Result.Count > 0) return true;
            List<MyShapes> myShapes = new List<MyShapes>();
            MyShapes area = new MyShapes();
            foreach (var item in p.ProjectShapeConn)
            {
                Shapes s = await repository.GetShapeR(item.ShapeId);
                List<PointDTO> pointDTOs = new List<PointDTO>();
                foreach (var pnt in s.Point)
                {
                    pointDTOs.Add(mapper.Map<PointDTO>(pnt));
                }
                if (item.Shape.Area==true) area = new MyShapes(s.Id, s.Unit, pointDTOs);
                else myShapes.Add(new MyShapes(s.Id, s.Unit, pointDTOs));
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

        public async Task<List<CompleteShape>> GetCompleteShapes(int pid)
        {
            return await repository.GetCompleteShapes(pid);
        }

        public async Task<CompleteShape> GetCompleteShape(int pid, int id)
        {
            return await repository.GetCompleteShape(pid, id);
        }
    }
}
