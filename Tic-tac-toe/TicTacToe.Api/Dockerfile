FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /source
COPY ["TicTacToe.Api/TicTacToe.Api.csproj", "TicTacToe.Api/"]
COPY ["TicTacToe.Domain/TicTacToe.Domain.csproj", "TicTacToe.Domain/"]
COPY ["TicTacToe.Application/TicTacToe.Application.csproj", "TicTacToe.Application/"]
COPY ["TicTacToe.Interfaces/TicTacToe.Interfaces.csproj", "TicTacToe.Interfaces/"]
COPY . .
RUN dotnet restore "./TicTacToe.Api/TicTacToe.Api.csproj" --disable-parallel
RUN dotnet publish "./TicTacToe.Api/TicTacToe.Api.csproj" -c releas -o /app --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./ 

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "TicTacToe.Api.dll"]