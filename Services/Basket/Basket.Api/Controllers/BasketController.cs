using Basket.Api.Entities;
using Basket.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]

    public class BasketController : ControllerBase
    {

        #region Constractor

        private readonly IBasketRepository _basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        #endregion


        #region basketCotroller
        [HttpGet("{userName}", Name = "GetUserBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(string userName)
        {
            var basket = await _basketRepository.GetUserBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
        {
            return Ok(await _basketRepository.UpdateBasket(basket));


        }
        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }

        #endregion

    }
}
