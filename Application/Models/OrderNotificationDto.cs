using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class OrderNotificationDto
    {
        public int Id { get; set; }
    [Required]
    public string Message { get; set; }

    // Método estático para convertir OrderNotification a OrderNotificationDto
    public static OrderNotificationDto ToDto(OrderNotification orderNotification)
    {
        if (orderNotification == null) return null; // Manejo de null si es necesario

        return new OrderNotificationDto
        {
            Id = orderNotification.Id,
            Message = orderNotification.Message
            // Asegúrate de mapear otras propiedades si existen
        };
    }
    }
}
