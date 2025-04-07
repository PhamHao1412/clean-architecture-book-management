# Base stage: Use the ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5001

# Environment variable to ensure the app listens on port 5001
ENV ASPNETCORE_URLS=http://+:5001
ENV ASPNETCORE_ENVIRONMENT=Development

# Build stage: Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["My Movie/My Movie.csproj", "My Movie/"]
RUN dotnet restore "My Movie/My Movie.csproj"

# Copy the entire project and build it
COPY . .
WORKDIR "/src/My Movie"
RUN dotnet build "My Movie.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage: Create a self-contained build
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "My Movie.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage: Use the base image and copy the build output
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
# Set the entry point for the application
ENTRYPOINT ["dotnet", "My Movie.dll"] 