using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
<<<<<<< HEAD
=======
using SalesWebMVC.Models.ViewsModels;
>>>>>>> 396c89fbeab6f9a489320aba88ba872d948949c1
using SalesWebMVC.Services;
using System.Drawing.Text;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {

        //Criando uma dependencia para o SellerService
        private readonly SellerService _sellerService;

<<<<<<< HEAD
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
=======
        //Criando uma dependencia para o DepartmentService
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
>>>>>>> 396c89fbeab6f9a489320aba88ba872d948949c1
        }

        public IActionResult Index()
        {
            //A ação está retornando toda a lista de vendedores que tem no banco de dados.

            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
<<<<<<< HEAD
            //Criando a ação do botão create Sellers->Index
            //Retornando o que está dentro de Sellers->Create
            return View();
=======
            //Criando a ação do botão create Sellers->
            //Criando a tela de formulário para cadastrar o vendedor
            var departments = _departmentService.FindAll(); // A ação está retornando toda a lista de departamentos que tem no banco de dados.
            var viewModel = new SellerFormViewModel { Departments= departments };
            return View(viewModel);
>>>>>>> 396c89fbeab6f9a489320aba88ba872d948949c1
        }


        //Declarando a ação Post,  uma ação "POST" é uma forma de enviar informações do
        //cliente para o servidor e solicitar que uma determinada ação seja executada com base nessas informações.
        //No caso crir um novo vendedor
        [HttpPost]
        //Em baixo estamos previnindo ataques de roubo de dados.
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            //Recebendo um objeto vendedor que veio da requisição, para isso, basta como o vendedor como paramêtro
            _sellerService.Insert(seller);
            //Redirecionando a requisição para o Index, ação que mostrará novamente a tela principal do crude.
            return RedirectToAction(nameof(Index));
        }
    }
}
