FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY OnlineShop/*.csproj OnlineShop/
COPY OnlineShop.sln ./
RUN dotnet restore OnlineShop/BackEnd.csproj

COPY OnlineShop/ OnlineShop/

WORKDIR /app/OnlineShop

RUN dotnet tool install --global dotnet-ef --version 9.0 \
    && echo 'export PATH="$PATH:/root/.dotnet/tools"' >> /etc/profile

ENV PATH="$PATH:/root/.dotnet/tools"

RUN dotnet ef migrations bundle --self-contained -r linux-x64 -o app/efbundle

EXPOSE 5242

CMD [ "dotnet", "watch", "run", "--urls","http://0.0.0.0:5242" ]
