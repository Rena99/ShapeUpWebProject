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
        public ShapeUpRepository(ShapeUp context)
        {
            this.context = context;
        }
        public async Task<Members> AddMember(Members members)
        {
            //Members members = new Members { UserName = name, UserPassword = password, Email = email };
            context.Members.Add(members);
            context.SaveChanges();
            return await GetMember(members.UserName, members.UserPassword);
        }
        public async Task<Members> GetMember(string name, string password)
        {
            return await context.Members.FirstOrDefaultAsync(m => m.UserName.Equals(name) && m.UserPassword.Equals(password));
        }
        public Projects AddProject(Projects project)
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
            return context.Projects.OrderByDescending(q => q.Id).FirstOrDefault();
        }
        public Projects GetProject(int id)
        {
            return context.Projects.Include(p=>p.ProjectShapeConn).FirstOrDefault(p => p.Id == id);

        }
        public Projects EditProjectTitle(Projects project)
        {
            context.Projects.FirstOrDefault(p => p.Id == project.Id).ProjectName = project.ProjectName;
            context.Projects.FirstOrDefault(p => p.Id == project.Id).OrderDate = project.OrderDate;
            context.Projects.FirstOrDefault(p => p.Id == project.Id).DueDate = project.DueDate;
            context.Projects.FirstOrDefault(p => p.Id == project.Id).ProjectStatus = project.ProjectStatus;
            context.SaveChanges();
            return GetProject(project.Id);
        }
        public void DeleteProject(int id)
        {
            Projects p = GetProject(id);
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
        public Shapes AddShape(Shapes s, int pid)
        {
            context.Shapes.Add(s);
            context.ProjectShapeConn.Add(new ProjectShapeConn { ProjectId = pid, ShapeId = context.Shapes.Last().Id });
            foreach(var point in s.Point)
            {
                context.Point.Add(new Point { X = point.X, Y = point.Y, ShapeId = context.Shapes.Last().Id });
            }
            Projects p = GetProject(pid);
            foreach (var item in p.Result)
            {
                context.Result.Remove(item);
            }
            context.SaveChanges();
            return context.Shapes.Last();
        }
        public Shapes GetShape(int i)
        {
            return context.Shapes.Include(s=>s.Point).FirstOrDefault(s => s.Id == i);
        }
        public Shapes EditShape(Shapes shape, int pid)
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
            Projects pr = GetProject(pid);
            foreach (var item in pr.Result)
            {
                context.Result.Remove(item);
            }
            context.Shapes.FirstOrDefault(p => p.Id == shape.Id).Area = shape.Area;
            context.Shapes.FirstOrDefault(p => p.Id == shape.Id).Unit = shape.Unit;
            context.SaveChanges();
            return GetShape(shape.Id);
        }
        public void DeleteShape(int id, int cpid)
        {
            context.ProjectShapeConn.Remove(context.ProjectShapeConn.FirstOrDefault(p => p.ShapeId == id && p.ProjectId == cpid));
            Projects pr = GetProject(cpid);
            foreach (var item in pr.Result)
            {
                context.Result.Remove(item);
            }
            context.SaveChanges();
        }

        public List<Result> GetResult(int id)
        {
            List<Result> results = new List<Result>();
            foreach (var item in GetProject(id).Result)
            {
                results.Add(item);
            }
            return results;
        }

        public void AddResult(double sx, double sy, double ax, double ay, int s, int p)
        {
            context.Result.Add(new Result { PointOfShapeX = (decimal)sx, PointOfShapeY = (decimal)sy, PointOnAreaX = (decimal)ax, PointOnAreaY = (decimal)ay, ShapeId = s, ProjectId = p });
            context.SaveChanges();
        }

        public List<Shape> GetShapes(int id)
        {
            List<Shape> shapes = new List<Shape>();
            foreach (var item in context.ProjectShapeConn)
            {
                if (item.ProjectId == id)
                {
                    Shapes shape=context.Shapes.Include(s=>s.Point).Include(s=>s.Result).FirstOrDefault(s => s.Id == item.ShapeId);
                    List<PointMap> points = new List<PointMap>();
                    foreach (var p in shape.Point)
                    {
                        points.Add(new PointMap(p.Id, p.X, p.Y));
                    }
                    Results result=new Results();
                    foreach (var r in shape.Result)
                    {
                        if (r.ProjectId == id)
                        {
                            result =new Results(r.Id, r.PointOfShapeX, r.PointOfShapeY, r.PointOnAreaX, r.PointOnAreaY);
                            break;
                        }
                    }
                    shapes.Add(new Shape(shape.Id, shape.Area, shape.Unit, points, result));
                }
            }
            return shapes;
        }

        public List<Projects> GetProjects(int id)
        {
            List<Projects> projects = new List<Projects>();
            foreach (var item in context.Projects)
            {
                if (item.MemberId == id) projects.Add(item);
            }
            return projects;
        }

        public async Task<Members> EditMember(Members members)
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
            return m;
        }
    }
}
