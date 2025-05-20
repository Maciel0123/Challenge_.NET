# 📦 Challenge .NET – Mottu API

Este projeto consiste em uma **API RESTful** desenvolvida com **ASP.NET Core**, voltada para o **gerenciamento de motos, zonas e pátios** da empresa Mottu. A API foi construída com boas práticas de arquitetura, integração com banco de dados Oracle via Entity Framework Core, e documentação completa via Swagger.

---

## 🚀 Funcionalidades

- Cadastro de Motos com associação à Zona
- Cadastro de Zonas vinculadas a um Pátio
- Cadastro de Pátios
- Relacionamentos entre entidades via EF Core
- Consultas com filtros (`QueryParams` e `PathParams`)
- Retornos HTTP adequados (`200 OK`, `201 Created`, `204 No Content`, etc.)

---

## 🧱 Estrutura do Projeto

 Challenge_.NET
├── MottuApi # Camada de apresentação (Controllers + Program.cs)
      ├── Mottu.http # Requisições de teste para Postman ou REST Client
├── MottuBusiness # Lógica de negócio e interfaces dos serviços
├── MottuData # Acesso a dados e configuração do EF Core + Oracle
├── MottuModel # Modelos de dados (entidades)

## 🛠️ Como Executar Localmente

1. Clone o repositório:
git clone https://github.com/Maciel0123/Challenge_.NET.git

2. Abra a solução no Visual Studio ou VSCode.

3. Atualize a connection string para o Oracle no appsettings.json ou na ApplicationDbContextFactory.cs.

4. Gere o banco de dados (caso necessário):
Gere o banco de dados (caso necessário):

5. Rode o projeto WebAPI:
dotnet run --project MottuApi

6. Acesse o Swagger:
https://localhost:{porta}/swagger

📎 Observações
O projeto utiliza migrations para versionamento de banco de dados.

Para executar as requisições manualmente, utilize o arquivo Mottu.http.

Autor:

Henrique Maciel
