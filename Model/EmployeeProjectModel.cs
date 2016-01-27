namespace Models
{
    public class EmployeeProjectModel
    {
            public EmployeeModel Employee { get; set; }
            public int EmployeeId { get; set; }
            public ProjectModel Project { get; set; }
            public int ProjectId { get; set; }
            public ProjectRoleModel ProjectRole { get; set; }
            public int ProjectRoleId { get; set; }
        }
}
