using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Business.Abstract.Services;
using WebApp.Model.Entities;

namespace WebApp.Controllers
{
    public class SampleController : Controller
    {
        private readonly ISampleService sampleService;

        public SampleController(ISampleService sampleService)
        {
            this.sampleService = sampleService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await sampleService.GetAll());
        }
        
        public IActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Sample sample)
        {
            if (!ModelState.IsValid)
            {
                return View(sample);
            }

            var result = sampleService.Add(sample);

            if (!result.IsSuccess)
            {
                return View(sample);
            }

            return RedirectToAction("Index");
        }
    }
}