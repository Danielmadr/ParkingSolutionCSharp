# ParkingSolutionCSharp - Desafio de Projeto .NET Fundamentos

![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white) ![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)

Este repositório contém uma solução aprimorada para o desafio de projeto "Criando um sistema de estacionamento com C#" proposto no bootcamp .NET da Digital Innovation One (DIO). O projeto original pode ser encontrado em [digitalinnovationone/trilha-net-fundamentos-desafio](https://github.com/digitalinnovationone/trilha-net-fundamentos-desafio).

Esta versão implementa diversas melhorias e refatorações em relação ao código base original, visando maior organização, flexibilidade e robustez, além de introduzir novas funcionalidades.

## Visão Geral do Projeto

O `ParkingSolutionCSharp` simula um sistema de gerenciamento de estacionamento via console. Ele permite registrar a entrada de veículos (carros e motos), calcular o custo da estadia com base no tempo de permanência e nas tarifas configuradas, listar os veículos estacionados e registrar a saída.

## Principais Melhorias e Funcionalidades Implementadas

Esta solução se diferencia do projeto original através das seguintes modificações:

### 1. Reestruturação e Arquitetura em Camadas

O projeto foi reestruturado para adotar uma separação de responsabilidades mais clara. Foi introduzida uma camada de `Application` contendo subpastas para `Models` (Entidades, Enums, Exceções), `Services` (lógica de negócio) e `Validators` (validações de entrada). Essa organização facilita a manutenção, escalabilidade e testabilidade do código.

### 2. Modelo de Veículo Detalhado

Em vez de apenas armazenar placas como strings, foi criada a entidade `Vehicle` (`Application/Models/Entities/Vehicle.cs`). Este modelo agora inclui:

*   `Plate`: A placa do veículo (string).
*   `Type`: O tipo do veículo (enum `VehicleType`: `Car` ou `Motorcycle`), permitindo tratamento diferenciado.
*   `EntryDate`: A data e hora exatas (`DateTime`) em que o veículo entrou no estacionamento, registrada automaticamente.

Embora a entidade `Vehicle` possua uma identidade implícita na coleção, a lógica de remoção implementada ainda utiliza a `Plate` como chave principal. A estrutura com `DateTime` e `Type` oferece uma base mais rica para futuras expansões, como relatórios ou estatísticas.

### 3. Configuração de Preços via Variáveis de Ambiente

As tarifas do estacionamento (preço inicial e preço por hora) não são mais fixas ou solicitadas no início da execução. Elas são carregadas a partir de variáveis de ambiente no início da aplicação usando a biblioteca `DotNetEnv`. Isso permite:

*   Configurar os preços externamente (através de um arquivo `.env`), sem necessidade de alterar e recompilar o código.
*   Definir preços distintos para carros e motos (`INITIAL_PRICE_CAR`, `INITIAL_PRICE_MOTORCYCLE`, `PRICE_PER_HOUR_CAR`, `PRICE_PER_HOUR_MOTORCYCLE`).

**Exemplo de arquivo `.env`:**

```dotenv
INITIAL_PRICE_CAR=5.00
INITIAL_PRICE_MOTORCYCLE=3.00
PRICE_PER_HOUR_CAR=2.50
PRICE_PER_HOUR_MOTORCYCLE=1.50
```

### 4. Cálculo de Permanência Baseado em `DateTime`

O cálculo do custo foi aprimorado para usar a diferença entre a hora de entrada (registrada automaticamente) e a hora de saída (informada pelo usuário no momento da remoção). O sistema calcula o `TotalHours` de permanência (incluindo frações de hora) e aplica as tarifas correspondentes (inicial + horas adicionais) com base no tipo de veículo. Isso resulta em um cálculo mais preciso do que a versão original, que dependia da inserção manual do número total de horas.

*Observação:* A hora de saída ainda é inserida manualmente pelo usuário, mas a estrutura permite uma futura modificação para usar `DateTime.Now` para registrar a saída automaticamente.

### 5. Validação de Formato de Placa

Foi adicionado um `PlateValidator` (`Application/Validators/PlateValidator.cs`) para verificar se a placa inserida pelo usuário possui um formato considerado válido antes de permitir o registro do veículo. Isso aumenta a consistência dos dados.

### 6. Lógica de Negócio Centralizada no `ParkingService`

Toda a lógica de adicionar, remover, listar veículos e calcular preços foi encapsulada no `ParkingService` (`Application/Services/ParkingService.cs`). O `Program.cs` agora atua como a camada de apresentação, interagindo com o serviço, o que melhora a coesão e o desacoplamento.

### 7. Interface de Console Aprimorada

O menu e as mensagens no console foram revisados para maior clareza e para refletir as novas funcionalidades, como a diferenciação por tipo de veículo e a consulta de preços.

## Como Executar

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/Danielmadr/ParkingSolutionCSharp.git
    cd ParkingSolutionCSharp
    ```
2.  **Crie um arquivo `.env` na raiz do projeto** (ao lado do arquivo `.sln`) com as variáveis de ambiente para os preços:
    ```dotenv
    INITIAL_PRICE_CAR=5.00
    INITIAL_PRICE_MOTORCYCLE=3.00
    PRICE_PER_HOUR_CAR=2.50
    PRICE_PER_HOUR_MOTORCYCLE=1.50
    ```
    *Ajuste os valores conforme necessário.*
3.  **Navegue até o diretório do projeto:**
    ```bash
    cd DesafioFundamentos
    ```
4.  **Execute a aplicação:**
    ```bash
    dotnet run
    ```

Siga as instruções apresentadas no console para interagir com o sistema de estacionamento.

## Próximos Passos (Sugestões)

*   Implementar a persistência de dados (ex: salvar/carregar a lista de veículos em um arquivo ou banco de dados).
*   Automatizar a captura da hora de saída usando `DateTime.Now`.
*   Adicionar testes unitários para os serviços e validadores.
*   Implementar a funcionalidade de busca de veículo por ID (se um ID explícito for adicionado ao modelo `Vehicle`) para baixa via código de barras.
*   Criar relatórios (veículos atuais, faturamento diário, etc.).

---

*Este projeto foi desenvolvido como parte do Bootcamp .NET da DIO.*
