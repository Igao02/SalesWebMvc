using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System.Linq;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMVCContext _context;
        //Criando uma dependencia para o DbContext

        public DepartmentService(SalesWebMVCContext context)
        {
            //Criando um construtor para que a operação de dependencia possa ocorrer
            _context = context;
        }

        public List<Department> FindAll()
        {
            //Método retorna os departamentos ordenados.
            return _context.Departments.OrderBy(x => x.Name).ToList();
        }
    }
}
