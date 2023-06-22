﻿using CleanArchitectureTemplate.BuildingBlocks.EventBus.Abstractions;

namespace CleanArchitectureTemplate.Services.Ordering.API.Application.IntegrationEvents.EventHandling;

public class UserCheckoutAcceptedIntegrationEventHandler : IIntegrationEventHandler<UserCheckoutAcceptedIntegrationEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<UserCheckoutAcceptedIntegrationEventHandler> _logger;

    public UserCheckoutAcceptedIntegrationEventHandler(
        IMediator mediator,
        ILogger<UserCheckoutAcceptedIntegrationEventHandler> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// Integration event handler which starts the create order process
    /// </summary>
    /// <param name="@event">
    /// Integration event message which is sent by the
    /// basket once it has successfully process the 
    /// order items.
    /// </param>
    /// <returns></returns>
    public async Task Handle(UserCheckoutAcceptedIntegrationEvent @event)
    {
        using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new ("IntegrationEventContext", @event.Id) }))
        {
            _logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

            var result = false;

            if (@event.RequestId != Guid.Empty)
            {
                using (_logger.BeginScope(new List<KeyValuePair<string, object>> { new ("IdentifiedCommandId", @event.RequestId) }))
                {
                    var createOrderCommand = new CreateOrderCommand(@event.Basket.Items, @event.UserId, @event.UserName, @event.City, @event.Street,
                        @event.State, @event.Country, @event.ZipCode,
                        @event.CardNumber, @event.CardHolderName, @event.CardExpiration,
                        @event.CardSecurityNumber, @event.CardTypeId);

                    var requestCreateOrder = new IdentifiedCommand<CreateOrderCommand, bool>(createOrderCommand, @event.RequestId);

                    _logger.LogInformation(
                        "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                        requestCreateOrder.GetGenericTypeName(),
                        nameof(requestCreateOrder.Id),
                        requestCreateOrder.Id,
                        requestCreateOrder);

                    result = await _mediator.Send(requestCreateOrder);

                    if (result)
                    {
                        _logger.LogInformation("CreateOrderCommand suceeded - RequestId: {RequestId}", @event.RequestId);
                    }
                    else
                    {
                        _logger.LogWarning("CreateOrderCommand failed - RequestId: {RequestId}", @event.RequestId);
                    }
                }
            }
            else
            {
                _logger.LogWarning("Invalid IntegrationEvent - RequestId is missing - {@IntegrationEvent}", @event);
            }
        }
    }
}