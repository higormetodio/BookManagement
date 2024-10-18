# BookManagement
Esse é um projeto proposto pela Mentoria do [Luiz Felipe](https://www.linkedin.com/in/luisdeol/) na plataforma [Next Wave Education](https://nextwave.education/).
O projeto consiste em um sistema de gerenciamento de livros, que visa atender algumas necessidades do que seria uma biblioteca.


## Requisitos Iniciais :page_facing_up:
:white_check_mark: Cadastro de livro

:white_check_mark: Consulta de todos os livros

:white_check_mark: Consulta de um livro específico 

:white_check_mark: Remoção de livro

:white_check_mark: Cadastro de usuário

:white_check_mark: Cadastro de empréstimo

:white_check_mark: Devolução do livro

:black_square_button: Emitir mensagem com dias de atraso ou se estiver em dia

## Novas Features :new:
:white_check_mark: Controle de estoque de livros (para atender o relacionamento N:N entre livros / empréstimos / usuários), sabendo quantos livros estão disponíveis e quantos estão emprestados.

:white_check_mark: Consulta de um livro e seus empréstimos

:white_check_mark: Método HTTP Patch - Atualização do status Ativo do livro e usuário para os casos em que deseja-se reativá-los novamente, já que a deleção não faz a remoção lógica do banco de dados e somente atualiza para que o livro ou o usuário esteja inativo.

:white_check_mark: Consulta de um usuário e seus empréstimos

:white_check_mark: Validação da API utilizando Fluent Validation

:white_check_mark: Autenticação e Autorização utilizando JWT

:black_square_button: Documentação da API no Swagger

:white_check_mark: Testes Unitátios com XUnit


## Temas Abordados :pencil:
:arrow_forward: Arquitetura Limpa

:arrow_forward: Padrão CQRS

:arrow_forward: Padrão Repository

:arrow_forward: Padrão Result

:arrow_forward: Programação Assíncrona

:arrow_forward: Testes Unitários

## Tecnologias
:arrow_forward: [C#](https://learn.microsoft.com/en-us/dotnet/csharp/)

:arrow_forward: [ASP.NET Core 8](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0)

:arrow_forward: [Entity Framework Core 8.0](https://learn.microsoft.com/en-us/ef/)

:arrow_forward: [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

:arrow_forward: [Swagger](https://swagger.io/)

:arrow_forward: [XUnit](https://xunit.net/)


## Docker Desktop com SQL Server
Obtendo imagem oficial SQL Server para um container Docker 

```powershell
docker pull mcr.microsoft.com/mssql/server:2022-latest
```

Rodando o SQL Server
```powershell
docker run --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

## Pacotes instalados
:arrow_forward: Microsoft.EntityFrameworkCore.SqlServer
:arrow_forward: MediatR
:arrow_forward: Microsoft.AspNetCore.JsonPatch
:arrow_forward: Microsoft.AspNetCore.Mvc.NewtonsoftJson
:arrow_forward: Swashbuckle.AspNetCore
