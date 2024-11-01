﻿using Domain.Entities;
using ServiceStack.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
   public class OrderNotificationCreateRequest
{
    public string Message { get; set; }

    public static OrderNotification ToEntity(OrderNotificationCreateRequest dto)
    {
        return new OrderNotification
        {
            Message = dto.Message
           
        };
    }
}
}
