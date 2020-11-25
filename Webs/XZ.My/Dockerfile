FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Webs/XZ.My/XZ.My.csproj", "Webs/XZ.My/"]
RUN dotnet restore "Webs/XZ.My/XZ.My.csproj"
COPY . .
WORKDIR "/src/Webs/XZ.My"
RUN dotnet build "XZ.My.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XZ.My.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XZ.My.dll"]