﻿namespace Cpnucleo.API.Controllers.V2;

[Authorize]
[ApiController]
[ApiVersion("2")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ImpedimentoTarefaController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Listar impedimentos de tarefas
    /// </summary>
    /// <remarks>
    /// # Listar impedimentos de tarefas
    /// 
    /// Lista impedimentos de tarefas da base de dados.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpPost]
    [Route("ListImpedimentoTarefa")]
    public async Task<ActionResult<ListImpedimentoTarefaViewModel>> ListImpedimentoTarefa([FromBody] ListImpedimentoTarefaQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Consultar impedimento de tarefa
    /// </summary>
    /// <remarks>
    /// # Consultar impedimento de tarefa
    /// 
    /// Consulta um impedimento de tarefa na base de dados.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpPost]
    [Route("GetImpedimentoTarefa")]
    public async Task<ActionResult<GetImpedimentoTarefaViewModel>> GetImpedimentoTarefa([FromBody] GetImpedimentoTarefaQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Consultar impedimento de tarefa por tarefa
    /// </summary>
    /// <remarks>
    /// # Consultar impedimento de tarefa por tarefa
    /// 
    /// Consulta um impedimento de tarefa por tarefa na base de dados.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpPost]
    [Route("GetImpedimentoTarefaByTarefa")]
    public async Task<ActionResult<ListImpedimentoTarefaByTarefaViewModel>> GetImpedimentoTarefaByTarefa([FromBody] ListImpedimentoTarefaByTarefaQuery query)
    {
        return await sender.Send(query);
    }

    /// <summary>
    /// Incluir impedimento de tarefa
    /// </summary>
    /// <remarks>
    /// # Incluir impedimento de tarefa
    /// 
    /// Inclui um impedimento de tarefa na base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("CreateImpedimentoTarefa")]
    public async Task<ActionResult<OperationResult>> CreateImpedimentoTarefa([FromBody] CreateImpedimentoTarefaCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// Alterar impedimento de tarefa
    /// </summary>
    /// <remarks>
    /// # Alterar impedimento de tarefa
    /// 
    /// Altera um impedimento de tarefa na base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("UpdateImpedimentoTarefa")]
    public async Task<ActionResult<OperationResult>> UpdateImpedimentoTarefa([FromBody] UpdateImpedimentoTarefaCommand command)
    {
        return await sender.Send(command);
    }

    /// <summary>
    /// Remover impedimento de tarefa
    /// </summary>
    /// <remarks>
    /// # Remover impedimento de tarefa
    /// 
    /// Remove um impedimento de tarefa da base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("RemoveImpedimentoTarefa")]
    public async Task<ActionResult<OperationResult>> RemoveImpedimentoTarefa([FromBody] RemoveImpedimentoTarefaCommand command)
    {
        return await sender.Send(command);
    }
}
