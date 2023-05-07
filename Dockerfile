FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ProbabilityTool.Api/ProbabilityTool.Api.csproj", "src/ProbabilityTool.Api/"]
COPY ["src/ProbabilityTool.Calculations/ProbabilityTool.Calculations.csproj", "src/ProbabilityTool.Calculations/"]
COPY ["src/ProbabilityTool.Models/ProbabilityTool.Models.csproj", "src/ProbabilityTool.Models/"]
COPY ["src/ProbabilityTool.DataStore/ProbabilityTool.DataStore.csproj", "src/ProbabilityTool.DataStore/"]
RUN dotnet restore "src/ProbabilityTool.Api/ProbabilityTool.Api.csproj"
COPY . .
WORKDIR "/src/src/ProbabilityTool.Api"
RUN dotnet build "ProbabilityTool.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProbabilityTool.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProbabilityTool.Api.dll"]
