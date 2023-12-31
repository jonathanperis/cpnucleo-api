﻿namespace Cpnucleo.API.Controllers.V2;

[Authorize]
[ApiController]
[ApiVersion("2")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class TarefaController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Listar tarefas
    /// </summary>
    /// <remarks>
    /// # Listar tarefas
    /// 
    /// Lista tarefas da base de dados.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpPost]
    [Route("ListTarefa")]
    public async Task<ActionResult<ListTarefaViewModel>> ListTarefa([FromBody] ListTarefaQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Consultar tarefa
    /// </summary>
    /// <remarks>
    /// # Consultar tarefa
    /// 
    /// Consulta um tarefa na base de dados.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpPost]
    [Route("GetTarefa")]
    public async Task<ActionResult<GetTarefaViewModel>> GetTarefa([FromBody] GetTarefaQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Consultar tarefa por recurso
    /// </summary>
    /// <remarks>
    /// # Consultar tarefa por recurso
    /// 
    /// Consulta um tarefa por recurso na base de dados.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpPost]
    [Route("GetTarefaByRecurso")]
    public async Task<ActionResult<ListTarefaByRecursoViewModel>> GetTarefaByRecurso([FromBody] ListTarefaByRecursoQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Incluir tarefa
    /// </summary>
    /// <remarks>
    /// # Incluir tarefa
    /// 
    /// Inclui um tarefa na base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("CreateTarefa")]
    public async Task<ActionResult<OperationResult>> CreateTarefa([FromBody] CreateTarefaCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// Alterar tarefa
    /// </summary>
    /// <remarks>
    /// # Alterar tarefa
    /// 
    /// Altera um tarefa na base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("UpdateTarefa")]
    public async Task<ActionResult<OperationResult>> UpdateTarefa([FromBody] UpdateTarefaCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// Alterar workflow da tarefa
    /// </summary>
    /// <remarks>
    /// # Alterar workflow da tarefa
    /// 
    /// Altera um workflow da tarefa na base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("UpdateTarefaByWorkflow")]
    public async Task<ActionResult<OperationResult>> UpdateTarefaByWorkflow([FromBody] UpdateTarefaByWorkflowCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// Remover tarefa
    /// </summary>
    /// <remarks>
    /// # Remover tarefa
    /// 
    /// Remove um tarefa da base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("RemoveTarefa")]
    public async Task<ActionResult<OperationResult>> RemoveTarefa([FromBody] RemoveTarefaCommand command)
    {
        return await sender.Send(command);
    }
}
