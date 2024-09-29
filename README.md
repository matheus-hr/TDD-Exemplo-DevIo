# TDD exemplo com xUnit

![project_language](https://img.shields.io/badge/language-C%23-green)
![server_backend](https://img.shields.io/badge/backend%2Fserver-.NET%20-blue)
![test](https://img.shields.io/badge/project-Unit%20Tests-blueviolet)

Este é um projeto com o intuito de aprender a abordagem do TDD em que consiste na criação de um codigo juntamente ao teste relacionado a ele. A aplicação consiste em um modulo de vendas um e-commerce, no qual temos as funcionalidades de produto, carrinho, voucher dentre outros. <br/><br/>
Esse codigo faz parte do curso de Dominando os testes de software do Eduardo Pires - Desenvolvedor.io

## 🚀 Tecnologias Utilizadas

* **xUnit:** Framework de teste de unidade para .NET.
* **Moq:** Biblioteca para criação de objetos simulados (mocks) durante os testes.

## 💻 Conceitos Utilizados
*  **TDD - Test Driven Development**  Uma metodologia onde os testes são escritos antes do código original. Ambos evoluem juntos, garantindo uma alta coerência entre o código e os testes, além de promover maior qualidade e confiabilidade no desenvolvimento.

*  **Arrange, Act, Assert:** Uma abordagem para estruturar testes de unidade de forma clara e organizada
    *  **Arrange:** Nesta etapa, você deve preparar o ambiente para o teste, como instanciar e preparar objetos.
      
    *  **Act:** Nesta etapa, você executa a ação ou o comportamento que deseja testar. Como acionar um método.
      
    *  **Assert:** Nesta etapa, você verifica o resultado esperado do teste. Você compara o resultado obtido com o resultado esperado usando asserções (assertions).

*   **Fact:** Identifica um método de teste como um fato independente.
    
*  **Trait:** Adiciona metadados aos testes para facilitar a organização e categorização.

*  **Mock:** Criação objetos simulados para testar dependências em isolamento.
  
