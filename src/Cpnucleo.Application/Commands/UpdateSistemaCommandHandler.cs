namespace Cpnucleo.Application.Commands;

public sealed class UpdateSistemaCommandHandler(IApplicationDbContext context) : IRequestHandler<UpdateSistemaCommand, OperationResult>
{
    public async ValueTask<OperationResult> Handle(UpdateSistemaCommand request, CancellationToken cancellationToken)
    {
        var sistema = await context.Sistemas
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (sistema is null)
        {
            return OperationResult.NotFound;
        }

        sistema = Sistema.Update(sistema, request.Nome, request.Descricao);
        context.Sistemas.Update(sistema);

        var result = await context.SaveChangesAsync(cancellationToken);

        return result;
    }
}
