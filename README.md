Um das Programm auszuführen, müssen folegende Schritte befolgt werden:

### Zookeeper in der Powershell/CMD starten
```
.\bin\windows\zookeeper-server-start.bat .\config\zookeeper.properties
```

### Kafka in anderer Powershell/CMD starten
```
.\bin\windows\kafka-server-start.bat .\config\server.properties
```

### Services starten
Zunächst den CoreBankingService starten und zu "https://localhost:44316/swagger/" navigieren. Nachdem die anderen zwei Services ebenfalls gestartet wurden, kann eine Geldmenge in Swagger eingegeben werden.