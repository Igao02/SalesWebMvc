using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewsModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;
using System.Diagnostics;
using System.Drawing.Text;
using System.Threading.Tasks;

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

        public async Task <IActionResult> Index()
        {
            //A ação está retornando toda a lista de vendedores que tem no banco de dados.

            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task <IActionResult> Create()
        {

            //Criando a ação do botão create Sellers->
            //Criando a tela de formulário para cadastrar o vendedor
            var departments = await _departmentService.FindAllAsync(); // A ação está retornando toda a lista de departamentos que tem no banco de dados.
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }


        //Declarando a ação Post,  uma ação "POST" é uma forma de enviar informações do
        //cliente para o servidor e solicitar que uma determinada ação seja executada com base nessas informações.
        //No caso crir um novo vendedor
        [HttpPost]
        //Em baixo estamos previnindo ataques de roubo de dados.
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            //Recebendo um objeto vendedor que veio da requisição, para isso, basta como o vendedor como paramêtro
            await _sellerService.InsertAsync(seller);
            //Redirecionando a requisição para o Index, ação que mostrará novamente a tela principal do crude.
            return RedirectToAction(nameof(Index));
        }

        public async Task <IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not provided" });

            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Delete(int id)
        {
            await _sellerService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            return View(obj);
        }


        public async Task <IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id Not Found" });
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments =  await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch" });
            }
            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

            catch(DbConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
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
