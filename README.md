# API ToDo

## 🚀 Descrição

A **API ToDo** é uma aplicação backend que permite o cadastro e gerenciamento de tarefas por usuários autenticados.  
O projeto foi construído com foco em **boas práticas de desenvolvimento** e separação de responsabilidades.

Inclui:

- Autenticação e autorização via **OAuth**
- Envio de token via **Cookie**
- Registro de exceções com **logger customizado**
- Integração com **SQL Server**
- Documentação automática com **Swagger**
- Uso de **DTOs** e mapeamento com **Mapster**
- Arquitetura em camadas com **Repositories**
- **Migrations** para controle de banco de dados

---

## 🛠️ Tecnologias utilizadas

- C#
- .NET Framework
- ASP.NET Web API 2
- ASP.NET Identity
- OWIN
- OAuth 2.0
- Entity Framework 6 (EF6)
- SQL Server
- Swagger (Swashbuckle)
- Mapster (mapeamento entre entidades e DTOs)
- Data Annotations (validações)
- Migrations (Code First)

---

## 🧱 Arquitetura e padrões

O projeto segue boas práticas de organização e separação de responsabilidades:

### 🔹 Repository Pattern
Responsável pelo acesso a dados e isolamento da camada de persistência.

### 🔹 DTOs (Data Transfer Objects)
Evita exposição direta das entidades do banco e melhora a segurança e performance.

### 🔹 Mapster
Utilizado para mapear entidades ↔ DTOs de forma simples e performática.

### 🔹 Identity + OAuth
- Autenticação via ASP.NET Identity  
- Geração de token OAuth  
- Token enviado via Cookie  
- Controle de acesso por usuário autenticado  

### 🔹 Logger customizado
Sistema de log criado para registrar exceções e facilitar diagnóstico de erros.

### 🔹 Migrations
Controle de versão do banco de dados com Entity Framework Migrations.

---

## 🔐 Autenticação + Autorização

A API utiliza:

- OAuth 2.0  
- ASP.NET Identity  
- Token armazenado em Cookie  
- Controle de acesso por `[Authorize]`  

**Fluxo básico:**

1. Usuário faz login  
2. API gera token OAuth  
3. Token é enviado via cookie  
4. Requisições autenticadas usam o cookie automaticamente  

---

## 📦 Funcionalidades

- Cadastro de usuário  
- Login com geração de token  
- CRUD de tarefas (ToDo)   
- Validações com DataAnnotations  
- Logs de exceções  
- Documentação Swagger  

---

## 🎯 Objetivo do projeto

Este projeto foi criado para demonstrar:

- Estrutura de API em .NET Framework
- Autenticação e autorização com Identity + OAuth
- Padrões como Repository e DTO
- Boas práticas de organização
- Integração com SQL Server
- Tratamento e log de exceções

