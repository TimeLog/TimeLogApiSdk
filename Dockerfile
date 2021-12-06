FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TimeLog.Api.Documentation/TimeLog.Api.Documentation.csproj", "TimeLog.Api.Documentation/"]
RUN dotnet restore "TimeLog.Api.Documentation/TimeLog.Api.Documentation.csproj"
COPY . .
WORKDIR "/src/TimeLog.Api.Documentation"
RUN dotnet build "TimeLog.Api.Documentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TimeLog.Api.Documentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimeLog.Api.Documentation.dll", "--environment=Development"]
