using BookstoreApp.Application.DTOs;
using BookstoreApp.Application.Interfaces;
using BookstoreApp.Application.Interfaces.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly IOrderRepository _orderRepository;

        public PaymentController(IVnPayService vnPayService, IOrderRepository orderRepository)
        {
            _vnPayService = vnPayService;
            _orderRepository = orderRepository;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentInformation model)
        {
            if (model == null || model.Amount <= 0)
            {
                return BadRequest("Invalid payment information");
            }

            var ipAddress = "127.0.0.1";
            var url = _vnPayService.CreatePaymentUrl(model, ipAddress);

            return Ok(new { paymentUrl = url.Result });
        }


        [HttpGet("callback")]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            if (response.Success)
            {
                await _orderRepository.UpdatePaymentStatusAsync(response.OrderId, "Paid");
                return Redirect("http://localhost:3000/checkout/success");
            }
            else
            {
                return Redirect("http://localhost:3000/checkout/fail");
            }
        }
    }
}
