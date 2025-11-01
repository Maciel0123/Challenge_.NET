# 📦 Challenge .NET – Mottu API

Este projeto consiste em uma **API RESTful** desenvolvida com **ASP.NET Core**, voltada para o **gerenciamento de motos, zonas e pátios** da empresa Mottu. A API foi construída com boas práticas de arquitetura, integração com banco de dados Oracle via Entity Framework Core, e documentação completa via Swagger.

---

## 🎯 Entidades do Domínio

As 3 entidades principais foram escolhidas por refletirem diretamente o fluxo logístico da empresa Mottu:

- **Moto**: representa o ativo principal da empresa. Cada moto possui modelo, placa e está alocada em uma Zona.
- **Zona**: representa uma subdivisão operacional de um Pátio, usada para organização física e controle de alocação das motos.
- **Pátio**: estrutura física onde as motos são armazenadas. Cada pátio contém uma ou mais Zonas.

---

## 🚀 Funcionalidades

- CRUD completo para Motos, Zonas e Pátios
- Associação de Motos a Zonas e de Zonas a Pátios
- Consultas com filtros (`QueryParams`, `PathParams`)
- Paginação (`?page=1&pageSize=10`)
- HATEOAS nas respostas `GET /{id}`
- Retornos HTTP adequados (`200 OK`, `201 Created`, `204 No Content`, etc.)
- Documentação via Swagger

---

## 🧱 Estrutura do Projeto

Challenge_.NET<br>
├── MottuApi # Camada de apresentação (Controllers + Program.cs)<br>
├── Mottu.http # Requisições de teste para Postman ou REST Client<br>
├── MottuBusiness # Lógica de negócio e interfaces dos serviços<br>
├── MottuData # Acesso a dados e configuração do EF Core + Oracle<br>
├── MottuModel # Modelos de dados (entidades)

---

## 🛠️ Como Executar Localmente

1. Clone o repositório:
```
git clone https://github.com/Maciel0123/Challenge_.NET.git
```
3. Abra a solução no Visual Studio ou VSCode.

4. Atualize a connection string para o Oracle no appsettings.json ou na ApplicationDbContextFactory.cs.

5. Gere o banco de dados (caso necessário):
```
dotnet ef database update --project MottuData
```
7. Rode o projeto WebAPI:
```
dotnet run --project MottuApi
```
9. Acesse o Swagger:
```    
https://localhost:{porta}/swagger
```

##🧪 Testes de Requisições

Você pode testar os endpoints usando:

Swagger UI (/swagger)

REST Client (Mottu.http)

Postman

Extra:

## ⚙️ Tecnologias Utilizadas

- ASP.NET Core 9.0
- Entity Framework Core 9
- Oracle Database
- Swagger / OpenAPI
- C# moderno (`required`, `Guid`, `nullable`, etc.)
  
## 📌 Endpoints Principais

### 🔸 Moto

| Verbo  | Rota                       | Descrição                              |
|--------|----------------------------|----------------------------------------|
| POST   | `/api/mottu`               | Criar uma nova Moto                   |
| GET    | `/api/mottu`               | Listar todas as Motos                 |
| GET    | `/api/mottu/{id}`          | Buscar Moto por ID                    |
| GET    | `/api/mottu/paginado`      | Listar Motos com paginação            |
| PUT    | `/api/mottu`               | Atualizar Moto                        |
| DELETE | `/api/mottu/{id}`          | Deletar Moto por ID                   |


### 🔸 Zona

| Verbo  | Rota                       | Descrição                             |
|--------|----------------------------|---------------------------------------|
| POST   | `/api/zona`                | Criar uma nova Zona                  |
| GET    | `/api/zona`                | Listar todas as Zonas                |
| GET    | `/api/zona?patioId={guid}` | Listar zonas de um pátio específico  |
| GET    | `/api/zona/{id}`           | Buscar Zona por ID                   |
| GET    | `/api/zona/paginado`       | Listar Zonas com paginação           |
| PUT    | `/api/zona`                | Atualizar Zona                       |
| DELETE | `/api/zona/{id}`           | Deletar Zona por ID                  |

### 🔸 Pátio

| Verbo  | Rota                       | Descrição                           |
|--------|----------------------------|-------------------------------------|
| POST   | `/api/patio`               | Criar um novo Pátio                |
| GET    | `/api/patio`               | Listar todos os Pátios             |
| GET    | `/api/patio/{id}`          | Buscar Pátio por ID                |
| GET    | `/api/patio/paginado`      | Listar Pátios com paginação        |
| PUT    | `/api/patio`               | Atualizar Pátio                    |
| DELETE | `/api/patio/{id}`          | Deletar Pátio por ID               |

📎 Observações

- O projeto utiliza **migrations** para versionamento do banco de dados.
- Todas as respostas **GET /{id}** retornam links de ação no padrão **HATEOAS**.
- O código segue boas práticas de **arquitetura em camadas**.
- A **paginação** foi implementada nas rotas de listagem de **Pátios**, **Zonas** e **Motos** com os parâmetros `page` e `pageSize` para controle de quantidade de resultados por página.

Integrantes:

Gabriela Moguinho Gonçalves - RM556143<br>
Henrique Maciel - RM556480<br>
Mariana Christina Rodrigues Fernandes - RM554773
