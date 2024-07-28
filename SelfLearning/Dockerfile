# Use the official .NET image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

RUN apt-get update && apt-get install -y curl

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SelfLearning.csproj", "./"]
RUN dotnet restore "SelfLearning.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "SelfLearning.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SelfLearning.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SelfLearning.dll"]
