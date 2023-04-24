﻿using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public DateTime BirthDate{ get; set; }
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        //Realizado a implementação de Seller para Department (*...1)
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
        //Realizando a associação da classe Seler com Sales Record (1...*)


        //Criando o construtor vázio
        public Seller()
        {

        }

        //Criando o construtor com argumentos
        public Seller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        
        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }
        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }


      
    }
}
