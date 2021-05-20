FROM mcr.microsoft.com/dotnet/core/sdk:5.0-alpine AS build
LABEL maintainer="elvia@elvia.no"

WORKDIR /app
COPY . .
RUN dotnet restore \
        ./core-demo-app.csproj \
    && dotnet publish \
        ./core-demo-app.csproj \
        --configuration Release \
        --output ./out


FROM mcr.microsoft.com/dotnet/core/runtime:5.0-alpine AS runtime
LABEL maintainer="elvia@elvia.no"

RUN addgroup application-group --gid 1001 \
    && adduser application-user --uid 1001 \
        --ingroup application-group \
        --disabled-password

WORKDIR /app
COPY --from=build /app/out .
RUN chown --recursive application-user .
USER application-user
ENTRYPOINT ["dotnet", "core-demo-app.dll"]
