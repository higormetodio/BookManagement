# BookManagement
Esse é um projeto proposto pela Mentoria do [Luiz Felipe](https://www.linkedin.com/in/luisdeol/) na plataforma [Next Wave Education](https://nextwave.education/).
O projeto consiste em um sistema de gerenciamento de livros, que visa atender algumas necessidades do que seria uma biblioteca.


## Requisitos Iniciais :page_facing_up:
:black_square_button: Cadastro de livro

:black_square_button: Consulta de todos os livros

:black_square_button: Consulta de um livro específico 

:black_square_button: Remoção de livro

:black_square_button: Cadastro de usuário

:black_square_button: Cadastro de empréstimo

:black_square_button: Devolução do livro


## Temas Abordados :pencil:
:arrow_forward: Arquitetura Limpa

:arrow_forward: Padrão Repository

:arrow_forward: Programação Assíncrona


## Tecnologias
:arrow_forward: [C#](https://learn.microsoft.com/en-us/dotnet/csharp/)

:arrow_forward: [ASP.NET Core 8](https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0)

:arrow_forward: [Entity Framework Core 8.0](https://learn.microsoft.com/en-us/ef/)

:arrow_forward: [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

:arrow_forward: [Swagger](https://swagger.io/)


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
