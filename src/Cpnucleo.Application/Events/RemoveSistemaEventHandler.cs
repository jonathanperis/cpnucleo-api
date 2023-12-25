namespace Cpnucleo.Application.Events;

public sealed class RemoveSistemaEventHandler(IApplicationDbContext context) : IMessageReceptionHandler<RemoveSistemaEvent>
{
    public async Task Handle(RemoveSistemaEvent @event, CancellationToken cancellationToken)
    {
        //Some business logic here.
        var sistemas = await context.Sistemas
            .Where(x => x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);
    }
}
