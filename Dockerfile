FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
COPY . ./
RUN apt-get update \
    && apt-get install -y cabextract wget xfonts-utils \
    && curl -s -o ttf-mscorefonts-installer_3.7_all.deb http://ftp.us.debian.org/debian/pool/contrib/m/msttcorefonts/ttf-mscorefonts-installer_3.7_all.deb \
    && sh -c "echo ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true | debconf-set-selections" \
    && dpkg -i ttf-mscorefonts-installer_3.7_all.deb
RUN dotnet publish -c Release -o out && \
    dotnet test -c Release --no-build

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
LABEL maintainer="https://github.com/3dbrows"
WORKDIR /app
COPY --from=build-env /app/out .
COPY --from=build-env /usr/share/fonts/truetype/msttcorefonts /usr/share/fonts/truetype/msttcorefonts
ENTRYPOINT ["dotnet", "txt2png.dll"]