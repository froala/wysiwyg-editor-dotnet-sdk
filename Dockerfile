FROM node:14.17.3
FROM mcr.microsoft.com/dotnet/core/sdk:2.2

LABEL maintainer="rizwan@celestialsys.com"

COPY --from=node / /

ARG PackageName
ARG PackageVersion
ARG NexusUser
ARG NexusPassword

ENV ASPNETCORE_ENVIRONMENT=Development

RUN apt update -y \
    && apt install -y jq unzip curl

COPY . /app
WORKDIR /app/demo-core

RUN npm install -g bower
RUN bower --allow-root install

RUN wget --no-check-certificate --user ${NexusUser}  --password ${NexusPassword} https://nexus.tools.froala-infra.com/repository/Froala-npm/${PackageName}/-/${PackageName}-${PackageVersion}.tgz
RUN tar -xvf ${PackageName}-${PackageVersion}.tgz

RUN rm -rf wwwroot/lib/froala-wysiwyg-editor
RUN mv package/ wwwroot/lib/froala-wysiwyg-editor

RUN rm -rf ${PackageName}-${PackageVersion}.tgz

EXPOSE 5002
CMD ["dotnet","run"]
