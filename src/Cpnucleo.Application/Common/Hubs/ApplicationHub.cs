namespace Cpnucleo.Application.Common.Hubs;

public sealed class ApplicationHub(IApplicationDbContext context) : Hub
{
    public async Task Echo(string name, string message)
    {
        //Some business logic here.
        var sistemas = await context.Sistemas
            .Where(x => x.Ativo)
            .OrderBy(x => x.DataInclusao)
            .Select(x => x.MapToDto())
            .ToListAsync();

        await Clients.Client(Context.ConnectionId).SendAsync("echo", name, $"{message} (echo from server)");
    }
}
