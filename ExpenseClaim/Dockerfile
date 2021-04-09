FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ExpenseClaim.csproj", "ExpenseClaim/"]
RUN dotnet restore "ExpenseClaim/ExpenseClaim.csproj"
COPY . ExpenseClaim/
WORKDIR "/src/ExpenseClaim"
RUN dotnet build "ExpenseClaim.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExpenseClaim.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpenseClaim.dll"]
