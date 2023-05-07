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
            return _context.Sellers.ToList();
        }

        public void Insert(Seller obj) 
        {
            //Adicionando um novo vendedor no nosso Banco de Dados
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id)
        {
            return _context.Sellers.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Sellers.Find(id);
            _context.Sellers.Remove(obj);
            _context.SaveChanges();
        }


    }
}
