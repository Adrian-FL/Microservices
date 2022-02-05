using BasketService.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using OrderService.Data;
using OrderService.Models.Domain;
using OrderService.Models.Dto;
using OrderService.Models.DTO;
using System.Net.Http;
using System.Text.Json;

namespace OrderService.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {

        public OrderServiceContext Context { get; }
        public IHttpClientFactory httpClientFactory { get; set; }
        public OrdersController(OrderServiceContext context, IHttpClientFactory httpClientFactory)
        {
            Context = context;
            this.httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewOrder(CreateOrderDto newOrderDto)
        {
            var httpRequestMessage = new HttpRequestMessage(
              HttpMethod.Get,
              $"http://localhost:5002/api/baskets/{newOrderDto.Identifier}")
            {
                Headers = { { HeaderNames.Accept, "application/json" }, }
            };

            var httpClient = httpClientFactory.CreateClient();

            using var httpResponseMessage =
                await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {

                Order order = new Order
                {
                    Identifier = newOrderDto.Identifier,
                    Customer = newOrderDto.Customer
                }; 
                Context.Orders.Add(order);

                Context.SaveChanges();

                string serializedBasket = await httpResponseMessage.Content.ReadAsStringAsync();

                var basket = JsonSerializer.Deserialize<UpdateBasketDto>(serializedBasket);
                foreach(var basketProduct in basket.Items)
                {

                    OrderLine orderLine = new OrderLine
                    {
                        OrderId = order.OrderId,
                        ProductId = basketProduct.ProductId,
                        Quantity = basketProduct.Quantity
                    };

                    Context.OrderLines.Add(orderLine);

                    Context.SaveChanges();
                }


                return Created("", new CreatedOrderId
                {
                    OrderId = order.OrderId.ToString()
                });
            }

            throw new Exception();
        }
    }
}
