FROM microsoft/mssql-server-linux:2017-CU13
RUN apt-get update && apt-get install -y net-tools iputils-ping

RUN mkdir -p /opt/mssql-scripts

COPY setup.sql /opt/mssql-scripts

ENV MSSQL_SA_PASSWORD=P@ssw0rd2020
ENV ACCEPT_EULA=Y

RUN ( /opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'P@ssw0rd2020' -i /opt/mssql-scripts/setup.sql  \
    && pkill sqlservr 