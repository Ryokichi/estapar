public interface ITempoMedioService
{
    public List<Passagem> BuscaCarrosNoPeriodoPorFormaPagto(string codGaragem, DateTime dataInicio, DateTime dataFim, List<string> codPagamento);
}