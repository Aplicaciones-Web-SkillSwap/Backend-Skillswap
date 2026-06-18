FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SkillSwap.Platform/SkillSwap.Platform/SkillSwap.Platform.csproj", "SkillSwap.Platform/SkillSwap.Platform/"]
RUN dotnet restore "SkillSwap.Platform/SkillSwap.Platform/SkillSwap.Platform.csproj"
COPY . .
WORKDIR "/src/SkillSwap.Platform/SkillSwap.Platform"
RUN dotnet build "SkillSwap.Platform.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SkillSwap.Platform.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SkillSwap.Platform.dll"]