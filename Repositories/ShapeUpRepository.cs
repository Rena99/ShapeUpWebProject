using AutoMapper;
using Comon;
using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ShapeUpRepository:IShapeUpRepository
    {
        private ShapeUp context;
        private IMapper mapper;

        public ShapeUpRepository(ShapeUp context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public async Task<MembersDTO> AddMember(Members members)
        {
            context.Members.Add(members);
            context.SaveChanges();
            return await GetMember(members.UserName, members.UserPassword);
        }
        public async Task<MembersDTO> GetMember(string name, string password)
        {
            Members mem = await context.Members.FirstOrDefaultAsync(m => m.UserName.Equals(name) && m.UserPassword.Equals(password));

            return mapper.Map<MembersDTO>(mem);
        }
        public MembersDTO EditMember(Members members)
        {
            Members m = null;
            foreach (var item in context.Members)
            {
                if (item.Email.Equals(members.Email) && item.UserName.Equals(members.UserName))
                {
                    item.UserPassword = members.UserPassword;
                    m = item;
                }
            }
            return mapper.Map<MembersDTO>(m); 
        }
        public async Task<ProjectsDTO> AddProject(Projects project)
        {
            foreach (var p in context.Projects)
            {
                if (p.ProjectName.Equals(project.ProjectName))
                {
                    return null;
                }
            }
            context.Projects.Add(project);
            context.SaveChanges();
            Projects pr= await context.Projects.OrderByDescending(q => q.Id).FirstOrDefaultAsync();
            return mapper.Map<ProjectsDTO>(pr);
        }
        public async Task<ProjectsDTO> GetProject(int id)
        {
            Projects pr= await context.Projects.FirstOrDefaultAsync(p => p.Id == id);
            return mapper.Map<ProjectsDTO>(pr);

        }
        public async Task<Projects> GetProjectR(int id)
        {
            return await context.Projects.Include(p=>p.ProjectShapeConn).Include(p=>p.Result).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<ProjectsDTO> EditProjectTitle(Projects project)
        {
            Projects p = await context.Projects.FirstOrDefaultAsync(pr => pr.Id == project.Id);
            p.ProjectName = project.ProjectName;
            p.OrderDate = project.OrderDate;
            p.DueDate = project.DueDate;
            p.ProjectStatus = project.ProjectStatus;
            context.SaveChanges();
            return await GetProject(project.Id);
        }
        public async void DeleteProject(int id)
        {
            Projects p = await context.Projects.FirstOrDefaultAsync(pr => pr.Id == id);
            foreach (var item in p.Result)
            {
                context.Result.Remove(item);
            }
            foreach (var shape in context.ProjectShapeConn)
            {
                if (shape.ProjectId == p.Id)
                {
                    context.ProjectShapeConn.Remove(shape);
                }
            }
            context.Projects.Remove(p);
            context.SaveChanges();
        }
        public List<ProjectsDTO> GetProjects(int id)
        {
            List<ProjectsDTO> projects = new List<ProjectsDTO>();
            foreach (var item in context.Projects)
            {
                if (item.MemberId == id) projects.Add(mapper.Map<ProjectsDTO>(item));
            }
            return projects;
        }
        public async Task<List<ShapesDTO>> GetShapes(int id)
        {
            List<ShapesDTO> shapes = new List<ShapesDTO>();
            foreach (var item in context.ProjectShapeConn)
            {
                if (item.ProjectId == id)
                {
                    Shapes shape = await context.Shapes.FirstOrDefaultAsync(s => s.Id == item.ShapeId);
                    shapes.Add(mapper.Map<ShapesDTO>(shape));
                }
            }
            return shapes;
        }

        public async Task<ShapesDTO> AddShape(Shapes s, int pid)
        {
            context.Shapes.Add(s);
            context.ProjectShapeConn.Add(new ProjectShapeConn { ProjectId = pid, ShapeId = context.Shapes.Last().Id });
            foreach(var point in s.Point)
            {
                context.Point.Add(new Point { X = point.X, Y = point.Y, ShapeId = context.Shapes.Last().Id });
            }
            Projects p = await context.Projects.FirstOrDefaultAsync(pr=>pr.Id==pid);
            foreach (var item in p.Result)
            {
                context.Result.Remove(item);
            }
            context.SaveChanges();
            return mapper.Map<ShapesDTO>(context.Shapes.LastAsync());
        }
        public async Task<ShapesDTO> GetShape(int i)
        {
            Shapes sh = await context.Shapes.FirstOrDefaultAsync(s => s.Id == i);
            return mapper.Map<ShapesDTO>(sh);
        }
        public async Task<Shapes> GetShapeR(int i)
        {
            return await context.Shapes.Include(s=>s.Point).FirstOrDefaultAsync(s => s.Id == i);
        }
        public async Task<ShapesDTO> EditShape(Shapes shape, int pid)
        {
            foreach(var s in context.Point)
            {
                if (s.ShapeId == shape.Id) context.Point.Remove(s);
            }
            foreach (var s in shape.Point)
            {
                context.Point.Add(new Point { ShapeId= shape.Id, X=s.X, Y=s.Y});
            }
            context.ProjectShapeConn.FirstOrDefault(p => p.ShapeId == shape.Id && p.ProjectId == pid).ProjectId=pid;
            Projects pr = await context.Projects.FirstOrDefaultAsync(p=>p.Id==pid);
            foreach (var item in pr.Result)
            {
                context.Result.Remove(item);
            }
            context.Shapes.FirstOrDefault(p => p.Id == shape.Id).Area = shape.Area;
            context.Shapes.FirstOrDefault(p => p.Id == shape.Id).Unit = shape.Unit;
            context.SaveChanges();
            return await GetShape(shape.Id);
        }
        public async void DeleteShape(int id, int cpid)
        {
            context.ProjectShapeConn.Remove(context.ProjectShapeConn.FirstOrDefault(p => p.ShapeId == id && p.ProjectId == cpid));
            Projects pr = await context.Projects.FirstOrDefaultAsync(p=>p.Id==id);
            foreach (var item in pr.Result)
            {
                context.Result.Remove(item);
            }
            context.SaveChanges();
        }
        public async Task<List<PointDTO>> GetPoints(int id)
        {
            List<PointDTO> points = new List<PointDTO>();
            Shapes s = await context.Shapes.FirstOrDefaultAsync(sh => sh.Id == id);
            foreach (var item in s.Point)
            {
                points.Add(mapper.Map<PointDTO>(item));
            }
            return points;
        }
        public async Task<List<ResultsDTO>> GetResult(int id)
        {
            List<ResultsDTO> results = new List<ResultsDTO>();
            Projects p = await context.Projects.FirstOrDefaultAsync(pr => pr.Id == id);
            foreach (var item in p.Result)
            {
                results.Add(mapper.Map<ResultsDTO>(item));
            }
            return results;
        }

        public void AddResult(double sx, double sy, double ax, double ay, int s, int p)
        {
            context.Result.Add(new Result { PointOfShapeX = (decimal)sx, PointOfShapeY = (decimal)sy, PointOnAreaX = (decimal)ax, PointOnAreaY = (decimal)ay, ShapeId = s, ProjectId = p });
            context.SaveChanges();
        }
    }
}
