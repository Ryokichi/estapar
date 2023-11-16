----------------------------------------------------------------------------------------------------
Infelizmente não consegui consegui fazer a aplicação rodar inteiramente em Docker,
apenas o banco de dados está rodando no container. Quanto ao ambiente que rodaria a 
aplicação web não consegui determinar com certeza se o problema era com o acesso a
porta ou a compilação incorreta da aplicação.
Dada essa situação, para que a aplicação rode, é necessário que o projeto esteja em
um ambiente com SO Windows, .net core instalado e docker.

Para iniciar o projeto será necessário entrar na raiz por um terminal e executar os
seguintes comandos:

docker composer up -d       ////Inicia o container onde está o banco de dados
cd ./estapar_web_api        ////Acessa a pasta do projeto principal
dotnet ef database update   ////Cria as tabelas no banco de dados
dotnet run seeddata         ////Popula a base dados com as informações previamente disponibilizadas

O último comando também inicia o servido web onde a aplicação ficará disponível no link abaixo.
http://localhost:8080/swagger/index.html

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
Dados para acesso ao banco de dados

server: localhost
porta: 1433
user: sa
senha: passwd-db-estapar

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
Para executar a rotina de testes unitários será necessário pelo terminal [root]/estapar_web_api.Testes
e executar o comando:

dotnet test

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------

SOBRE OS MÉTODOS:

** seed-passagem-db
Método apenas para facilitar a inclusão de dados para testes.
Recebe um array com Passagens conforme o modelo passado:
[
    {
        "Garagem":"EVO01",
        "CarroPlaca":"ABC-0O12",
        "CarroMarca":"Honda",
        "CarroModelo":"FIT",
        "DataHoraEntrada":"04/09/2023 13:30",
        "DataHoraSaida":"04/09/2023 15:15", <---- Pode ser nulo para indicar que o carro está estacionado
        "FormaPagamento":"PIX",             <---- Pode ser nulo para o caso onde carro está estacionado
        "PrecoTotal": 0                     <---- Pode ser 0, será calculado no insert no banco
    }
]

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
** registrar-entrada
Método registra a entrada de um carro na garagem. Se houver uma entrada sem saída, retorna alerta para
usuário informando inconsistência e última entrada válida.
Recebe um JSON no formato
{
  "carroPlaca": "string",
  "carroMarca": "string",
  "carroModelo": "string"
}

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
** registro-saida
Finaliza a passagem do carro pela garagem
Recebe a placa de um carro e procura pela última entrada. Sendo a saida nula retorna calcula o valor
da estadia e retorna os dados para o usuario. Caso não tenha uma saída nula (entrada não registrada 
ou duplicidade de saída) retorna um aviso de alerta e a ultima passagem válida.
Recebe um JSON no formato
{
  "carroPlaca": "string",
  "formaPagamento": "string"
}

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
** carros-no-periodo
Dado um período, retorna lista de carros que deram entrada no início do período e saíram até o fim 
deste período.

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
** carros-estacionados
Retorna lista de todos as passagens onde o campo DataHoraSaida é igual a nulo.

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
** carros-que-passaram
Retorna lista de todos as passagens onde o campo DataHoraSaida é diferente de nulo.

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
** fechamento-periodo
Retorna uma lista dos métodos de pagamento e os totais pagos no período informado.
Para o caso de mensalistas existe uma condição especial explicada nas observações abaixo.

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
** tempo-medio
Separa mensalistas de outras formas de pagamento e retorna em horas a média de que os carros passaram
estacionados

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
** lista-todas-garagens / litsta-todos-pagamentos / lita-todas-passagens
Métodos apenas para facilitar a consulta dos registros no banco de dados.

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
OBSERVAÇÕES:


Metodo ExecutaFecahamentoDoPeriodo() 
Este método quando olha para os mensalistas não tem como trazer o valor correto.
O que consigo olhar é apenas quantos carros com placas diferentes se enquadram no período. Pode existir
o caso do mensalista não ter estacionado nenhuma vez no período escolhido, portanto minha escolha foi por
considerar que todos os mensalistas estacionaram ao menos 1 vez e nunca cancelaram sua mensalidade

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------
Foi solicitado que o preço calculado das estadias fosse arredondado para baixo sempre que possível.
Implementei o método CalculaPrecoEstadia inicialmente para ter esse comportamento com Math.Truncate().
Multiplicando o valor por 100, truncando e depois dividindo novamente por 100, mas para o valor informado 
de 40.8 a aplicação considera o valor como uma dízima periódica. A unica socução que encontrei sem ter
que tratar tudo como números inteiros e depois tratar apenas na hora de exibir para o usuário foi usar
Math.Round();

////Erro disparado pelo teste para o valor informado de 40.8
Expected: 40,799999999999997
Actual:   40,789999999999999
////

----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------

Quanto aos testes unitários, consegui apenas realizar testes em funções estáticas que não necessitavam
acessar o Banco de Dados. Para os outros casos não consegui popular um BD "mockado" para realizar as
consultas necessárias.
----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------