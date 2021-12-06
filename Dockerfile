FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TimeLog.API.Documentation/TimeLog.API.Documentation.csproj", "TimeLog.API.Documentation/"]
RUN dotnet restore "TimeLog.API.Documentation/TimeLog.API.Documentation.csproj"
COPY . .
WORKDIR "/src/TimeLog.API.Documentation"
RUN dotnet build "TimeLog.API.Documentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TimeLog.API.Documentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TimeLog.API.Documentation.dll", "--environment=Development"]
