﻿namespace CleanArchitectureTemplate.Services.Ordering.Application.Validations;

public class IdentifiedCommandValidator : AbstractValidator<IdentifiedCommand<CreateOrderCommand, bool>>
{
    public IdentifiedCommandValidator(ILogger<IdentifiedCommandValidator> logger)
    {
        RuleFor(command => command.Id).NotEmpty();

        logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
    }
}
