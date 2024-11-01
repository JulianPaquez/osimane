using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Application.Services
{
    public class OrderNotificationService : IOrderNotificationService
    {
        private readonly IOrderNotificationRepository _orderNotificationRepository;

        public OrderNotificationService(IOrderNotificationRepository orderNotificationRepository)
        {
            _orderNotificationRepository = orderNotificationRepository;
        }

        public async Task<List<OrderNotificationDto>> GetOrderNotificationById(int id)
        {
                var notifications = await _orderNotificationRepository.GetListNotificationsByOrderId(id);

    
                var notificationDtos = new List<OrderNotificationDto>();

    
                foreach (var notification in notifications)
            {
        
                if (notification != null)
                {
                    var dto = OrderNotificationDto.ToDto(notification);
                    notificationDtos.Add(dto);
                }
            }

            return notificationDtos;
            

    
        }
        


        public OrderNotificationDto CreateOrderNotification(OrderNotificationCreateRequest dto)
        {
             var orderNotification = OrderNotificationCreateRequest.ToEntity(dto);

        
            _orderNotificationRepository.AddAsync(orderNotification); // Debes implementar esto como un método sincrónico

        
            var notificationDto = OrderNotificationDto.ToDto(orderNotification);
        
            return notificationDto;
        }
    }

    

    
}
