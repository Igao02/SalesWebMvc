using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name Required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name size should between 3 and 60")]
        public String Name { get; set; }


        [Required(ErrorMessage = "E-mail Required")]
        [EmailAddress(ErrorMessage = "Enter a Valid Email")]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }


        [Required(ErrorMessage = "BirthDate Required")]
        [Display (Name = "Birth Date")]
        [DataType (DataType.Date)]
        public DateTime BirthDate{ get; set; }


        [Required(ErrorMessage = "Base Salary Required")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }

        [ForeignKey("Department_Id")] 
        public Department Department { get; set; }
        //Realizado a implementação de Seller para Department (*...1)
        public int Department_Id { get; set; }


        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
        //Realizando a associação da classe Seler com Sales Record (1...*)


        //Criando o construtor vázio
        public Seller()
        {

        }

        //Criando o construtor com argumentos
        public Seller(string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
           
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        
        public void AddSales(SalesRecord sr)
        {
            //Adicionando um novo SalesRecord
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            //Removendo um novo SalesRecord
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }


      
    }
}
