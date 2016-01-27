using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities;
using Repository.Pattern.Infrastructure;

namespace DAL
{
    public class RoleBasedSystemDBContextSeeder : DropCreateDatabaseAlways<RoleBasedSystemDBContext>//CreateDatabaseIfNotExists<FoodOrderingSystemDBContext>
    {
        //public virtual void Insert(TEntity entity)
        //{
        //    entity.ObjectState = ObjectState.Local.Added; ;
        //    _dbSet.Add(entity);
        //    _context.SyncObjectState(entity);
        //}
        //public override int SaveChanges()
        //{
        //    SyncObjectsStatePreCommit();
        //    var changes = base.SaveChanges();
        //    SyncObjectsStatePostCommit();
        //    return changes;
        //}
        protected override void Seed(RoleBasedSystemDBContext context)
        {
            foreach (var department in this.Departments())
            {
                department.ObjectState = ObjectState.Added;
                context.Departments.Add(department);
                context.SaveChanges();
            }

            foreach (var departmentRole in this.DepartmentRoles())
            {
                departmentRole.ObjectState = ObjectState.Added;
                context.DepartmentRoles.Add(departmentRole);
                context.SaveChanges();
            }
            foreach (var project in this.Projects())
            {
                project.ObjectState = ObjectState.Added;
                context.Projects.Add(project);
                context.SaveChanges();
            }
            foreach (var projectRole in this.ProjectRoles())
            {
                projectRole.ObjectState = ObjectState.Added;
                context.ProjectRoles.Add(projectRole);
                context.SaveChanges();
            }

            CreateAndSaveEmployee(context);
            foreach (var employeeAuthentication in this.EmployeeAuthentications())
            {
                employeeAuthentication.ObjectState = ObjectState.Added;
                context.EmployeeLogins.Add(employeeAuthentication);
                context.SaveChanges();
            }

            var projects = context.Projects.Local.ToList();
            foreach (var proj in projects)
            {
                var randomizer = new Random();
                int numberOfEmployees = 0, numberOfEmployeesNeeded = 0;
                var projectRoles = context.ProjectRoles.Local.ToList();
                var employees = context.Employees.Local.ToList();

                switch (proj.Name)
                {
                    case "Action Online":
                        numberOfEmployees = 0; numberOfEmployeesNeeded = 9;
                        break;
                    case "Blogging Chatter":
                        numberOfEmployees = 0; numberOfEmployeesNeeded = 6;
                        break;
                    case "Employee Performance Projector":
                        numberOfEmployees = 0; numberOfEmployeesNeeded = 6;
                        break;
                    case "Customer Management System":
                        numberOfEmployees = 0; numberOfEmployeesNeeded = 8;
                        break;
                    case "Machine Performance Analyser":
                        numberOfEmployees = 0; numberOfEmployeesNeeded = 9;
                        break;
                }
                while (numberOfEmployeesNeeded != numberOfEmployees)
                {
                    var employeeProject = new EmployeeProject();
                    employeeProject.Project = proj;
                    int randomNumber = employees.Count > 0 ? randomizer.Next(1, (employees.Count + 1)) : randomizer.Next(0, (employees.Count + 1));
                    var retrievedEmployee = employees.FirstOrDefault(x => x.EmployeeId == randomNumber);
                    var retrievedProjectRole = projectRoles.FirstOrDefault(pr => pr.Name.Equals(retrievedEmployee.DepartmentRole.Name));

                    if (retrievedProjectRole == null)
                    {
                        continue;
                    }
                    var numOfEmployeeProjects = context.EmployeeProjects.Local.FirstOrDefault() == null ? 0 : context.EmployeeProjects.Local.Where(x => x.EmployeeId == retrievedEmployee.EmployeeId && x.ProjectRoleId == retrievedProjectRole.ProjectRoleId && x.ProjectId == proj.ProjectId).ToList().Count;
                    if (numOfEmployeeProjects == 1)
                    {
                        continue;
                    }
                    else
                    {
                        employeeProject.Employee = retrievedEmployee;
                        employeeProject.ProjectRole = retrievedProjectRole;
                        employeeProject.ObjectState = ObjectState.Added;
                        context.EmployeeProjects.Add(employeeProject);
                        context.SaveChanges();
                        numberOfEmployees++;
                    }
                }
            }
            base.Seed(context);
        }
        private void CreateAndSaveEmployee(RoleBasedSystemDBContext context)
        {
            foreach (var employee in this.Employees())
            {
                var randomizer = new Random();
                var departmentRoles = context.DepartmentRoles.Local.ToList();
                var departments2 = context.Departments.Local.ToList();
                var employees = context.Employees.Local.ToList();
                var numOfDepartments = departmentRoles.Count() + 1;
                int randomNumberDepartmentId = numOfDepartments > 0 ? randomizer.Next(1, numOfDepartments) : randomizer.Next(0, numOfDepartments);
                var empToSave = employee;
                //empToSave.EmployeeLogin = this.EmployeeAuthentications().Find(e => e.EmployeeId == employee.EmployeeId);
                var shouldChange = true;
                do
                {
                    randomNumberDepartmentId = numOfDepartments > 0 ? randomizer.Next(1, numOfDepartments) : randomizer.Next(0, numOfDepartments);
                    var deptRole = departmentRoles.Where<DepartmentRole>(dr => dr.DepartmentRoleId == randomNumberDepartmentId).FirstOrDefault();
                    int? numberOfExistingUsersWithRole = employees.Count() <= 0 ? 0 : employees.FindAll(w => w.DepartmentRole.Name.Equals(deptRole.Name)).Count();
                    empToSave.DepartmentRole = deptRole;
                    switch (empToSave.DepartmentRole.Name)
                    {
                        case "SharePoint Developer":
                            empToSave.Department = context.Departments.Local.FirstOrDefault(d => d.Name.Equals("Information Technology"));
                            if (numberOfExistingUsersWithRole == 1)
                            {
                                shouldChange = true;
                            }
                            else
                            {
                                shouldChange = false;
                            }
                            break;
                        case "Database Administrator":
                            empToSave.Department = context.Departments.Local.FirstOrDefault(d => d.Name.Equals("Information Technology"));
                            if (numberOfExistingUsersWithRole == 1)
                            {
                                shouldChange = true;
                            }
                            else
                            {
                                shouldChange = false;
                            }
                            break;
                        case "BI Developer":
                            empToSave.Department = context.Departments.Local.FirstOrDefault(d => d.Name.Equals("Information Technology"));
                            if (numberOfExistingUsersWithRole == 1)
                            {
                                shouldChange = true;
                            }
                            else
                            {
                                shouldChange = false;
                            }
                            break;
                        case "Tester":
                            empToSave.Department = context.Departments.Local.FirstOrDefault(d => d.Name.Equals("Information Technology"));
                            if (numberOfExistingUsersWithRole == 2)
                            {
                                shouldChange = true;
                            }
                            else
                            {
                                shouldChange = false;
                            }
                            break;
                        case "Scrum Master":
                            empToSave.Department = context.Departments.Local.FirstOrDefault(d => d.Name.Equals("Information Technology"));
                            if (numberOfExistingUsersWithRole == 1)
                            {
                                shouldChange = true;
                            }
                            else
                            {
                                shouldChange = false;
                            }
                            break;
                        case ".Net Developer":
                            empToSave.Department = context.Departments.Local.FirstOrDefault(d => d.Name.Equals("Information Technology"));
                            if (numberOfExistingUsersWithRole == 3)
                            {
                                shouldChange = true;
                            }
                            else
                            {
                                shouldChange = false;
                            }
                            break;
                        case "Architect":
                            empToSave.Department = context.Departments.Local.FirstOrDefault(d => d.Name.Equals("Information Technology"));
                            if (numberOfExistingUsersWithRole == 1)
                            {
                                shouldChange = true;
                            }
                            else
                            {
                                shouldChange = false;
                            }
                            break;
                        case "Technical Team Lead":
                            empToSave.Department = context.Departments.Local.FirstOrDefault(d => d.Name.Equals("Information Technology"));
                            if (numberOfExistingUsersWithRole == 1)
                            {
                                shouldChange = true;
                            }
                            else
                            {
                                shouldChange = false;
                            }
                            break;
                        case "System Administrator":
                            empToSave.Department = context.Departments.Local.FirstOrDefault(d => d.Name.Equals("Information Technology"));
                            if (numberOfExistingUsersWithRole == 1)
                            {
                                shouldChange = true;
                            }
                            else
                            {
                                shouldChange = false;
                            }
                            break;
                        case "Sales Executive":
                            empToSave.Department = context.Departments.Local.FirstOrDefault(d => d.Name.Equals("Sales"));
                            if (numberOfExistingUsersWithRole == 2)
                            {
                                shouldChange = true;
                            }
                            else
                            {
                                shouldChange = false;
                            }
                            break;

                    }
                    if (!shouldChange)
                    {
                        empToSave.ObjectState = ObjectState.Added;
                        context.Employees.Add(empToSave);
                        context.SaveChanges();
                        //empToSave.EmployeeLogin.ObjectState = ObjectState.Added;
                        //context.EmployeeLogins.Add(empToSave.EmployeeLogin);
                        //context.SaveChanges();
                    }
                    else
                    {
                        continue;
                    }
                } while (shouldChange);
            }
        }

