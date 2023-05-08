using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System.Linq;
using System.Threading.Tasks;


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

        public async Task<List<Department>> FindAllAsync()
        {
            //Método retorna os departamentos ordenados.
            return await _context.Departments.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
