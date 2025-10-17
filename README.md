# 📦 RegisterAppBenner

Aplicação desktop desenvolvida em **C# com WPF (.NET Framework 4.7)** para o gerenciamento completo de **Pessoas, Produtos e Pedidos**.  
O sistema segue o padrão **MVVM (Model-View-ViewModel)**, com persistência de dados em **arquivos JSON**, interface responsiva e foco em **boas práticas de arquitetura e usabilidade**.

---

## 🧭 Índice
- [Funcionalidades](#-funcionalidades)
- [Arquitetura](#-arquitetura)
- [Tecnologias Utilizadas](#-tecnologias-utilizadas)
- [Requisitos de Execução](#-requisitos-de-execução)
- [Como Executar o Projeto](#-como-executar-o-projeto)
- [Estrutura de Pastas](#-estrutura-de-pastas)
- [Principais Telas](#-principais-telas)
- [Destaques Técnicos](#-destaques-técnicos)
- [Autor](#-autor)

---

## ⚙️ Funcionalidades

### 👤 **Módulo de Pessoas**
- Cadastro de pessoas com **Nome**, **CPF** e **Endereço**  
- Edição direta no **DataGrid** (edição inline)
- Filtros por **Nome** e **CPF**
- Exclusão de registros
- Inclusão de **pedidos vinculados à pessoa selecionada**
- Exibição dos pedidos da pessoa com filtro por **status**

### 🧾 **Módulo de Pedidos**
- Criação de pedidos associados a uma pessoa  
- Seleção de produto, quantidade e forma de pagamento  
- Cálculo automático de subtotal e total  
- Atualização de status: **Pago**, **Enviado**, **Recebido**  
- Armazenamento persistente em `orders.json`  

### 📦 **Módulo de Produtos**
- Cadastro de produtos com **Nome**, **Código** e **Preço**  
- Edição inline e exclusão  
- Filtros por:
  - Nome  
  - Código  
  - Faixa de valor (mínimo e máximo)
- Persistência em `product.json`  

---

## 🏗️ Arquitetura

O projeto foi desenvolvido com base no padrão **MVVM**, separando de forma clara as responsabilidades entre interface, lógica e dados.

RegisterAppBenner/
│

├── Models/ # Entidades principais (PersonModel, ProductModel, OrderModel)

├── ViewModels/ # Lógica de interface (PersonViewModel, ProductViewModel, OrderViewModel)

├── Views/ # Telas em XAML (WPF)

├── Services/ # Camada de dados (JsonDataService, ProductService, etc.)

├── Enums/ # Enumerações (ex: OrderStatusEnum)

├── Data/ # Armazenamento persistente de dados .json

├── Converters/ # Classes auxiliares para procedimentos em tempo de execução.

└── App.xaml # Configuração da aplicação WPF


A persistência utiliza **arquivos JSON locais**, manipulados pela classe genérica `JsonDataService<T>`, que realiza leitura, escrita e atualização de registros.

---

## 🧰 Tecnologias Utilizadas

| Categoria | Ferramenta |
|------------|------------|
| **.NET Framework 4.7** | Base do projeto (com compatibilidade retroativa à 4.6)
| Interface Gráfica | **WPF (Windows Presentation Foundation)** |
| Arquitetura | **MVVM (Model-View-ViewModel)** |
| Persistência | **JSON local (System.Text.Json / File I/O)** |
| IDE recomendada | **Visual Studio 2022 Community ou Enterprise** |
| Plataforma | **Windows (x64)** |

---

## 🧩 Requisitos de Execução

- **.NET Framework 4.6 (ou superior)**  
  👉 Baixe aqui: [Developer Pack .NET Framework 4.7](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net47)

- **Visual Studio 2022**  
  Durante a instalação, selecione o workload:  
  ☑ **Desenvolvimento de Desktop com .NET Framework**

- **Sistema Operacional:** Windows 10 ou Windows 11
- 
---
### ⚙️ Dependências

O projeto utiliza as seguintes bibliotecas:

- **System.Text.Json** — para serialização e persistência local em arquivos `.json`
  ```bash
  Install-Package System.Text.Json -Version 6.0.8

---

## 🚀 Como Executar o Projeto

1. **Clonar o repositório**
   ```bash
   git clone https://github.com/seu-usuario/RegisterAppBenner.git
   cd RegisterAppBenner
2. **Abrir o projeto no Visual Studio 2022**

3. **Selecionar a configuração de build**

* Debug para desenvolvimento

* Release para execução final

4. **Executar**

* Pressione F5 (Executar com depuração)

* Ou clique em Iniciar sem depuração (Ctrl + F5)

---

## 🗂️ Estrutura de Pastas

📦 RegisterAppBenner

 ┣ 📁 Models
 
 ┃ ┣ PersonModel.cs
 
 ┃ ┣ ProductModel.cs
 
 ┃ ┗ OrderModel.cs
 
 ┣ 📁 ViewModels
 
 ┃ ┣ PersonViewModel.cs
 
 ┃ ┣ ProductViewModel.cs
 
 ┃ ┗ OrderViewModel.cs
 
 ┣ 📁 Views
 
 ┃ ┣ PersonView.xaml / .cs
 
 ┃ ┣ ProductView.xaml / .cs
 
 ┃ ┗ OrderView.xaml / .cs
 
 ┣ 📁 Services
 
 ┃ ┣ JsonDataService.cs
 
 ┃ ┣ ProductService.cs
 
 ┃ ┣ PersonService.cs
 
 ┃ ┗ OrderService.cs

  ┣ 📁 Converters
 
 ┃ ┣ InverseBoolConverter.cs
 
 ┃ ┣ MultiplyConverter.cs

┣ 📁 Data (criada no momento da execução do código)
 
 ┣ 📄 persons.json
 
 ┣ 📄 product.json
 
 ┗ 📄 orders.json
 
 ┣ 📁 Enums
 
 ┃ ┗ OrderStatusEnum.cs
 
 ┣ 📄 App.xaml
 
 ┣ 📄 RegisterAppBenner.csproj

---

## **🖼️ Principais Telas**
### 🧍 Tela de Pessoas

Filtros por Nome e CPF

Edição inline e atualização imediata

Inclusão e exclusão de registros

Exibição dos pedidos da pessoa com filtro de status

### 🧾 Tela de Pedidos

Pedidos associados ao cliente selecionado

Botões de ação de status (Pago, Enviado, Recebido)

Registro automático em orders.json

### 📦 Tela de Produtos

Filtros por Nome, Código e faixa de preço

Inclusão, edição e exclusão diretas

Atualização em tempo real no grid

---

## Destaques Técnicos

Arquitetura MVVM limpa e separada por camadas

Data Binding bidirecional com INotifyPropertyChanged

Persistência local com JSON genérico reutilizável

Filtros inteligentes e reativos (atualizam conforme o usuário digita)

Edição direta nos grids com atualização automática

Tratamento de erros e mensagens amigáveis via MessageBox

Interface responsiva, organizada e intuitiva

Código enxuto, documentado e de fácil manutenção

---

## 👩‍💻 Autor

Larissa Ferreira

Desenvolvedora Full Stack | .NET | Vue.Js | TDD | Cypress | Cli/Git

📍 Rio de Janeiro, Brasil

---
