FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
COPY . ./
RUN apt-get update && \
    apt-get install -y --no-install-recommends ttf-wqy-zenhei && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*
RUN dotnet publish -c Release -o out && \
    dotnet test -c Release --no-build

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
LABEL maintainer="https://github.com/3dbrows"
WORKDIR /app
COPY --from=build-env /app/out .
RUN apt-get update && \
    apt-get install -y --no-install-recommends ttf-wqy-zenhei && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*
ENTRYPOINT ["dotnet", "txt2png.dll"]