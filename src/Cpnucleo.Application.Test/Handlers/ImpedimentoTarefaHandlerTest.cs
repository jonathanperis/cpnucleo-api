﻿namespace Cpnucleo.Application.Test.Handlers;

public class ImpedimentoTarefaHandlerTest
{
    [Fact]
    public async Task CreateImpedimentoTarefaCommand_Handle_Success()
    {
        // Arrange
        var context = DbContextHelper.GetContext();

        var tarefa = context.Tarefas.First();
        var impedimento = context.Impedimentos.First();

        var request = MockCommandHelper.GetNewCreateImpedimentoTarefaCommand(tarefa.Id, impedimento.Id);

        // Act
        CreateImpedimentoTarefaCommandHandler handler = new(context);
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(response == OperationResult.Success);
    }

    [Fact]
    public async Task GetImpedimentoTarefaQuery_Handle_Success()
    {
        // Arrange
        var context = DbContextHelper.GetContext();

        var impedimentoTarefa = context.ImpedimentoTarefas.First();

        var request = MockQueryHelper.GetNewGetImpedimentoTarefaQuery(impedimentoTarefa.Id);

        // Act
        GetImpedimentoTarefaQueryHandler handler = new(context);
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(response.ImpedimentoTarefa != null);
        Assert.True(response.ImpedimentoTarefa.Id != Guid.Empty);
        Assert.True(response.ImpedimentoTarefa.DataInclusao.Ticks != 0);
    }

    [Fact]
    public async Task ListImpedimentoTarefaQuery_Handle_Success()
    {
        // Arrange
        var context = DbContextHelper.GetContext();

        var request = MockQueryHelper.GetNewListImpedimentoTarefaQuery();

        // Act
        ListImpedimentoTarefaQueryHandler handler = new(context);
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(response.ImpedimentoTarefas != null);
        Assert.True(response.ImpedimentoTarefas.Count != 0);
    }

    [Fact]
    public async Task RemoveImpedimentoTarefaCommand_Handle_Success()
    {
        // Arrange
        var context = DbContextHelper.GetContext();

        var impedimentoTarefa = context.ImpedimentoTarefas.First();

        var request = MockCommandHelper.GetNewRemoveImpedimentoTarefaCommand(impedimentoTarefa.Id);
        var request2 = MockQueryHelper.GetNewGetImpedimentoTarefaQuery(impedimentoTarefa.Id);

        // Act
        RemoveImpedimentoTarefaCommandHandler handler = new(context);
        var response = await handler.Handle(request, CancellationToken.None);

        GetImpedimentoTarefaQueryHandler handler2 = new(context);
        var response2 = await handler2.Handle(request2, CancellationToken.None);

        // Assert
        Assert.True(response == OperationResult.Success);
        Assert.True(response2.OperationResult == OperationResult.NotFound);
    }

    [Fact]
    public async Task UpdateImpedimentoTarefaCommand_Handle_Success()
    {
        // Arrange
        var context = DbContextHelper.GetContext();

        var tarefa = context.Tarefas.First();
        var impedimento = context.Impedimentos.First();
        var impedimentoTarefa = context.ImpedimentoTarefas.First();

        var request = MockCommandHelper.GetNewUpdateImpedimentoTarefaCommand(tarefa.Id, impedimento.Id, impedimentoTarefa.Id);
        var request2 = MockQueryHelper.GetNewGetImpedimentoTarefaQuery(impedimentoTarefa.Id);

        // Act
        UpdateImpedimentoTarefaCommandHandler handler = new(context);
        var response = await handler.Handle(request, CancellationToken.None);

        GetImpedimentoTarefaQueryHandler handler2 = new(context);
        var response2 = await handler2.Handle(request2, CancellationToken.None);

        // Assert
        Assert.True(response == OperationResult.Success);
        Assert.True(response2.ImpedimentoTarefa != null);
        Assert.True(response2.ImpedimentoTarefa.Id == impedimentoTarefa.Id);
    }

    [Fact]
    public async Task ListImpedimentoTarefaByTarefaQuery_Handle_Success()
    {
        // Arrange
        var context = DbContextHelper.GetContext();

        var tarefa = context.Tarefas.First();

        var request = MockQueryHelper.GetNewListImpedimentoTarefaByTarefaQuery(tarefa.Id);

        // Act
        ListImpedimentoTarefaByTarefaQueryHandler handler = new(context);
        var response = await handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.True(response.ImpedimentoTarefas != null);
        Assert.True(response.ImpedimentoTarefas.Count != 0);
    }
}
