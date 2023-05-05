using SalesWebMVC.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesWebMVC.Models
{
    public class SalesRecord
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public SalesStatus Status { get; set; }
        [ForeignKey("Seller_Id")]
        public Seller Seller { get; set; }
        public int Seller_Id { get; set; }
        //Associação de SalesRecord com Seller (*...1)

        //Criando o construtor sem argumentos
        public SalesRecord() 
        {

        }

        //Criando o construtor com arugmentos
        public SalesRecord( DateTime date, double amount, SalesStatus status, Seller seller)
        {
            
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}
