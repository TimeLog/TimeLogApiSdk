FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TimeLog.Api.Core.Documentation/TimeLog.Api.Core.Documentation.csproj", "TimeLog.Api.Core.Documentation/"]
RUN dotnet restore "TimeLog.Api.Core.Documentation/TimeLog.Api.Core.Documentation.csproj"
COPY . .
WORKDIR "/src/TimeLog.Api.Core.Documentation"
RUN dotnet build "TimeLog.Api.Core.Documentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TimeLog.Api.Core.Documentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimeLog.Api.Core.Documentation.dll", "--environment=Development"]
