FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["jfrs_personal_loans/jfrs_personal_loans.csproj", "jfrs_personal_loans/"]
RUN dotnet restore "jfrs_personal_loans/jfrs_personal_loans.csproj"
COPY . .
WORKDIR "/src/jfrs_personal_loans"
RUN dotnet build "jfrs_personal_loans.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "jfrs_personal_loans.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "jfrs_personal_loans.dll"]