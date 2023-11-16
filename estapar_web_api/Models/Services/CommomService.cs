using System.ComponentModel;
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

    public static string calculaTempoEstadia(Passagem passagem)
    {
        TimeSpan tempo = new TimeSpan();
        if (passagem.DataHoraSaida != null) {
            tempo = passagem.DataHoraSaida.Value - passagem.DataHoraEntrada;
        }
        return String.Format("{0:00}:{1:00}", (tempo.Days*24 + tempo.Hours), tempo.Minutes);
    }

    public static int GetTimeSpanEmMinutos(DateTime dataInicio, DateTime dataFim)
    {
        TimeSpan diferenca = dataInicio - dataFim;
        int spanHoras = diferenca.Duration().Days * 24 + diferenca.Duration().Hours;
        int spanMinutos = spanHoras * 60 + diferenca.Duration().Minutes;
        return spanMinutos;
    }

    public static double CalculaPrecoEstadia(Passagem passagem, Garagem garagem, FormaPagamento? formaPagamento)
    {
        if (formaPagamento == null || formaPagamento.Codigo == "MEN" || passagem.DataHoraSaida == null)
        {
            return 0;
        }
        if (passagem.PrecoTotal > 0)
        {
            return passagem.PrecoTotal;
        }

        DateTime dataInicio = passagem.DataHoraEntrada;
        DateTime dataFim = passagem.DataHoraSaida.Value;
        double PrecoHora1 = garagem.Preco_1aHora;
        double PrecoHoraExtra = garagem.Preco_HorasExtra;
        double TotalACobrar;
        int PeriodoMinutos, Horas, Minutos;

        PeriodoMinutos = GetTimeSpanEmMinutos(dataInicio, dataFim);
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

}