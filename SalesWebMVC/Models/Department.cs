using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models

{
    public class Department
    {
        //Criação da Nova Classe "Departamentos"


        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection <Seller> Sellers { get; set; } = new List<Seller>();
        //Essa ultima linha, está realizando a associação do Department com o Seller, onde no diagrama está Department 1 para Sellers * (1...*)


        //Criando o construtor vázio
        public Department()
        {

        }

        //Criando o construtor com argumentos
        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void Add(Seller seller) 
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
            //Estou pegando cada vendedor da minha lista, de acordo com uma data inicial e final, e realizo uma soma para
            //Todos os vendedores do meu departamento.
        }



    }
}
