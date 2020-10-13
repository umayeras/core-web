using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Business.Abstract.Services;

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
    }
}