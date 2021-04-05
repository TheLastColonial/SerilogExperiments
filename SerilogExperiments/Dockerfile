FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SerilogExperiments/SerilogExperiments.csproj", "SerilogExperiments/"]
RUN dotnet restore "SerilogExperiments/SerilogExperiments.csproj"
COPY . .
WORKDIR "/src/SerilogExperiments"
RUN dotnet build "SerilogExperiments.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SerilogExperiments.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SerilogExperiments.dll"]