Projeto usando entity framework com banco de dados em memória, não precisa de migrations, basta rodar o projeto: dotnet run.
Usuários para login:
    1-
    Email = "admin@admin.com"
    Senha = "admin"
    2-
    Email = "user@admin.com"
    Senha = "user"

Perguntas e respostas:
1) Qual fluxo os dados de um formulário em uma View deve percorrer até ser
armazenado na fonte de dados em um projeto DDD?
    1- Usuário inputa dados na view;
    2- Usuário envia o formulário;
    3- Controller recebe requisição, valida e envia para camada de serviço;
    4- Camada de serviço mapeia/cria para objeto de dominio;
    5- Dominio implementa regras de negócio;
    6- Serviço envia para repositório;
    7- Repositório persiste os dados no banco de dados;

2) Nossa aplicação necessita que um usuário esteja autenticado para poder navegar nas funcionalidades, em .net, como podemos desenvolver essa autenticação?
    Implementação de JWT token no login, armazenando-o em localstorage ou cookie, para ser enviado como header das requisições. Sendo as requisiões podendo ser validadas através de middlewares ou Class/Properties Attributes como [Authorize];


3) Estamos enfrentando problemas de performance na pagina inicial de um portal que tem muitos acessos. Essa página exibe eventos separados em 6 sessões do html,
filtrados, cada sessão, por uma regra de negócio. Trata-se de um projeto MVC, no qual são realizados 6 consultas via ORM para carregar a View que será processada no
servidor e disponibilizada ao browser. O que podemos fazer para tentar mitigar esse problema de performance?
    Diversas coisas podem ser feitas:
        - Executar as consultas ao mesmo tempo de maneira assincrona;
        - Partial views para cada sessão com sua própria request;
        - Avaliar queries geradas no ORM e assim talvez implementar query direto no banco, pois dependendo da complexidade as queries do ORM podem ser bem lentas;
        - Avaliar possibilidade da implementação de cache para a(s) resição(ões);
        - Avaliar possibilidade da implementação de background jobs para alimentar esses caches;
