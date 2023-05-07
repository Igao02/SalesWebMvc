using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewsModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;
using System.Drawing.Text;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {

        //Criando uma dependencia para o SellerService
        private readonly SellerService _sellerService;

        //Criando uma dependencia para o DepartmentService
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            //A ação está retornando toda a lista de vendedores que tem no banco de dados.

            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {

            //Criando a ação do botão create Sellers->
            //Criando a tela de formulário para cadastrar o vendedor
            var departments = _departmentService.FindAll(); // A ação está retornando toda a lista de departamentos que tem no banco de dados.
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }


        //Declarando a ação Post,  uma ação "POST" é uma forma de enviar informações do
        //cliente para o servidor e solicitar que uma determinada ação seja executada com base nessas informações.
        //No caso crir um novo vendedor
        [HttpPost]
        //Em baixo estamos previnindo ataques de roubo de dados.
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            //Recebendo um objeto vendedor que veio da requisição, para isso, basta como o vendedor como paramêtro
            _sellerService.Insert(seller);
            //Redirecionando a requisição para o Index, ação que mostrará novamente a tela principal do crude.
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not provided" });

            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Provided" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            return View(obj);
        }


        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Provided" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            catch(DbConcurrencyException f)
            {
                return RedirectToAction(nameof(Error), new { message = f.Message });
            }
        }

        public IActionResult Error (string message)
        {
            var viewModel = new ErrorViewModel
            {
                //Solicita o Id interno da requisição
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
            
        }

    }
}
