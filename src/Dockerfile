FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
LABEL maintainer="elvia@elvia.no"

WORKDIR /app
COPY . .
RUN dotnet restore \
        ./core-demo-app.csproj \
    && dotnet publish \
        ./core-demo-app.csproj \
        --configuration Release \
        --output ./out


FROM mcr.microsoft.com/dotnet/runtime:6.0 AS runtime
LABEL maintainer="elvia@elvia.no"

RUN apt-get update
RUN apt-get upgrade -y

RUN addgroup application-group --gid 1001 \
    && adduser application-user --uid 1001 \
        --ingroup application-group \
        --disabled-password

WORKDIR /app
COPY --from=build /app/out .
RUN chown --recursive application-user .
USER application-user
ENTRYPOINT ["dotnet", "core-demo-app.dll"]
