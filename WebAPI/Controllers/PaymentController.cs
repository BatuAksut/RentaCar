using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequest request)
        {
            // Burada sahte bir ödeme işlemi gerçekleştirin
            // Gerçek bir ödeme servisi entegrasyonuna ihtiyaç duyulursa, bunu burada gerçekleştirin

            return Ok(new { success = true, message = "Ödeme başarılı!" });
        }
    }

    public class PaymentRequest
    {
        public string CreditCardInfo { get; set; }
    }
}
