using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Services.Exceptions;
using System.Threading.Tasks;

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

        
        public async Task<List<Seller>> FindAllAsync()
        {
            //Aqui pegamos todos os vendedores do Banco de Dados
            return await _context.Sellers.ToListAsync();
        }

        public async Task InsertAsync(Seller obj) 
        {
            //Adicionando um novo vendedor no nosso Banco de Dados
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            //Realizando o Join das tabelas do Department e Seller, tem que adicionar o link entity framework core
            return await _context.Sellers.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id)
        {
            var obj = await _context.Sellers.FindAsync(id);
            _context.Sellers.Remove(obj);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Seller obj)
        {
            //este método verifica se um vendedor com o ID fornecido existe no contexto do banco de dados e, se existir, atualiza-o. Caso contrário, ele lança uma exceção.
            bool hasAny = await _context.Sellers.AnyAsync(x => x.Id == obj.Id);
            if (!hasAny)
            {
                throw new NotFoundException("Id Not Found");
            }
            try
            {
                _context.Update(obj);
               await _context.SaveChangesAsync();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }   
            
            
        }


    }
}
