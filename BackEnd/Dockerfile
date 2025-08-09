FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY OnlineShop/*.csproj OnlineShop/
COPY OnlineShop.sln ./
RUN dotnet restore OnlineShop/BackEnd.csproj

COPY OnlineShop/ OnlineShop/

WORKDIR /app/OnlineShop

EXPOSE 5242

CMD [ "dotnet", "watch", "run", "--urls","http://0.0.0.0:5242" ]
