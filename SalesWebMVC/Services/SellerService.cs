using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;

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
            //Realizando o Join das tabelas do Department e Seller, tem que adicionar o link entity framework core
            return _context.Sellers.Include(obj =>obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Sellers.Find(id);
            _context.Sellers.Remove(obj);
            _context.SaveChanges();
        }


        public void Update(Seller obj)
        {
            //este método verifica se um vendedor com o ID fornecido existe no contexto do banco de dados e, se existir, atualiza-o. Caso contrário, ele lança uma exceção.
            if (!_context.Sellers.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id Not Found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }   
            
            
        }


    }
}
