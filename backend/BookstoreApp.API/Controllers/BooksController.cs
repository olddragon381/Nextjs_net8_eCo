using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace BookstoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookService _bookService;
        private readonly IRecommendationSystem  _recommendationSystem;

        public BooksController(IBookService bookService, IRecommendationSystem recommendationSystem)
        {
            _bookService = bookService;
            _recommendationSystem = recommendationSystem;
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetPagedBooks([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] List<string>? genre = null)
        {
            

            var genreList = genre ?? new List<string>();
            if (genreList.Count > 0 && genreList[0] == null)
            {
                genreList = new List<string>();
            }

            var result = await _bookService.GetBooksPagingAsync(page, pageSize, genreList);
            return Ok(result);
        }


        [HttpGet("get8productnew")]
        public async Task<IActionResult> Get8ProductNew()
        {


            var result = await _bookService.Get8ProductNew();
            return Ok(result);
        }

        [HttpGet("getproduct/{id}")]
        public async Task<IActionResult> GetProduct(string id)
       {


            var result = await _bookService.GetProductAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpGet("getProductHero")]
        public async Task<IActionResult> GetProductForHero()
        {


            var result = await _bookService.GetProductForHero();
   

            if (result == null)
                return NotFound();
            return Ok(result);
        }



        [HttpGet("recomend/{id}")]
        public async Task<IActionResult> RecomendProduct(string id)
        {


            var results = await _recommendationSystem.GetRecommendationsAsync(id);
            return Ok(results);
        }
    }
}
