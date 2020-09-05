# .NET Core + RabbitMQ

Olá esse é um projeto de estudos e testes em .net core para entender o mecanismo de uso do RabbitMQ com BackgroundService.
A instância do RabbitMQ está contida no arquivo de DockerCompose e para executa-lo basta roodar o comando abaixo na raiz do projeto:
```
docker-compose up -d
```

# Projetos

Temos dois projetos: 
### Sender
Que, obviamente, envia as mensagens através de um HttpPost:
```
https://localhost:5001/messages
Body:
{
"Content" : "Mensagem de teste"
}
```

### Receiver
Que, obviamente, recebe as mensagens através da fila e as escreve no Console da aplicação.

