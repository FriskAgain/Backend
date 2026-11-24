# Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /app

# Copy csproj først
COPY AarhusSpaceProgram.MissionLogGenerator.csproj .
RUN dotnet restore

# Copy resten
COPY . .
RUN dotnet publish AarhusSpaceProgram.MissionLogGenerator.csproj -c Release -o out

# Runtime
FROM mcr.microsoft.com/dotnet/runtime:10.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "AarhusSpaceProgram.MissionLogGenerator.dll"]