public class GaragemDTO
{
    public string Codigo { get; set; }
    public string Nome { get; set; }
}

public class FormaPagamentoDTO
{
    public string Codigo { get; set; }
    public string Descricao { get; set; }
}

public class UserResponse
{
    public string Mensagem { get; set; }
    public object Retorno { get; set; }
}

public class PassagemDTO
{
    public string Garagem { get; set; }
    public string CarroPlaca { get; set; }
    public string CarroMarca { get; set; }
    public string CarroModelo { get; set; }
    public string DataHoraEntrada { get; set; }
    public string DataHoraSaida { get; set; }
    public string FormaPagamento { get; set; }
    public double PrecoTotal { get; set; }
}

public class EstadiaDTO
{
    public string Garagem { get; set; }
    public string CarroPlaca { get; set; }
    public string CarroMarca { get; set; }
    public string CarroModelo { get; set; }
    public DateTime DataHoraEntrada { get; set; }
    public DateTime? DataHoraSaida { get; set; }
    public string TempoEstadia { get; set; }
    public string? FormaPagamento { get; set; }
    public double PrecoTotal { get; set; }
}

public class CarroDTO
{
    public string Garagem { get; set; }
    public string CarroPlaca { get; set; }
    public string CarroMarca { get; set; }
    public string CarroModelo { get; set; }
    public DateTime DataHoraEntrada { get; set; }
    public DateTime? DataHoraSaida { get; set; }
    public string FormaPagamento { get; set;}
    public double PrecoTotal { get; set; }
}

public class UltimaEntradaCarroDTO
{
    public string CarroPlaca { get; set; }
    public DateTime DataHoraEntrada { get; set; }
}

public class RegistroPassagemDTO
{
    public string Mensagem { get; set; }
    public CarroDTO? Passagem { get; set; }
}

public class FechamentoDTO 
{
    public DateTime DataHoraInicio { get; set; }
    public DateTime DataHoraFim { get; set; }
    public string FormaPagamento { get; set; }
    public double PrecoTotal { get; set; }
}

public class TempoMedioDTO 
{
    public string FormaPagamento { get; set;}
    public string TempoMedio { get; set; }
    public int Registros { get; set; }
}