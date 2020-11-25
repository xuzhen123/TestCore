FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Webs/XZ.Css/XZ.Css.csproj", "Webs/XZ.Css/"]
COPY ["Infrastructure/XZ.Main.Repository/XZ.Main.Repository.csproj", "Infrastructure/XZ.Main.Repository/"]
COPY ["Shared/XZ.Infrastructure.Core/XZ.Infrastructure.Core.csproj", "Shared/XZ.Infrastructure.Core/"]
COPY ["Shared/XZ.Domain.Abstractions/XZ.Domain.Abstractions.csproj", "Shared/XZ.Domain.Abstractions/"]
COPY ["Domain/XZ.Main.Domain/XZ.Main.Domain.csproj", "Domain/XZ.Main.Domain/"]
COPY ["Shared/XZ.Core/XZ.Core.csproj", "Shared/XZ.Core/"]
RUN dotnet restore "Webs/XZ.Css/XZ.Css.csproj"

#COPY */*.csproj ./
#RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done 

COPY . .
WORKDIR "/src/Webs/XZ.Css"
RUN dotnet build "XZ.Css.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XZ.Css.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XZ.Css.dll"]