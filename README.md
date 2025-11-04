# 📦 Challenge .NET – Mottu API

Este projeto consiste em uma **API RESTful** desenvolvida com **ASP.NET Core 9**, voltada para o **gerenciamento de motos, zonas e pátios** da empresa Mottu. A API segue boas práticas REST, segurança com JWT, versionamento de API, Health Checks, uso de ML.NET para previsão e documentação detalhada via Swagger.

---

## 🎯 Entidades do Domínio

- **Moto** → ativo principal da operação
- **Zona** → subdivisão do pátio onde as motos são alocadas
- **Pátio** → espaço físico que comporta várias zonas

---

## 🚀 Funcionalidades Implementadas

| Funcionalidade | Status |
|----------------|--------|
| CRUD completo (Moto, Zona, Pátio) | ✅ |
| Versionamento de API (`/api/v1/...`) | ✅ |
| Documentação via Swagger e OpenAPI | ✅ |
| Segurança JWT com Auth Token | ✅ |
| Endpoint de Health Check (`/health`) | ✅ |
| ML.NET integrado (`/api/v1/moto/predict`) | ✅ |
| Testes unitários com xUnit | ✅ |
| Testes de integração com WebApplicationFactory | ✅ |

---

## 🧠 ML.NET dentro da API

A API possui um modelo simples demonstrativo de Machine Learning que retorna uma **previsão de risco da moto** com base no modelo informado.

Exemplo de uso:

POST /api/v1/moto/predict

```
{
  "modelo": "Yamaha Fazer"
}
```

Retorno:

{
  "modelo": "Yamaha Fazer",
  "risco": 0.75,
  "categoria": "alto"
}

---

## 🧱 Estrutura do Projeto

Challenge_.NET<br>
├── MottuApi # Camada de apresentação (Controllers + Program.cs)<br>
├── Mottu.http # Requisições de teste para Postman ou REST Client<br>
├── MottuBusiness # Lógica de negócio e interfaces dos serviços<br>
├── MottuData # Acesso a dados e configuração do EF Core + Oracle<br>
├── MottuModel # Modelos de dados (entidades)<br>
├── MottuTestes # Camada de testes automatizados

---

## 🔐 Autenticação JWT

Gere o token:

POST /api/v1/auth/token
```
{
  "username": "gabi",
  "password": "123"
}
```

Copie o access_token recebido

Clique em Authorize no Swagger e insira o token

Após isso todos os endpoints protegidos estarão liberados para uso.

## 🛠️ Como Executar Localmente

1. Clone o repositório:
```
git clone https://github.com/Maciel0123/Challenge_.NET.git
```
2. Abra a solução no Visual Studio ou VSCode.

3. Atualize a connection string para o Oracle no appsettings.json ou na ApplicationDbContextFactory.cs.

4. Gere o banco de dados (caso necessário):
```
dotnet ef database update --project MottuData
```
5. Rode o projeto WebAPI:
```
dotnet run --project MottuApi
```
6. Acesse o Swagger:
```    
https://localhost:7039/swagger
```
7. Health Check (não requer token):
```
https://localhost:7039/health
```

## 🧪 Testes de Requisições

Você pode testar os endpoints usando:

- Swagger UI (https://localhost:7039/swagger)

- REST Client (Mottu.http)

- Postman: importar manualmente as rotas

## 🧪 Testes Automatizados

Projeto de testes: MottuTestes

Para executar:

```
dotnet test
```

Os testes incluem:

- Validação da lógica de domínio

- Validação do ML.NET

- Teste de integração do HealthCheck

Extra:

## ⚙️ Tecnologias Utilizadas

- ASP.NET Core 9.0
- Entity Framework Core 9
- Oracle Database
- ML.NET
- JWT Authentication
- Swagger / OpenAPI
  
## 📌 Endpoints Principais

### 🔸 Auth

| Verbo | Rota                 | Descrição                                              |
| ----- | -------------------- | ------------------------------------------------------ |
| POST  | `/api/v1/auth/token` | Gera um token JWT para acesso aos endpoints protegidos |

### 🔸 Moto

| Verbo  | Rota                    | Descrição                           |
| ------ | ----------------------- | ----------------------------------- |
| GET    | `/api/v1/Moto`          | Lista todas as motos                |
| POST   | `/api/v1/Moto`          | Cria uma nova moto                  |
| PUT    | `/api/v1/Moto`          | Atualiza os dados de uma moto       |
| GET    | `/api/v1/Moto/paginado` | Lista motos com paginação           |
| GET    | `/api/v1/Moto/{id}`     | Busca moto por ID                   |
| DELETE | `/api/v1/Moto/{id}`     | Remove uma moto pelo ID             |
| POST   | `/api/v1/Moto/predict`  | Prediz risco de manutenção (ML.NET) |



### 🔸 Zona

| Verbo  | Rota                           | Descrição                                     |
| ------ | ------------------------------ | --------------------------------------------- |
| GET    | `/api/v1/Zona`                 | Lista todas as zonas com seus relacionamentos |
| POST   | `/api/v1/Zona`                 | Cria uma nova zona                            |
| PUT    | `/api/v1/Zona`                 | Atualiza uma zona existente                   |
| GET    | `/api/v1/Zona/paginado`        | Lista zonas com paginação                     |
| GET    | `/api/v1/Zona/patio/{patioId}` | Lista zonas de um determinado pátio           |
| GET    | `/api/v1/Zona/{id}`            | Busca zona por ID                             |
| DELETE | `/api/v1/Zona/{id}`            | Remove uma zona pelo ID                       |


### 🔸 Pátio

| Verbo  | Rota                     | Descrição                                     |
| ------ | ------------------------ | --------------------------------------------- |
| GET    | `/api/v1/Patio`          | Lista todos os pátios cadastrados (com zonas) |
| POST   | `/api/v1/Patio`          | Cria um novo pátio                            |
| PUT    | `/api/v1/Patio`          | Atualiza pátio existente                      |
| GET    | `/api/v1/Patio/paginado` | Lista pátios com paginação                    |
| GET    | `/api/v1/Patio/{id}`     | Busca pátio por ID                            |
| DELETE | `/api/v1/Patio/{id}`     | Remove um pátio pelo ID                       |

## Integrantes:

Gabriela Moguinho Gonçalves - RM556143<br>
Henrique Maciel - RM556480<br>
Mariana Christina Rodrigues Fernandes - RM554773
