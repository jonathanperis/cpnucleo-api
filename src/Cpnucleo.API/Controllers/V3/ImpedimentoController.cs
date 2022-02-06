﻿using Cpnucleo.Application;
using Cpnucleo.Application.Commands.Impedimento.CreateImpedimento;
using Cpnucleo.Application.Commands.Impedimento.RemoveImpedimento;
using Cpnucleo.Application.Commands.Impedimento.UpdateImpedimento;
using Cpnucleo.Application.Queries.Impedimento.GetImpedimento;
using Cpnucleo.Application.Queries.Impedimento.ListImpedimento;

namespace Cpnucleo.API.Controllers.V3;

//[Authorize]
[ApiController]
[ApiVersion("3")]
[Produces("application/json")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ImpedimentoController : ControllerBase
{
    private readonly IMediator _mediator;

    public ImpedimentoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Listar impedimentos
    /// </summary>
    /// <remarks>
    /// # Listar impedimentos
    /// 
    /// Lista impedimentos da base de dados.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpGet]
    [Route("ListImpedimento")]
    public async Task<ActionResult<ListImpedimentoViewModel>> ListImpedimento([FromQuery] ListImpedimentoQuery query)
    {
        return await _mediator.Send(query);
    }

    /// <summary>
    /// Consultar impedimento
    /// </summary>
    /// <remarks>
    /// # Consultar impedimento
    /// 
    /// Consulta um impedimento na base de dados.
    /// </remarks>
    /// <param name="query">Objeto de consulta com os parametros necessários</param>        
    [HttpGet]
    [Route("GetImpedimento")]
    public async Task<ActionResult<GetImpedimentoViewModel>> GetImpedimento([FromQuery] GetImpedimentoQuery query)
    {
        return await _mediator.Send(query);
    }

    /// <summary>
    /// Incluir impedimento
    /// </summary>
    /// <remarks>
    /// # Incluir impedimento
    /// 
    /// Inclui um impedimento na base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPost]
    [Route("CreateImpedimento")]
    public async Task<ActionResult<OperationResult>> CreateImpedimento([FromBody] CreateImpedimentoCommand command)
    {
        return await _mediator.Send(command);
    }

    /// <summary>
    /// Alterar impedimento
    /// </summary>
    /// <remarks>
    /// # Alterar impedimento
    /// 
    /// Altera um impedimento na base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpPut]
    [Route("UpdateImpedimento")]
    public async Task<ActionResult<OperationResult>> UpdateImpedimento([FromBody] UpdateImpedimentoCommand command)
    {
        return await _mediator.Send(command);
    }

    /// <summary>
    /// Remover impedimento
    /// </summary>
    /// <remarks>
    /// # Remover impedimento
    /// 
    /// Remove um impedimento da base de dados.
    /// </remarks>
    /// <param name="command">Objeto de envio com os parametros necessários</param>        
    [HttpDelete]
    [Route("RemoveImpedimento")]
    public async Task<ActionResult<OperationResult>> RemoveImpedimento([FromBody] RemoveImpedimentoCommand command)
    {
        return await _mediator.Send(command);
    }
}
