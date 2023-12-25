namespace Cpnucleo.Application.Queries;

public sealed class ListSistemaQueryHandler(IApplicationDbContext context, IHubContext<ApplicationHub> hubContext) : IRequestHandler<ListSistemaQuery, ListSistemaViewModel>
{
    public async ValueTask<ListSistemaViewModel> Handle(ListSistemaQuery request, CancellationToken cancellationToken)
    {
        var sistemas = await context.Sistemas
            .AsNoTracking()
            .Where(x => x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync(cancellationToken);

        if (sistemas is null)
        {
            return new ListSistemaViewModel(OperationResult.NotFound);
        }

        await hubContext.Clients.All.SendAsync("broadcastMessage", "Broadcast: Test Message.", "Lorem ipsum dolor sit amet", cancellationToken);

        return new ListSistemaViewModel(OperationResult.Success, sistemas);
    }
}