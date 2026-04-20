using AutoMapper;
using DTOs;
using Entity;
using Microsoft.Extensions.Logging;
using Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderrRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _imapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderrRepository orderRepository, IMapper imapper, IProductRepository productRepository, ILogger<OrderService> logger)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _imapper = imapper;
            _logger = logger;
        }

        public async Task<OrderDto> addOrder(Order order)
        {
            try
            {
                order.OredrDate = DateOnly.FromDateTime(DateTime.Now);
                double originalSum = order.OrderSum ?? 0;
                double calculatedSum = 0;

                foreach (var item in order.OrdeItems)
                {
                    if (item.ProductId.HasValue)
                    {
                        var product = await _productRepository.GetProductById(item.ProductId.Value);
                        if (product != null && product.Price.HasValue)
                        {
                            calculatedSum += product.Price.Value * (item.Quantity ?? 1);
                        }
                    }
                }

                if (Math.Abs(originalSum - calculatedSum) > 0.01)
                {
                    _logger.LogWarning("Order sum mismatch! Received: {originalSum}, Calculated: {calculatedSum}", originalSum, calculatedSum);
                }

                order.OrderSum = calculatedSum;
                Order addedOrder = await _orderRepository.AddOrder(order);
                return _imapper.Map<OrderDto>(addedOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while placing order for User {UserId}", order.UserId);
                throw;
            }
        }

        public async Task<OrderDto> GetOrderByid(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            return _imapper.Map<OrderDto>(order);
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrders();
            return _imapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserId(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserId(userId);
            return _imapper.Map<IEnumerable<OrderDto>>(orders);
        }

        public async Task UpdateStatus(int id, string status)
        {
            await _orderRepository.UpdateStatus(id, status);
        }
    }
}