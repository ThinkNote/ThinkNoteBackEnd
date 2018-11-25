FROM microsoft/dotnet:2.1-sdk AS build
ENV PROJ_NAME=ThinkNoteBackEnd

WORKDIR /app

COPY src/${PROJ_NAME}/. ./${PROJ_NAME}/

WORKDIR /app/${PROJ_NAME}/
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.1-aspnetcore-runtime AS runtime
WORKDIR /app

ENV PROJ_NAME=ThinkNoteBackEnd
COPY --from=build /app/${PROJ_NAME}/out ./

ENTRYPOINT ["dotnet","ThinkNoteBackEnd.dll"]

