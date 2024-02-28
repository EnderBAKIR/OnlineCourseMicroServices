using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Services;
using System.Text.Json;

namespace FreeCourse.Services.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketService(RedisService redisService, ISharedIdentityService sharedIdentityService)
        {
            _redisService = redisService;
            _sharedIdentityService = sharedIdentityService;
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("bu kullanıcıya ait sepet bulunamadı", 404);
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var existBasket= await _redisService.GetDb().StringGetAsync(userId);


            if (String.IsNullOrEmpty(existBasket))
            {
                return Response<BasketDto>.Fail("bu kullanıcıya ait sepet bulunamadı", 404);
            }

            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);


        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            

            var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId , JsonSerializer.Serialize(basketDto));

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Sepetiniz kayıt edilemedi veya güncellenemedi daha sonra tekrar deneyiniz", 500);
        }
    }
}
