# LocalRental - DDD/CQRS

Aplicação de referência, construída utilizando DDD/CQRS, baseada na ideia de uma simples Locadora de Veículos.

## Build Status


## Requisitos
Para executar este projeto, é necessário o [.NET Core 3.0 SDK](https://dotnet.microsoft.com/download/dotnet-core)

## Como inicializar o projeto através do CLI
Para inicializar este projeto utilizando o **dotnet cli**, execute os seguintes comandos no prompt/terminal

    dotnet build
    dotnet run
Para executar os testes unitários, utilize o comando:

    dotnet test

# Overview
Esta aplicação de referência foi construída para exemplificar a utilização da ideologia do DDD, juntamente com o padrão CQRS (Command Query Responsibility Segregation).
Esta arquitetura propôe a utilização dos conceitos primários de uma aplicação orientada ao domínio (Aggregates, Entities, Value Objects, Domain Services e Domain Events), juntamente com outros padrões já conhecidos como Repository e Singleton.
Além disso, o padrão CQRS foi implementado visando a separação do workflow de escrita e de leitura. A fim de facilitar a implementação e diminuir a complexidade, a camada de persistência foi compartilhada entre os dois lados (escrita/leitura) através de um banco de dados NoSQL local.

## LocalRental - O Projeto

LocalRental é um projeto baseado em uma ideia simples de negócio, onde temos uma pequena loja de aluguel de carros. Para se alugar um carro, basta o cliente se dirigir até a loja, solicitar o aluguel de um veículo, retirar o veículo do pátio e devolvê-lo na data estipulada. Após a devolução, a loja gera uma fatura do período utilizado e o cliente realiza o pagamento da mesma, finalizando assim o fluxo de aluguel.

# Domínio

## Raízes de agregação

Olhando para o fluxo de negócio, temos inicialmente as seguintes raízes agregações (aggregate-root):

- **Cliente** - Contém os dados básicos do cliente
- **Aluguel** - Contém as informações do aluguel que será feito
- **Veículo** - Contém as características do veículo
- **Fatura** - Contém os dados de cobrança para pagamento do Aluguel

## Event Storming

Após identificar as raízes de agregação, é necessário definir o workflow de como o negócio funciona. Uma técnica muito útil utilizada neste cenário é o [Event Storming](https://en.wikipedia.org/wiki/Event_storming), um pequeno workshop onde rapidamente se define o que acontece no domínio na linha do tempo.
Após a realização do Event Storming, temos o seguinte resultado:
![enter image description here](https://raw.githubusercontent.com/gufigueiredo/ddd-cqrs/master/docs/local-rental-event-storming.png)

## Domain Model
![enter image description here](https://raw.githubusercontent.com/gufigueiredo/ddd-cqrs/master/docs/domain-model.png)
# Arquitetura
Conforme pontuado, o CQRS foi utilizado na segregação das camadas de leitura e escrita, resultando em workflows bem definidos, encapsulados e isolados de comandos (write side) e queries (read side). A imagem a seguir exemplifica o modelo de implementação do CQRS:

![enter image description here](https://www.akveo.com/blog/content/images/2018/07/CQRS_Schema_pic1.png)

## Domain Events
A imagem abaixo demonstra o workflow de eventos de domínio (domain events). Estes eventos são utilizados na comunicação entre bounded contexts e agregações e é feita através do [mediator pattern](https://en.wikipedia.org/wiki/Mediator_pattern).
![enter image description here](https://docs.microsoft.com/pt-br/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/media/image17.png)
## Estrutura do Projeto

    LocalRental.API
    --Application
    -----CommandHandlers
    -----Commands
    -----EventHandlers
    -----Queries
    --Controllers
    LocalRental.Domain
    --Events
    --Model
    --Services
    LocalRental.Infrastructure
    --DataAccess
    --DataModel
    --Repositories
    LocalRental.UnitTests
    
### LocalRental.API
Camada de controllers e orquestração (application). Dentro da application, temos:
- **Commands** - modelo de entrada (write side)
- **CommandHandlers** - handlers de commands
- **EventHandlers** - handlers de Domain Events
- **Queries** - serviços de leitura (read side)

### LocalRental.Domain
Camada que contém o **modelo de domínio** - aggregates, value objects, domain events, domain services e interfaces de repositórios. É o **core** do negócio e não pode ter dependência de nenhuma outra camada. 

### LocalRental.Infrastructure
Contém toda a implementação de **acesso à dados**, **repositórios** e **serviços externos**. Na parte de dados, é responsável por traduzir todo o *domínio* para o *modelo de dados*

## Dependências
As seguintes dependências foram utilizadas neste projeto:

- **MediatR** - Biblioteca que abstrai toda a parte de mediação entre *commands/command handlers* e *domain events/event handlers*
- **LiteDB** - Banco NoSQL embarcado para persistência de dados
