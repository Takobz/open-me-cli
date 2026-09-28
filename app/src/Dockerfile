# ---------- Build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy csproj files first so the restore layer is cached
COPY adapters/in/web/OpenME.WEB.API/OpenME.WEB.API.csproj adapters/in/web/OpenME.WEB.API/
COPY application/OpenME.Core.Application/OpenME.Core.Application.csproj application/OpenME.Core.Application/
COPY domain/OpenME.Core.Domain/OpenME.Core.Domain.csproj domain/OpenME.Core.Domain/
COPY infrastructure/OpenME.Infrastructure/OpenME.Infrastructure.csproj infrastructure/OpenME.Infrastructure/
COPY adapters/out/data/OpenME.Data/OpenME.Data.csproj adapters/out/data/OpenME.Data/

RUN dotnet restore adapters/in/web/OpenME.WEB.API/OpenME.WEB.API.csproj

# Copy the rest of the source and publish
COPY . .
RUN dotnet publish adapters/in/web/OpenME.WEB.API/OpenME.WEB.API.csproj \
    -c Release -o /app/publish --no-restore

# ---------- Runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=5151
EXPOSE 5151

COPY --from=build /app/publish .

USER app
ENTRYPOINT ["dotnet", "OpenME.WEB.API.dll"]
