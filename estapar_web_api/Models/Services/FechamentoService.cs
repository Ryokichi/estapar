using estapar_web_api;

public class FechamentoService : CommomService
{
    public FechamentoService(EstaparDbContext ctx): base(ctx)
    {
    }

    public List<FechamentoDTO>ExecutaFecahamentoDoPeriodo(string CodGaragem, DateTime DataInicio, DateTime DataFim )
    {
        // Pega carros no periodo, depois para cada um deles calcula o valor da estadia caso ja nao tenha claculado anteriormente
        // e no caso da necessidade de calcular, utilizar o design abstract factory https://refactoring.guru/pt-br/design-patterns/abstract-factory
        // para criar o meio de pagamento adequado. Armazena tudo isso num objeto que representa o fechamento da garagem no periodo.


        List<FechamentoDTO> Fechamento = new List<FechamentoDTO>();
        List<FormaPagamento> FormasPagamento = DB.FormaPagamento.ToList();
        Garagem DadosGaragem = DB.Garagem.Where(g => g.Codigo == CodGaragem).First();

        double TotalDoFechamento = 0;

        foreach(var FormaPagamento in FormasPagamento) 
        {
            List<Passagem> PassagemDosCarros = BuscaCarrosNoPeriodoPorFormaPagto(CodGaragem, DataInicio, DataFim, FormaPagamento.Codigo);
            double TotalFormaPagto = 0;

        //     ////Analisar o caso de mensalidade
            if(FormaPagamento.Codigo == "MEN") {

            }
            else {
                foreach(var passagem in PassagemDosCarros) {
                    if (passagem.DataHoraSaida == null)
                        continue;

                    DateTime DataHoraEntrada = passagem.DataHoraEntrada;
                    DateTime DataHoraSaida = passagem.DataHoraSaida.Value;
                    double PrimeiraHora = DadosGaragem.Preco_1aHora;
                    double HoraExtra = DadosGaragem.Preco_HorasExtra;

                    double ValorEstadia = CalculaPrecoEstadia(DataHoraEntrada, DataHoraSaida, PrimeiraHora, HoraExtra);
                    TotalFormaPagto += ValorEstadia;
                }
            }

            Fechamento.Add(GetFechamentoDTO(DataInicio, DataFim, FormaPagamento.Descricao, TotalFormaPagto));
            TotalDoFechamento += TotalFormaPagto;
        }
        Fechamento.Add(GetFechamentoDTO(DataInicio, DataFim, "Total Consolidado", TotalDoFechamento));
        return Fechamento;
    }

    private FechamentoDTO GetFechamentoDTO(DateTime DataInicio, DateTime DataFim, string Descricao, double TotalFormaPagto)
    {
        return new FechamentoDTO {
            DataHoraInicio = DataInicio,
            DataHoraFim = DataFim,
            FormaPagamento = Descricao,
            ValorTotal = Math.Truncate(TotalFormaPagto * 100) / 100
        };
    }

}