        private List<Employee> Employees()
        {
            var employeessEntityData = new List<Employee>()
            {
                new Employee{ 
                    EmployeeId = 1,
                    FirstName = "Mabongi",
                    LastName = "Mkhize", 
                    Phone = "0117822345", 
                    Address = "2434 Drago Street",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Employee{ 
                    EmployeeId = 2,
                    FirstName = "Basetsana",
                    LastName = "Lerumo", 
                    Phone = "014549383", 
                    Address = "345 Domino Street",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Employee{ 
                    EmployeeId = 3,
                    FirstName = "Charlie",
                    LastName = "Hop", 
                    Phone = "0193345567", 
                    Address = "5678 Green Street",
                   DateTimeCreated = DateTime.UtcNow
                },
                new Employee{ 
                    EmployeeId = 4,
                    FirstName = "Eric",
                    LastName = "Kim", 
                    Phone = "0173822345", 
                    Address = "234 From Street",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Employee{ 
                    EmployeeId = 5,
                    FirstName = "Jota",
                    LastName = "Yindi", 
                    Phone = "014549383", 
                    Address = "345 Lanes Street",
                      DateTimeCreated = DateTime.UtcNow
                },
                new Employee{ 
                    EmployeeId = 6,
                    FirstName = "Samuel",
                    LastName = "Green", 
                    Phone = "0193345567", 
                    Address = "5678 Green Street",
                   DateTimeCreated = DateTime.UtcNow
                },
                new Employee{ 
                    EmployeeId = 7,
                    FirstName = "Micheal",
                    LastName = "John", 
                    Phone = "0146822345", 
                    Address = "244 Origon Street",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Employee{ 
                    EmployeeId = 8,
                    FirstName = "Benedict",
                    LastName = "Kite", 
                    Phone = "0145434143", 
                    Address = "345 Kerry Street",
                      DateTimeCreated = DateTime.UtcNow
                },
                new Employee{ 
                    EmployeeId = 9,
                    FirstName = "Ziyanda",
                    LastName = "Nkomo", 
                    Phone = "0151345567", 
                    Address = "5678 Gorg Street",
                   DateTimeCreated = DateTime.UtcNow
                },
                new Employee{ 
                    EmployeeId = 10,
                    FirstName = "Puseletso",
                    LastName = "Motsei", 
                    Phone = "0117222345", 
                    Address = "34 Frim Street",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Employee{ 
                    EmployeeId = 11,
                    FirstName = "Nkem",
                    LastName = "Abdul", 
                    Phone = "014549383", 
                    Address = "345 Domo Street",
                      DateTimeCreated = DateTime.UtcNow
                },
                new Employee{ 
                    EmployeeId = 12,
                    FirstName = "Nishaan",
                    LastName = "Naidoo", 
                    Phone = "0213345567", 
                    Address = "78 Zen Street",
                   DateTimeCreated = DateTime.UtcNow
                },
                new Employee{ 
                    EmployeeId = 13,
                    FirstName = "Kerryn",
                    LastName = "Smith", 
                    Phone = "0132236383", 
                    Address = "32145 Dono Street",
                      DateTimeCreated = DateTime.UtcNow
                },
                new Employee{ 
                    EmployeeId = 14,
                    FirstName = "Nabeela",
                    LastName = "Moodley", 
                    Phone = "0213345567", 
                    Address = "78 Wild Street",
                   DateTimeCreated = DateTime.UtcNow
                }
            };
            return employeessEntityData;
        }
        private List<EmployeeLogin> EmployeeAuthentications()
        {
            var employeessEntityData = new List<EmployeeLogin>()
            {
                new EmployeeLogin{ 
                    EmployeeId = 1,
                    UserName = "MMkhize",
                    Password = "MabongiM"
                },
                new EmployeeLogin{ 
                    EmployeeId = 2,
                    UserName = "BLerumo",
                    Password = "BasetsanaL"
                },
                new EmployeeLogin{ 
                    EmployeeId = 3,
                    UserName = "CHop",
                    Password = "CharlieH"
                },
                new EmployeeLogin{ 
                    EmployeeId = 4,
                    UserName = "EKim",
                    Password = "EricK"
                },
                new EmployeeLogin{ 
                    EmployeeId = 5,
                    UserName = "JYindi",
                    Password = "JotaY"
                },
                new EmployeeLogin{ 
                    EmployeeId = 6,
                    UserName = "SGreen",
                    Password = "SamuelG"
                },
                new EmployeeLogin{ 
                    EmployeeId = 7,
                    UserName = "MJohn",
                    Password = "MichealJ"
                },
                new EmployeeLogin{ 
                    EmployeeId = 8,
                    UserName = "BKite",
                    Password = "BenedictK"
                },
                new EmployeeLogin{ 
                    EmployeeId = 9,
                    UserName = "ZNkomo",
                    Password = "ZiyandaN"
                },
                new EmployeeLogin{ 
                    EmployeeId = 10,
                    UserName = "PMotsei",
                    Password = "PuseletsoM"
                },
                new EmployeeLogin{ 
                    EmployeeId = 11,
                    UserName = "NAbdul",
                    Password = "NkemA"
                },
                new EmployeeLogin{ 
                    EmployeeId = 12,
                    UserName = "NNaidoo",
                    Password = "NishaanN"
                },
                new EmployeeLogin{ 
                    EmployeeId = 13,
                    UserName = "KSmith",
                    Password = "KerrynS"
                },
                new EmployeeLogin{ 
                    EmployeeId = 14,
                    UserName = "NMoodley",
                    Password = "NabeelaM"
                }
            };
            return employeessEntityData;
        }
        private List<ProjectRole> ProjectRoles()
        {
            var projectRoleEntityData = new List<ProjectRole>()
            {
                new ProjectRole{ 
                    ProjectRoleId = 1,
                    Name = "Architect",
                    Description = "Incharge of system architecture.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 650
                },
                new ProjectRole{ 
                    ProjectRoleId = 2,
                    Name = ".Net Developer",
                    Description = "In charge of coding projects using the C# programming language.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 400
                },
                new ProjectRole{ 
                    ProjectRoleId = 3,
                    Name = "Technical Team Lead",
                    Description = "A Technical Team Lead to lead the team through its up and downs.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 600
                },
                new ProjectRole{ 
                    ProjectRoleId = 4,
                    Name = "Scrum Master",
                    Description = "A scrum master to handle all scrum meetings.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 350
                },
                new ProjectRole{ 
                    ProjectRoleId = 5,
                    Name = "Tester",
                    Description = "A software tester who test evry part of a system throughly.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 250
                },
                new ProjectRole{ 
                    ProjectRoleId = 6,
                    Name = "BI Developer",
                    Description = "Business Intelligence Developer assists business management with money saving, investing and making ideas.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 550
                },
                new ProjectRole{ 
                    ProjectRoleId = 7,
                    Name = "Database Administrator",
                    Description = "A Database Administrator that will have all access to data administered and data managed.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 400
                },
                new ProjectRole{ 
                    ProjectRoleId = 8,
                    Name = "SharePoint Developer",
                    Description = "A SharePoint Developer that can create efficient in house web applications.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 500
                },
                new ProjectRole{ 
                    ProjectRoleId = 9,
                    Name = "System Administrator",
                    Description = "A software tester who test evry part of a system throughly.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 250
                }
            };
            return projectRoleEntityData;
        }
        private List<DepartmentRole> DepartmentRoles()
        {
            var DepartmentRoleEntityData = new List<DepartmentRole>()
            {
                new DepartmentRole{ 
                    DepartmentRoleId = 1,
                    Name = "Architect",
                    Description = "Incharge of system architecture.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 650
                },
                new DepartmentRole{ 
                    DepartmentRoleId = 2,
                    Name = ".Net Developer",
                    Description = "In charge of coding projects using the C# programming language.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 400
                },
                new DepartmentRole{ 
                    DepartmentRoleId = 3,
                    Name = "Technical Team Lead",
                    Description = "A Technical Team Lead to lead the team through its up and downs.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 600
                },
                new DepartmentRole{ 
                    DepartmentRoleId = 4,
                    Name = "Scrum Master",
                    Description = "A scrum master to handle all scrum meetings.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 350
                },
                new DepartmentRole{ 
                    DepartmentRoleId = 5,
                    Name = "Tester",
                    Description = "A software tester who test evry part of a system throughly.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 250
                },
                new DepartmentRole{ 
                    DepartmentRoleId = 6,
                    Name = "BI Developer",
                    Description = "Business Intelligence Developer pleases business management with money saving, investing and making ideas.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 550
                },
                new DepartmentRole{ 
                    DepartmentRoleId = 7,
                    Name = "Database Administrator",
                    Description = "A Database Administrator that will have all access to data administered and data managed.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 400
                },
                new DepartmentRole{ 
                    DepartmentRoleId = 8,
                    Name = "SharePoint Developer",
                    Description = "A SharePoint Developer that can create efficient in house web applications.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 500
                },
                new DepartmentRole{ 
                    DepartmentRoleId = 9,
                    Name = "System Administrator",
                    Description = "A software tester who test evry part of a system throughly.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 250
                },
                new DepartmentRole{ 
                    DepartmentRoleId = 10,
                    Name = "Sales Executive",
                    Description = "Business Intelligence Developer pleases business management with money saving, investing and making ideas.",
                    DateTimeCreated = DateTime.UtcNow,
                    RatePerHour = 550
                }
            };
            return DepartmentRoleEntityData;
        }
        private List<Project> Projects()
        {
            var ProjectEntityData = new List<Project>()
            {
                new Project{ 
                    ProjectId = 1,
                    Name = "Action Online",
                    Description = "A online game that will be created using MVC.",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Project{ 
                    ProjectId = 2,
                    Name = "Blogging Chatter",
                    Description = "A in house application that can be used in house by all employees.",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Project{ 
                    ProjectId = 3,
                    Name = "Employee Performance Projector",
                    Description = "A reporting site displaying how the performance of a employe is at current time and will be after five years.",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Project{ 
                    ProjectId = 4,
                    Name = "Customer Management System",
                    Description = "A system that can help manage how users interact with in house products.",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Project{ 
                    ProjectId = 5,
                    Name = "Machine Performance Analyser",
                    Description = "A system that keeps analysis and process how company software is acting with any kind of hardware.",
                    DateTimeCreated = DateTime.UtcNow
                }
            };
            return ProjectEntityData;
        }
        private List<Department> Departments()
        {
            var departmentsEntityData = new List<Department>()
            {
                new Department{
                    DepartmentId = 1,
                    Name = "Human Resource",
                    MailStop = "A Mail stop ",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Department{
                    DepartmentId = 2,
                    Name = "Information Technology",
                    MailStop = "A Mail stop ",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Department{
                    DepartmentId = 3,
                    Name = "Finance",
                    MailStop = "A Mail stop ",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Department{
                    DepartmentId = 4,
                    Name = "Marketing",
                    MailStop = "A Mail stop ",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Department{
                    DepartmentId = 5,
                    Name = "Law",
                    MailStop = "A Mail stop ",
                    DateTimeCreated = DateTime.UtcNow
                },
                new Department{
                    DepartmentId = 6,
                    Name = "Sales",
                    MailStop = "A Mail stop ",
                    DateTimeCreated = DateTime.UtcNow
                }
            };
            return departmentsEntityData;
        }
    }
}
