# ZenLog

## Descrição
O projeto **ZenLog** tem como objetivo integrar pedidos de venda, notas de saída e materiais entre dois ERP's.

## Funcionalidades
- **Sincronização de Materiais**
- **Sincronização notas de saída**
- **Sincronização notas de saída**
- **Log de Operações**: Gera logs detalhados de cada operação de integração, facilitando a auditoria e a detecção de erros.
- **Tratamento de Erros**: Captura e trata possíveis exceções durante o processo de integração, garantindo a estabilidade do sistema.

## Requisitos
- **.NET 6.0 ou superior** (C#)
- **SQL Server** versão 2016 ou superior
- **Oracle Database** versão 19c ou superior

### Pacotes NuGet:
- `Dapper`
- `Oracle.ManagedDataAccess`
- `System.Data.SqlClient`
- `Newtonsoft.Json` (opcional, caso haja uso de serialização JSON)
