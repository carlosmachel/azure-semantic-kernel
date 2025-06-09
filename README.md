
# Azure Semantic Kernel
Demonstração do uso do Microsoft Semantic Kernel no C# com Azure Agent Services

## Sobre
Este projeto demonstra a implementação e uso do Semantic Kernel, uma biblioteca de código aberto da Microsoft que permite integrar facilmente modelos de IA em aplicações .NET, utilizando Azure Agent Services.

## Tecnologias
- .NET 9.0
- C# 13.0
- Microsoft Semantic Kernel
- Azure Agent Services
- ASP.NET Core

## Funcionalidades
- Integração com Azure Agent Services
- Criação e gerenciamento de agentes
- Processamento de threads de conversa
- API REST para interação com agentes

## Configuração
1. Clone o repositório
2. Configure no `appsettings.json`:
    - URI do Azure Agent Services
    - Modelo a ser utilizado
3. Configure suas credenciais do Azure
4. Execute o projeto

## Endpoints API
- `/basic-flow`: Demonstra o fluxo básico de conversação com um agente
- `/create-thread`: Cria uma nova thread de conversa com um agente

## Referências
- [Quick Start Guide - Semantic Kernel](https://learn.microsoft.com/en-us/semantic-kernel/get-started/quick-start-guide?pivots=programming-language-csharp)
- [Azure AI Agent Streaming Sample](https://github.com/microsoft/semantic-kernel/blob/main/dotnet/samples/Concepts/Agents/AzureAIAgent_Streaming.cs#L121)