using estapar_web_api;

public class CommomService
{
    protected EstaparDbContext DB;

    public CommomService(EstaparDbContext ctx)
    {
        this.DB = ctx;
    }

    public List<Garagem> GetTodasGaragens()
    {
        return DB.Garagem.ToList();
    }

    public List<FormaPagamento> GetTodosPagamentos()
    {
        return DB.FormaPagamento.ToList();
    }

    public List<Passagem> GetTodasPassagens()
    {
        return DB.Passagem.ToList();
    }

    public int GetTimeSpanEmMinutos(DateTime DataInicio, DateTime DataFim)
    {
        TimeSpan Diferenca = DataInicio - DataFim;
        int SpanHoras = Diferenca.Duration().Days * 24 + Diferenca.Duration().Hours;
        int SpanMinutos = SpanHoras * 60 + Diferenca.Duration().Minutes;
        return SpanMinutos;
    }

    public double CalculaPrecoEstadia( Garagem garagem, Passagem passagem)
    {
        if (passagem.FormaPagamento.Codigo == "MEN" || passagem.DataHoraSaida == null)
            return 0;

        DateTime DataInicio = passagem.DataHoraEntrada;
        DateTime DataFim = passagem.DataHoraSaida.Value;
        double PrecoHora1 = garagem.Preco_1aHora;
        double PrecoHoraExtra = garagem.Preco_HorasExtra;
        double TotalACobrar;
        int PeriodoMinutos, Horas, Minutos;

        PeriodoMinutos = GetTimeSpanEmMinutos(DataInicio, DataFim);
        TotalACobrar = PrecoHora1;
        if (PeriodoMinutos > 60) {
            Horas =  (PeriodoMinutos / 60) - 1;
            Minutos = PeriodoMinutos % 60;

            TotalACobrar += (Horas >= 1) ? Horas * PrecoHoraExtra : 0;
            if (Minutos > 0)
                TotalACobrar += (Minutos > 30) ? PrecoHoraExtra : PrecoHoraExtra/2.0;
        }
        // TotalACobrar = Math.Truncate(TotalACobrar * 100.0 ) / 100;
        TotalACobrar = Math.Round(TotalACobrar, 2);
        return TotalACobrar;
    }

    public List<Passagem> BuscaCarrosNoPeriodoPorFormaPagto(string codGaragem, DateTime dataInicio, DateTime dataFim, String pagamento )
    {
        return DB.Passagem
            .Where(p => p.Garagem.Codigo == codGaragem
                && p.FormaPagamento.Codigo == pagamento
                && p.DataHoraSaida >= dataInicio
                && p.DataHoraSaida <= dataFim
            )
            .ToList();
    }
}