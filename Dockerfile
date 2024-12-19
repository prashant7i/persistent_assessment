# Use .NET runtime as base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PersistentAssessmentApi/PersistentAssessmentApi.csproj", "PersistentAssessmentApi/"]
COPY ["PersistentAssessmentApiTest/PersistentAssessmentApiTest.csproj", "PersistentAssessmentApiTest/"]
RUN dotnet restore "PersistentAssessmentApi/PersistentAssessmentApi.csproj"
COPY . .
WORKDIR "/src/PersistentAssessmentApi"
RUN dotnet publish -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PersistentAssessmentApi.dll"]
