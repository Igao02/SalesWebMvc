﻿using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Services;
using System.Drawing.Text;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {

        //Criando uma dependencia para o SellerService
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            //A ação está retornando toda a lista de vendedores que tem no banco de dados.

            var list = _sellerService.FindAll();
            return View(list);
        }
    }
}
