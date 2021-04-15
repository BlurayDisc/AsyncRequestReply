# AsyncRequestReply API

Application built using ASP .NET Core 5.0 WebApi.  

This application demonstrates an implementation of Asynchronous Request-Reply pattern.

### Publish
```ps
dotnet publish -c Release
```

### Building a Docker image
```ps
docker build -t async-request-reply -f Dockerfile .
```

### Running the Docker image
```ps
docker run -it --rm -p 8080:5001 --name myapp async-request-reply
```