using FlashCards.Services.Common.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlashCards.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LearnController : ControllerBase
    {
        private readonly ILearnService _learnService;

        public LearnController(ILearnService learnService)
        {
            _learnService = learnService;
        }

        [HttpGet]
        public IActionResult GetFlashcards()
        {
            List<object> test = new List<object>();

            _learnService.DrawFlashcards();

            Console.WriteLine();

            return Ok(test);
        }
    }
}
