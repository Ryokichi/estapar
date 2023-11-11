using estapar_web_api;

public class TempoMedioService : CommomService
{

    public TempoMedioService(EstaparDbContext ctx): base(ctx)
    {
    }

    public List<TempoMedioDTO>ExecutaCalculoDeTempoMedioNoPeriodo(string CodGaragem, DateTime DataInicio, DateTime DataFim )
    {
        List<TempoMedioDTO> ListaTempos = new List<TempoMedioDTO>();

        string[] array = { "MEN" };
        var FormasPagamento = new List<string>(array);
        var Estadias = BuscaCarrosNoPeriodoPorFormaPagto(CodGaragem, DataInicio, DataFim, FormasPagamento);
        ListaTempos.Add(CalculoTempoMedio("Mensalistas", Estadias));

        FormasPagamento = DB.FormaPagamento.Where(f => f.Codigo != "MEN").Select( f => f.Codigo ).ToList();
        Estadias = BuscaCarrosNoPeriodoPorFormaPagto(CodGaragem, DataInicio, DataFim, FormasPagamento );
        ListaTempos.Add(CalculoTempoMedio("Não Mensalistas", Estadias));

        return ListaTempos;
    }

    private TempoMedioDTO CalculoTempoMedio(string Nome, List<Passagem> Estadias)
    {
        int QtdRegistros = (Estadias.Count() > 0)? Estadias.Count() : 1;
        int Minutos = 0;
        foreach (var Estadia in Estadias) {
            if (Estadia.DataHoraSaida == null)
                continue;
            Minutos += GetTimeSpanEmMinutos(Estadia.DataHoraEntrada, Estadia.DataHoraSaida.Value);
        }
        int MinutosMedio = Minutos / QtdRegistros;
        TimeSpan TempoMedio = TimeSpan.FromMinutes(MinutosMedio);
        string TempoMedioFormatado = TempoMedio.ToString("hh\\:mm");

        return GetTempoMedioDTO(Nome, TempoMedioFormatado, QtdRegistros);

    }

    private TempoMedioDTO GetTempoMedioDTO (string FormaPagamento, string TempoMedio, int QtdRegistros) {
        return new TempoMedioDTO
        {
            FormaPagamento = FormaPagamento,
            TempoMedio = TempoMedio,
            Registros = QtdRegistros,
        };
    }

    public List<Passagem> BuscaCarrosNoPeriodoPorFormaPagto(string CodGaragem, DateTime DataInicio, DateTime DataFim, List<string> CodPagamento)
    {
        return DB.Passagem
            .Where(p => p.Garagem.Codigo == CodGaragem
                && p.DataHoraEntrada >= DataInicio
                && p.DataHoraSaida <= DataFim
                && CodPagamento.Contains(p.FormaPagamento.Codigo)
            )
            .ToList();
    }
}