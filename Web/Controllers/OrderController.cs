﻿using Application.Interfaces;
using Application.Models;
using Application.Models.Requests;
using Application.Models.Response;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        
        public ActionResult<OrderWhitPriceResponse> GetById([FromRoute] int id)
        {
            return Ok(_orderService.GetOrderById(id));
        }

        [HttpGet]
        
        public ActionResult<ICollection<OrderWhitPriceResponse>> GetAll()
        {
            return Ok(_orderService.GetAllOrders());
        }

        [HttpGet("OrdersByCustomer/{customerId}")]
        
        public ActionResult<ICollection<OrderDto>> GetByCustomerOrders(int customerId)
        {
            return Ok(_orderService.GetOrdersByCustomer(customerId));
        }

        [HttpGet("OrdersBySeller/{sellerId}")]
        
        public ActionResult<ICollection<OrderDto>> GetByCustomerSeller(int sellerId)
        {
            return Ok(_orderService.GetOrdersBySeller(sellerId));
        }

        [HttpPost("Create/")]
        
        public ActionResult<OrderDto> Create([FromBody] OrderCreateRequest order)
        {
            return _orderService.CreateOrder(order);
        }

        [HttpPut("Update/{id}")]
        
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ChangeOrderRequest changeOrder)
        {
            await _orderService.UpdateOrderProduct(id, changeOrder);
            return Ok();
        }

        [HttpDelete("Delete/{id}")]
        
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            string result = await _orderService.DeleteOrder(id);
            return Ok(result);
        }

        [HttpGet("CheckStock/{productId}")]
        
        public ActionResult<string> CheckStockProduct([FromRoute] int productId)
        {
            return Ok(_orderService.CheckStockProduct(productId));
        }

        [HttpPut("ChangeStatus/{id}")]
        
        public IActionResult ChangeStatus([FromRoute] int id, [FromBody] ChangeStatusOrderRequest changeOrderStatus)
        {
            _orderService.ChangeOrderStatus(id, changeOrderStatus);
            return Ok();
        }
    }
}
