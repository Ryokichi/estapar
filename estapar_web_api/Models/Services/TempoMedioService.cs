using estapar_web_api;

public class TempoMedioService : CommomService
{

    public TempoMedioService(EstaparDbContext ctx): base(ctx)
    {
    }

    public List<TempoMedioDTO>ExecutaCalculoDeTempoMedioNoPeriodo(string codGaragem, DateTime dataInicio, DateTime dataFim )
    {
        List<TempoMedioDTO> ListaTempos = new List<TempoMedioDTO>();

        string[] array = { "MEN" };
        var FormasPagamento = new List<string>(array);
        var Estadias = BuscaCarrosNoPeriodoPorFormaPagto(codGaragem, dataInicio, dataFim, FormasPagamento);
        ListaTempos.Add(CalculoTempoMedio("Mensalistas", Estadias));

        FormasPagamento = DB.FormaPagamento.Where(f => f.Codigo != "MEN").Select( f => f.Codigo ).ToList();
        Estadias = BuscaCarrosNoPeriodoPorFormaPagto(codGaragem, dataInicio, dataFim, FormasPagamento );
        ListaTempos.Add(CalculoTempoMedio("Não Mensalistas", Estadias));

        return ListaTempos;
    }

    private TempoMedioDTO CalculoTempoMedio(string nome, List<Passagem> estadias)
    {
        int qtdRegistros = (estadias.Count() > 0)? estadias.Count() : 1;
        int minutos = 0;
        foreach (var estadia in estadias) {
            if (estadia.DataHoraSaida == null)
                continue;
            minutos += GetTimeSpanEmMinutos(estadia.DataHoraEntrada, estadia.DataHoraSaida.Value);
        }
        int MinutosMedio = minutos / qtdRegistros;
        TimeSpan TempoMedio = TimeSpan.FromMinutes(MinutosMedio);
        string TempoMedioFormatado = TempoMedio.ToString("hh\\:mm");

        return GetTempoMedioDTO(nome, TempoMedioFormatado, qtdRegistros);

    }

    private TempoMedioDTO GetTempoMedioDTO (string FormaPagamento, string TempoMedio, int QtdRegistros) {
        return new TempoMedioDTO
        {
            FormaPagamento = FormaPagamento,
            TempoMedio = TempoMedio,
            Registros = QtdRegistros,
        };
    }

    public List<Passagem> BuscaCarrosNoPeriodoPorFormaPagto(string codGaragem, DateTime dataInicio, DateTime dataFim, List<string> codPagamento)
    {
        return DB.Passagem
            .Where(p => p.Garagem.Codigo == codGaragem
                && p.DataHoraEntrada >= dataInicio
                && p.DataHoraSaida <= dataFim
                && codPagamento.Contains(p.FormaPagamento.Codigo)
            )
            .ToList();
    }
}