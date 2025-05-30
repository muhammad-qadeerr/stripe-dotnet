using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using StripeDotnetWebApp.Data;
using StripeDotnetWebApp.Models;

namespace StripeDotnetWebApp.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ProductContext _productContext;

        public PaymentController(ProductContext productContext)
        {
            _productContext = productContext;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {
            var products = await _productContext.Products.ToListAsync();
            return View(products);
        }

        [HttpPost]
        public ActionResult CreateCheckout([Bind("Id, Name, ImageUrl, PriceId")] Product product)
        {
            var domain = "https://localhost:7181";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    // Provide the exact Price ID (for example, price_1234) of the product you want to sell
                    Price = $"{product?.PriceId}",
                    Quantity = 1,
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + "/Payment/Success",
                CancelUrl = domain + "/Payment/Cancel",
            };
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public IActionResult Success()
        {
            return View();
        }

        public IActionResult Cancel()
        {
            return View();
        }
    }
}
