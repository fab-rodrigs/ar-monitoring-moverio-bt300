# 👓 Documentação do Projeto: Monitoramento de Dados em AR com Moverio BT-300

## 1. Visão Geral
Este projeto foca no desenvolvimento de uma aplicação de **Realidade Aumentada (AR)** para os óculos **Epson Moverio BT-300**. O objetivo principal é a visualização em tempo real de dados de tensão e corrente adquiridos via hardware da **National Instruments (NI)** e processados via **LabVIEW**, permitindo o monitoramento de máquinas de forma interativa através de marcadores (QR Codes).

## 2. Estado Atual do Projeto
O projeto foi recebido com uma base funcional voltada para a visualização de dados estáticos.

### Estrutura de Software (Unity/Vuforia)
* **Integração AR:** O Vuforia está configurado e realizando o rastreio (tracking) de marcadores com sucesso.
* **Scripts de Lógica (C#):**
    * `MachineData.cs`: Define a estrutura dos objetos de dados (ID, nome, limites e listas de amostras).
    * `MachineDataLoader.cs`: Atualmente realiza a carga de informações de um arquivo JSON local (`Resources/machines.json`).
    * `BaseGraph.cs`: Motor matemático para renderização de curvas de Bézier e componentes visuais do gráfico.
    * `VoltageGraph.cs` / `CurrentGraph.cs`: Scripts especializados na renderização visual de tensão (branco) e corrente (verde/vermelho).

### Validação de Hardware
* **Plataforma de Saída:** Epson Moverio BT-300 (SO Android).
* **Estação de Trabalho:** Dell G15 5530 (Desenvolvimento e Build).

## 3. Atividades em Execução
O foco atual é a migração da arquitetura de dados estáticos para um sistema dinâmico.

* **Otimização de UI:** Ajuste de transparência (Alpha) e escala dos painéis para garantir legibilidade nas lentes transparentes do óculos sem obstruir a visão do mundo real.
* **Implementação de Rede:** Criação do script de recepção via protocolo **UDP** no Unity para permitir a entrada de dados via rede Wi-Fi.
* **Lógica de Osciloscópio:** Adaptação do `BaseGraph` para suportar atualização contínua de pontos (FIFO - First-In, First-Out).

## 4. Cronograma de Próximos Passos

### Fase 1: Integração LabVIEW (Gateway)
* Desenvolvimento do VI no LabVIEW para leitura dos canais analógicos da placa NI.
* Formatação de strings JSON para transmissão via pacote UDP (Porta 5005).

### Fase 2: Sincronização e Testes
* Estabelecimento de uma taxa de atualização estável (20Hz ~ 50Hz) para manter a fluidez do gráfico sem sobrecarregar o processador do BT-300.
* Validação da precisão dos dados entre a bancada e a exibição AR.

### Fase 3: Build e Deployment
* Geração do `.apk` final e instalação no controlador do Moverio.
* Testes de campo com hardware real e marcadores fixos na máquina.

## 5. Arquitetura de Comunicação Proposta

```mermaid
graph LR
    A[Sensores/Bancada] --> B[Placa NI]
    B --> C[LabVIEW no PC]
    C -- "Wi-Fi (UDP String)" --> D[Epson Moverio BT-300]
    D --> E[Unity AR Scene]