FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

COPY bin/Release/net5.0/publish/ App/
WORKDIR /App

EXPOSE 5000 5001
ENV ASPNETCORE_URLS=http://0.0.0.0:5000;https://0.0.0.0:5001

ENTRYPOINT ["dotnet", "AsyncRequestReply.dll"]