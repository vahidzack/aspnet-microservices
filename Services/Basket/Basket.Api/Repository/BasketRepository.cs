using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Basket.Api.Repository
{
    public class BasketRepository : IBasketRepository
    {

        #region constractor
        private readonly IDistributedCache _distributedCache;
        public BasketRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }
        #endregion


        #region Basket
        public async Task DeleteBasket(string userName)
        {
            await _distributedCache.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetUserBasket(string userName)
        {
            var basket = await _distributedCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _distributedCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetUserBasket(basket.UserName);
        }
        #endregion


    }
}
