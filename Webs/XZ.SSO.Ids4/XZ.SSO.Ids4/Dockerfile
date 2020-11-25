FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Webs/XZ.SSO.Ids4/XZ.SSO.Ids4/XZ.SSO.Ids4.csproj", "Webs/XZ.SSO.Ids4/XZ.SSO.Ids4/"]
RUN dotnet restore "Webs/XZ.SSO.Ids4/XZ.SSO.Ids4/XZ.SSO.Ids4.csproj"
COPY . .
WORKDIR "/src/Webs/XZ.SSO.Ids4/XZ.SSO.Ids4"
RUN dotnet build "XZ.SSO.Ids4.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XZ.SSO.Ids4.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XZ.SSO.Ids4.dll"]