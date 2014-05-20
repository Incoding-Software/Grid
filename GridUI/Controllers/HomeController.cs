using System.Linq;
using System.Web.Mvc;
using Grid.Paging;
using GridUI.Models;
using GridUI.Queries;
using Incoding.MvcContrib;

namespace GridUI.Controllers
{
    public class HomeController : IncControllerBase
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult New()
        {
            return View("New");
        }

        public ActionResult GetProducts(GetProductsQuery query)
        {
            var products = this.dispatcher.Query(query);
            return IncJson(products);
        }

        public ActionResult GetProductsWithPaging(GetProductsPagingQuery query)
        {
            var result = this.dispatcher.Query(query);
            var items = result.Items.ToList();
            var model = new PagingResult<ProductVm>(items.Select(r => new ProductVm(r)).ToList(), query, result.TotalCount);
            return IncJson(model);
        }

        public ActionResult GetNullProducts(GetProductsQuery query)
        {
            var products = this.dispatcher.Query(query);
            return IncJson(products.Take(0));
        }
    }
}