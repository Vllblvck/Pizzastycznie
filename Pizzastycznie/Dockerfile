FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

ENV CONNECTION_STRING server=localhost;user=root;password=root;database=PizzastycznieDb
ENV SECURITY_KEY DC8AB32EA9866688A4EA218E02416E321C1C48EA97C06E5C3C5F293C2BF0374A

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Pizzastycznie.dll"]
