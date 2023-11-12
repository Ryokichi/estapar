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
        int PeriodoMinutos, HorasExtras = 0, MinutosExtras = 0;
        double TotalACobrar;

        PeriodoMinutos = GetTimeSpanEmMinutos(DataInicio, DataFim);

        TotalACobrar = PrecoHora1;
        if (PeriodoMinutos > 60) {
            HorasExtras = (PeriodoMinutos - 60) / 60;
            MinutosExtras = PeriodoMinutos % 60;

            TotalACobrar += HorasExtras * PrecoHoraExtra;
            if (MinutosExtras != 0)
                TotalACobrar += (MinutosExtras > 30) ? PrecoHoraExtra : PrecoHoraExtra/2.0;
        }
        // return Math.Floor(TotalACobrar*100)/100;
        return TotalACobrar;
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