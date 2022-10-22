using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Review.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : Controller
    {
        public ReviewController()
        {

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json("Hello world!");
        }
    }
}