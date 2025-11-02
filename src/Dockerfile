FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY . ./

# Restore dependencies
RUN dotnet restore "VisiarAssesment.sln"

# Build the application
RUN dotnet build "VisiarAssesment.sln" -c Release -o /app/build

# Publish the application
RUN dotnet publish "VisiarAssesment.sln" -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# Create a non-root user (optional but recommended)
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "Assesment.Api.dll"]