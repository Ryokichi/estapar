using System.Data;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class EstaparAdminController : ControllerBase
{
    [HttpGet("{codGaragem}/carros-no-periodo")]
    public IActionResult getCarrosPeriodo(string codGaragem, [FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim, [FromServices] CarroService dbService)
    {
        List<CarroDTO> carrosDto = dbService.BuscaCarrosNaGaragemPorPeriodo(codGaragem, dataInicio, dataFim);
        return Ok(carrosDto);
    }

    [HttpGet("{codGaragem}/carros-estacionados")]
    public IActionResult getCarrosEstacionados(string codGaragem, [FromServices] CarroService dbService)
    {
        List<CarroDTO> carrosDto = dbService.BuscaCarrosEstacionados(codGaragem);
        return Ok(carrosDto);
    }

    [HttpGet("{codGaragem}/carros-que-passaram")]
    public IActionResult getCarrosQuePassaram(string codGaragem, [FromServices] CarroService dbService)
    {
        List<CarroDTO> carrosDto = dbService.BuscaCarrosQuePassaram(codGaragem);
        return Ok(carrosDto);
    }

    [HttpGet("{codGaragem}/fechamento-periodo")]
    public IActionResult getFechamentoPeriodo(string codGaragem, [FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim, [FromServices] FechamentoService dbService)
    {
        List<FechamentoDTO> fechamentoDto = dbService.ExecutaFecahamentoDoPeriodo(codGaragem, dataInicio, dataFim);
        return Ok(fechamentoDto);
    }

    [HttpGet("{codGaragem}/tempo-medio")]
    public IActionResult getTempoMedio(string codGaragem, [FromQuery] DateTime dataInicio, [FromQuery] DateTime dataFim, [FromServices] TempoMedioService dbService)
    {
        List<TempoMedioDTO> tempoMedioDto = dbService.ExecutaCalculoDeTempoMedioNoPeriodo(codGaragem, dataInicio, dataFim);
        return Ok(tempoMedioDto);
    }

    [HttpPost("{codGaragem}/registrar-entrada")]
    public IActionResult postNovatEntradaCarro(string codGaragem, [FromBody] PassagemCadastroEntrada dadosDaEntrada, [FromServices] CarroService dbService)
    {
        RegistroPassagemDTO postCadastroNovaPassagem = dbService.PostCadastroNovaPassagem(codGaragem, dadosDaEntrada);
        return Ok(postCadastroNovaPassagem);
    }

    [HttpPost("{codGaragem}/registrar-saida")]
    public IActionResult posSaidaCarrostring(string codGaragem, [FromBody] PassagemCadastroSaida dadosDaSaida, [FromServices] CarroService dbService)
    {
        RegistroPassagemDTO tempoMedioDto = dbService.PostCadastroSaidaPassagem(codGaragem, dadosDaSaida);
        return Ok(tempoMedioDto);
    }

    [HttpPost("seed-passagem-db")]
    public IActionResult postSeedPassagemDB([FromBody] List<PassagemDTO> dadosDaEntrada, [FromServices] CarroService dbService)
    {
        object postCadastroNovasPassagens = dbService.PostSeedPassagemDB(dadosDaEntrada);
        return Ok(postCadastroNovasPassagens);
    }

    [HttpGet("lista-todas-garagens")]
    public IActionResult getListaGaragens([FromServices] CommomService dbService)
    {
        List<Garagem> garagens = dbService.GetTodasGaragens();
        return Ok(garagens);
    }

    [HttpGet("lista-todos-pagamentos")]
    public IActionResult getListaPagamentos([FromServices] CarroService dbService)
    {
        List<FormaPagamento> pagamentos = dbService.GetTodosPagamentos();
        return Ok(pagamentos);
    }

    [HttpGet("lista-todas-passagens")]
    public IActionResult getListapassagens([FromServices] CarroService dbService)
    {
        List<Passagem> passagens = dbService.GetTodasPassagens();
        return Ok(passagens);
    }

}