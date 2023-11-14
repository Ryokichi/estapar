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
Para executar a rotina de testes unitários será necessário pelo terminal [root]/estapar_web_api.Testes
e executar o comando:

dotnet test
----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------


Observações:

Metodo ExecutaFecahamentoDoPeriodo() quando olha para os mensalistas não tem como trazer o valor correto.
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



dotnet new webapi -n estapar_web_api
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer

dotnet user-secrets init
dotnet user-secrets set ConnectionStrings:ConnectToEstaparDB "Data Source=localhost;Initial Catalog=EstaparDB;User Id=SA;Password=passwd-db-estapar;TrustServerCertificate=True"
dotnet ef dbcontext scaffold Name=ConnectionStrings:ConnectToEstaparDB Microsoft.EntityFrameworkCore.SqlServer

dotnet ef migrations add InitialCreate

dotnet ef database update
dotnet run seeddata
dotnet watch run