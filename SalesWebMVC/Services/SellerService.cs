using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;
        //Criando uma dependencia para o DbContext

        public SellerService(SalesWebMVCContext context)
        {
            //Criando um construtor para que a operação de dependencia possa ocorrer
            _context = context;
        }

        
        public List<Seller> FindAll()
        {
            //Aqui pegamos todos os vendedores do Banco de Dados
            return _context.Seller.ToList();
        }
    }
}
