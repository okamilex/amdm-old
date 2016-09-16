using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace AMDM1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public partial class ProductList : System.Web.UI.Page
        {
            protected void Page_Load(object sender, EventArgs e)
            {
            }
            
        public IQueryable<Performer> GetPerformers([QueryString("id")] int? categoryId)
            {
                var _db = new AmdmContext();
                IQueryable<Performer> query = _db.Performers;
                if (categoryId.HasValue && categoryId > 0)
                {
                    query = query.Where(p => p.Id == categoryId);
                }
                return query; }
        }
    

    public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}