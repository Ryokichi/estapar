using estapar_web_api;

public class CommomService
{
    protected EstaparDbContext DB;

    public CommomService(EstaparDbContext ctx)
    {
        this.DB = ctx;
    }

    public int GetTimeSpanEmMinutos(DateTime DataInicio, DateTime DataFim)
    {
        TimeSpan Diferenca = DataInicio - DataFim;
        int SpanHoras = Diferenca.Duration().Days * 24 + Diferenca.Duration().Hours;
        int SpanMinutos = SpanHoras * 60 + Diferenca.Duration().Minutes;
        return SpanMinutos;
    }

    public double CalculaPrecoEstadia(DateTime DataInicio, DateTime DataFim, double PrecoHora1, double PrecoHoraExtra)
    {
        int PeriodoMinutos, HorasExtras, MinutosExtras;
        double TotalACobrar;

        PeriodoMinutos = GetTimeSpanEmMinutos(DataInicio, DataFim);

        TotalACobrar = PrecoHora1;
        if (PeriodoMinutos > 60) {
            HorasExtras = (PeriodoMinutos - 60) / 60;
            MinutosExtras = PeriodoMinutos % 60;

            TotalACobrar += HorasExtras * PrecoHoraExtra;
            TotalACobrar += (MinutosExtras > 30) ? PrecoHoraExtra : PrecoHoraExtra/2.0;
        }

        return Math.Truncate(TotalACobrar * 100) / 100;
    }

    public List<Passagem> BuscaCarrosNoPeriodoPorFormaPagto(string CodGaragem, DateTime DataInicio, DateTime DataFim, String Pagamento )
    {
        return DB.Passagem
            .Where(p => p.Garagem.Codigo == CodGaragem
                && p.FormaPagamento.Codigo == Pagamento
                && p.DataHoraSaida >= DataInicio
                && p.DataHoraSaida <= DataFim
            )
            .ToList();
    }
}