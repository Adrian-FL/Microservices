using BasketService.Models;
using BasketService.Models.DTO;
using FreakyFashionServicesTest.CatalogService.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace BasketService.Controllers
{
    [Route("api/baskets")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        public IHttpClientFactory httpClientFactory { get; set; }
        public IDistributedCache Cache { get; }

        public RegistrationController(IDistributedCache cache, IHttpClientFactory httpClientFactory)
        {
            Cache = cache;
            this.httpClientFactory = httpClientFactory;
        }


        [HttpPut("{identifier}")]
        public async Task<IActionResult> UpdateBasket(string identifier, UpdateBasketDto updateBasketDto)
        {
            var httpClient = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage(
         HttpMethod.Get,
         $"http://localhost:5001/api/products") 
            {
                Headers = { { HeaderNames.Accept, "application/json" }, }
            };


            using var httpResponseMessage =
                await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
             
                string serializedProducts = await httpResponseMessage.Content.ReadAsStringAsync();
                if (serializedProducts == null) 
                {
                    throw new Exception();
                }
                else
                {
                    List<ProductDto> allProducts = JsonSerializer.Deserialize<List<ProductDto>>(serializedProducts);
                  
                    Product[] validatedItems = new Product[allProducts.Count]; 
                    int i = 0;

                    foreach (var basketItem in updateBasketDto.Items)
                    {
                        if (allProducts.Exists(product => product.Id == basketItem.ProductId))
                        {
                            validatedItems[i] = new Product
                            {
                                ProductId = basketItem.ProductId,
                                Quantity = basketItem.Quantity
                            };
                            i++;
                        }
                    }
                    // copy the validated list into the updateBasketDto
                    updateBasketDto.Items = new Product[i];
                    for(int j = 0; j < i; j++)
                    {
                        updateBasketDto.Items[j] = validatedItems[j];
                    }

                    var serializedBasket = JsonSerializer.Serialize(updateBasketDto);

                    Cache.SetString(updateBasketDto.Identifier, serializedBasket);

                    return new NoContentResult(); // 204 No Content
                }
               
            }
            else
            {
                throw new Exception();
            }      
        }


        [HttpGet("{identifier}")]
        public IActionResult GetBasket(string identifier)
        {

            var serialized = Cache.GetString(identifier);

            if (serialized == null)
                return NotFound(); // 404 Not Found

            UpdateBasketDto? basket = JsonSerializer.Deserialize<UpdateBasketDto>(serialized);

            return Ok(basket);
        }
    }
}
