using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IShoppingCartRepository shoppingCartRepository;

        public OrderService(IUnitOfWork unitOfWork, IShoppingCartRepository shoppingCartRepository)
        {
            this.unitOfWork = unitOfWork;
            this.shoppingCartRepository = shoppingCartRepository;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string cartId, Address shippingAddress)
        {
            //Lets get the cart
            var cart = await shoppingCartRepository.GetShoppingCartAsync(cartId);

            //Get items from gamesRepo
            var items = new List<OrderItem>();

            foreach(var item in cart.Items)
            {
                var gameItem = await unitOfWork.Repository<Games>().GetByIdAsync(item.Id);

                var itemOrdered = new ProductItemOrdered(gameItem.Id, gameItem.Name, gameItem.PictureUrl);

                var orderItem = new OrderItem(itemOrdered, gameItem.Price, item.Quantity);

                items.Add(orderItem);
            }

            var deliveryMethod = await unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            var subTotal = items.Sum(item => item.Price * item.Quantity);

            var order = new Order(items, buyerEmail, shippingAddress, subTotal, deliveryMethod);

            unitOfWork.Repository<Order>().Add(order);

            var result = await unitOfWork.Complete();

            if (result <= 0) return null;

            await shoppingCartRepository.DeleteShoppingCartAsync(cartId); 

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var spec = new OrdersWithAndOrderingSpecification(id, buyerEmail);

            return await unitOfWork.Repository<Order>().GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithAndOrderingSpecification(buyerEmail);

            return await unitOfWork.Repository<Order>().ListAsync(spec);
        }
    }
}
