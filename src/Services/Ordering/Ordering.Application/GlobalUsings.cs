﻿global using System.Data.Common;
global using System.Runtime.Serialization;
global using FluentValidation;
global using MediatR;
global using CleanArchitectureTemplate.Services.Ordering.Application.Behaviors;
global using CleanArchitectureTemplate.Services.Ordering.Application.Commands;
global using CleanArchitectureTemplate.Services.Ordering.Application.IntegrationEvents;
global using CleanArchitectureTemplate.Services.Ordering.Application.IntegrationEvents.EventHandling;
global using CleanArchitectureTemplate.Services.Ordering.Application.IntegrationEvents.Events;
global using CleanArchitectureTemplate.Services.Ordering.Application.Models;
global using CleanArchitectureTemplate.Services.Ordering.Application.Queries;
global using CleanArchitectureTemplate.Services.Ordering.Application.Validations;
global using Microsoft.Extensions.Logging;
global using CleanArchitectureTemplate.BuildingBlocks.EventBus.Extensions;