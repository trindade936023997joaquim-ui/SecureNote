# SecureNotes
Sistema de gestão de notas com foco em **segurança de aplicações web**, desenvolvido no contexto do **Departamento de Pesquisa**.

---

## Objetivo

O **SecureNotes** foi desenvolvido com uma abordagem **security-first**, visando a implementação prática de mecanismos de defesa contra vulnerabilidades modernas, com base nas recomendações do **OWASP Top 10**.

---

## Tecnologias Utilizadas

* **Backend:** C# / .NET 10
* **Frontend:** Razor Pages
* **Base de Dados:** MySQL (Docker)
* **Containerização:** Docker

---

## Mecanismos de Segurança

### Autenticação e Sessão

* Autenticação por email/password
* Confirmação obrigatória de email
* Timeout de sessão após **5 minutos de inatividade**
* Reautenticação após expiração

---

### Proteção de Credenciais

* Hash de passwords com **BCrypt**
* Evolução prevista: **Argon2id**

---

### Proteção de Dados Sensíveis

* Encriptação de dados armazenados
* Recomendações:

  * Uso de **IV aleatório**
  * Algoritmo seguro (ex: AES-256)

---

### Logs e Auditoria

* Registo de atividades do utilizador
* Melhorias futuras:

  * Logs imutáveis
  * Sistema de alertas

---

### Segurança Web

#### ✔ Proteção contra XSS

* Implementado
* Recomenda-se validação de **Content Security Policy (CSP)**

#### ✔ Proteção contra SQL Injection

* ORM / queries parametrizadas

#### ✔ HTTPS / TLS

* Comunicação segura ativa
* Recomenda-se uso de **TLS 1.2+ ou superior**

#### ✔ OWASP Top 10

Mitigações aplicadas contra:

* Injection
* Broken Authentication
* Sensitive Data Exposure
* XSS
* Security Misconfiguration

---

## Funcionalidades

* Registo de utilizador
* Confirmação de email
* Login seguro
* Criação de notas
* Edição de notas
* Eliminação de notas
* Visualização de notas
* Alteração de senha
* Gestão de sessão

---

## Como Executar

### 1. Clonar repositório

```bash
git clone https://github.com/teu-username/SecureNotes.git
cd SecureNotes
```

### 2. Instalar dependências

* Docker
* .NET 10 SDK

---

### 3. Iniciar base de dados

```bash
docker-compose up -d
```

---

### 4. Executar aplicação

```bash
dotnet run
```
---

## Considerações Finais

Este projeto representa uma **base sólida de aplicação segura**, com possibilidade de evolução para:

* Argon2id para hashing
* Hardening de headers (CSP, HSTS)
* Monitorização de segurança
* Melhorias em auditoria e logging

---

## 💻 Autor

**Trindade Joaquim**
Departamento de Pesquisa

---
