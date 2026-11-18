# 3054 - Fullstack .NET

Repositório do curso 3054 - Fullstack .NET da plataforma [balta.io](https://balta.io).

> [!IMPORTANT]
> Todo conteúdo apresentado leva em consideração o que venho estudando ao longo do tempo e, consequentemente, difere do original. Determinados pontos, podem ser tidos como casos de _over engineer_, entretanto, são intencionais ao passo que sua aplicação serve para a fixação de conceitos.

Contém códigos de exemplos desenvolvidos durante as aulas.

Você pode encontrar os originais em:

- [balta.io](https://balta.io/cursos/fullstack-dotnet)
- [GitHub](https://github.com/balta-io/3054)

## Tecnologias Utilizadas

- [.NET 10+](https://dot.net)
- [Blazor](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
- [ASP.NET Core Minimal API](https://dotnet.microsoft.com/apps/aspnet/apis)
- [Visual Studio Community](https://visualstudio.com)
- [Visual Studio Code](https://code.visualstudio.com)
- [PostgreSQL](https://www.postgresql.org)

## Histórico de alterações

Acompanhe as [modificações aqui][changelog].

### Domínio e validação

- `Title` e `Description` removem espaços em branco no início e no fim antes de validar e armazenar o valor. O tamanho máximo é aplicado ao valor normalizado.
- A normalização compartilhada é feita por `TrimWhitespace`, em `Dima.Core.Primitives`; essa operação transforma a string, enquanto `Ensure` verifica condições sem normalizar a entrada.
- `Ensure.MaxLength` rejeita limites negativos com `ArgumentOutOfRangeException`. `Ensure.NotEmpty` trata `null`, vazio e texto composto apenas por espaços como violações da regra de domínio e lança `DomainException`.

## Licença

Copyright (c) 2024 Rene Bentes Pinto

Este projeto está sob a licença **MIT**. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

[changelog]: ../../commits
