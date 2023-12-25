namespace Cpnucleo.Application.Commands;

public sealed class RemoveSistemaCommandHandler(IApplicationDbContext context, IEventManager eventHandler) : IRequestHandler<RemoveSistemaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(RemoveSistemaCommand request, CancellationToken cancellationToken)
    {
        var sistema = await context.Sistemas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (sistema is null)
        {
            return OperationResult.NotFound;
        }

        sistema = Sistema.Remove(sistema);
        context.Sistemas.Update(sistema); //JONATHAN - Soft Delete.

        var result = await context.SaveChangesAsync(cancellationToken);

        if (result == OperationResult.Success)
        {
            await eventHandler.PublishEventAsync(new RemoveSistemaEvent(sistema.Id, sistema.Nome!));
        }

        return result;
    }
}
