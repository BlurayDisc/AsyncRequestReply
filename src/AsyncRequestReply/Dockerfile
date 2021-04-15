FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

COPY bin/Release/net5.0/publish/ App/
WORKDIR /App

EXPOSE 80
ENV ASPNETCORE_URLS=http://0.0.0.0:80

ENTRYPOINT ["dotnet", "AsyncRequestReply.dll"]