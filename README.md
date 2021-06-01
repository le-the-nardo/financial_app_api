# financial_app_api
>API Restful, desenvolvida com C# .NET Core 3.1, que serve de comunicação para a aplicação mobile [App Financial](https://github.com/le-the-nardo/financial_app). API desenvolvida seguindo padrões de qualidade de código back-end.

## Tópicos abordados

1. [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
2. [Testes Unitários](https://docs.microsoft.com/pt-br/visualstudio/test/walkthrough-creating-and-running-unit-tests-for-managed-code?view=vs-2019)
3. [Criptografia em dados sensíveis](https://docs.microsoft.com/pt-br/dotnet/standard/security/walkthrough-creating-a-cryptographic-application)
4. [Princípios de SOLID](https://en.wikipedia.org/wiki/SOLID)
5. [Clean Code](https://www.amazon.com.br/C%C3%B3digo-limpo-Robert-C-Martin/dp/8576082675/ref=sr_1_1?__mk_pt_BR=%C3%85M%C3%85%C5%BD%C3%95%C3%91&dchild=1&keywords=c%C3%B3digo+limpo&qid=1622500775&s=books&sr=1-1) 
6. [AWS - Serviços Cloud](https://aws.amazon.com/)


## Requisições suportadas pela API


POST api/security/login

```sh
Faz a verificação do usuário no banco, passando email e password. Compara o hash do password com o banco. Se os dados estiverem corretos, a api retorna um JWT de autenticação
```

POST api/security/CreateUser

```sh
Adiciona um usuário no banco com nome, password criptografado, cpf e email.
```

[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/1fabf8dd2cbe4149edc9)


## Autor 👦🏻

Feito por mim com muito ☕ e ❤.
