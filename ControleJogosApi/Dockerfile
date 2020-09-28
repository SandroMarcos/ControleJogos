#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ControleJogosApi/ControleJogosApi.csproj", "ControleJogosApi/"]
COPY ["Modelos.Validacao/Modelos.Validacao.csproj", "Modelos.Validacao/"]
COPY ["RegrasNegocio/RegrasNegocio.csproj", "RegrasNegocio/"]
COPY ["Infra/Infra.csproj", "Infra/"]
COPY ["AcessoADados/AcessoADados.csproj", "AcessoADados/"]
COPY ["Repositorios/Repositorios.csproj", "Repositorios/"]
COPY ["Modelos/Modelos.csproj", "Modelos/"]
COPY ["Modelos.Mapeamento/Modelos.Mapeamento.csproj", "Modelos.Mapeamento/"]
RUN dotnet restore "ControleJogosApi/ControleJogosApi.csproj"
COPY . .
WORKDIR "/src/ControleJogosApi"
RUN dotnet build "ControleJogosApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ControleJogosApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ControleJogosApi.dll"]
