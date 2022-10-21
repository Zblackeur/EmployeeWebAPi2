namespace EmployeeWebAPI2.Models.Repositories
{
    public interface IDepartmentRepository
    {
        IEnumerable<Departement> GetDepartments();
        Departement GetDepartment(int departmentId);
    }
}
