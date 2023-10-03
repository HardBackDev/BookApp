FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /home/app
COPY ./BookAppServer/BookAppServer.csproj ./BookAppServer/
COPY ./BookAppServer.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish ./BookAppServer/BookAppServer.csproj -o /publish/
WORKDIR /publish
ENV ASPNETCORE_URLS=https://+:5001;http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Docker
ENTRYPOINT ["dotnet", "BookAppServer.dll"]