﻿using CleanArchitecture.Mediator.Middlewares;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Infrastructure.RequestAudit.Extensions;

namespace Infrastructure.RequestAudit;

public sealed class RequestAuditDecorator<TRequest, TResponse> :
    IRequestProcessor<TRequest, TResponse>
    where TRequest : Request
{
    private readonly IRequestProcessor<TRequest, TResponse> next;
    private readonly RequestAuditAgent commandAudit;
    private readonly string lggingDomain;
    private readonly ILogger logger;

    public RequestAuditDecorator(
        IRequestProcessor<TRequest, TResponse> next,
        RequestAuditAgent commandAudit,
        string loggingDomain,
        ILogger logger)
    {
        this.next = next;
        this.commandAudit = commandAudit;
        lggingDomain = loggingDomain;
        this.logger = logger;
    }

    public async Task<Result<TResponse>> Handle(RequestContext<TRequest> context)
    {
        var actor = context.Actor;
        var request = context.Request;

        var logEntry = request.LogEntry(actor, lggingDomain);
        var timer = new Stopwatch();
        timer.Start();

        using var loggingScope = logger.BeginScope(new
        {
            Domain = lggingDomain,
            Command = typeof(TRequest).Name,
            request.CorrelationId,
            Actor = actor
        });

        try
        {
            var result = await next.Handle(context);
            timer.Stop();

            if (result.IsSuccess)
            {
                var response = result.Value;
                logEntry.Responsed(responseTime: timer.Elapsed, request: request, response: response);
                commandAudit.Post(logEntry);
            }
            else
            {
                logEntry.Failed(result.Errors, responseTime: timer.Elapsed);
                commandAudit.Post(logEntry);
            }


            logger.LogInformation("{Request}", request);

            return result;
        }
        catch (Exception exp)
        {
            timer.Stop();
            logEntry.Failed(exp, timer.Elapsed);
            commandAudit.Post(logEntry);

            throw;
        }
    }
}