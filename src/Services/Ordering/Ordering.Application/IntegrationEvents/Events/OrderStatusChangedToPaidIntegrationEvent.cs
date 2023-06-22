﻿using CleanArchitectureTemplate.BuildingBlocks.EventBus.Events;

namespace CleanArchitectureTemplate.Services.Ordering.Application.IntegrationEvents.Events;

public record OrderStatusChangedToPaidIntegrationEvent : IntegrationEvent
{
    public int OrderId { get; }
    public string OrderStatus { get; }
    public string BuyerName { get; }
    public IEnumerable<OrderStockItem> OrderStockItems { get; }

    public OrderStatusChangedToPaidIntegrationEvent(int orderId,
        string orderStatus,
        string buyerName,
        IEnumerable<OrderStockItem> orderStockItems)
    {
        OrderId = orderId;
        OrderStockItems = orderStockItems;
        OrderStatus = orderStatus;
        BuyerName = buyerName;
    }
}

