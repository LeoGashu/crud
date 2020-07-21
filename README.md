# Projeto CRUD Básico
Desenvolvido com .NET Core 3.1 e Angular 8

# Instruções de build e execução
    dotnet build
    
    dotnet run
    
# Configurações
Localizado no appsettings.json
- DBType
  - Mock: Simulação de banco de dados usando classes internas
  - Sqlite: Banco de dados simples relacional criado em tempo de execução
  - Mongo: Banco NoSql, deve ser configurado nos parâmetros de **MongoDBConnectionString**
    
# Pontos a serem melhorados
- Separação melhor dos componentes do Angular em componentes melhores e/ou diretivas
- Validação backend dos dados sendo recebidos
- Tratamento de erros tanto no front quanto no backend