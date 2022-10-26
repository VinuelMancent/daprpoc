FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DaprPoc/DaprPoc.csproj", "DaprPoc/"]
RUN dotnet restore "DaprPoc/DaprPoc.csproj"
COPY . .
WORKDIR "/src/DaprPoc"
RUN dotnet build "DaprPoc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DaprPoc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DaprPoc.dll"]
