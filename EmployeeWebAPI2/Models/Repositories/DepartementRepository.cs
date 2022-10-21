namespace EmployeeWebAPI2.Models.Repositories
{
    public class DepartementRepository:IDepartmentRepository
    {
        private readonly AppDbContext appDbContext;

        public  DepartementRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public Departement GetDepartement(int DepartementId) 
        {
            return appDbContext.Departements.FirstOrDefault(d => d.DepartementId == DepartementId);
        }


        public IEnumerable<Departement> GetDepartements() 
        {
            return appDbContext.Departements;
        }

        public Departement GetDepartment(int departmentId)
        {
            return appDbContext.Departements.FirstOrDefault(d => d.DepartementId == departmentId);
        }

        public IEnumerable<Departement> GetDepartments()
        {
            return appDbContext.Departements;
        }
    }
}
