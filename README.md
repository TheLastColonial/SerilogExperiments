# Serilog Experiements

Following the guide by [Tim Corey](https://www.youtube.com/watch?v=_iryZxv8Rxw&t=53s).

## Serilog Documenation

- [Getting Started](https://github.com/serilog/serilog-aspnetcore#serilogaspnetcore---)
- [Configuration Guide](https://github.com/serilog/serilog-settings-configuration#serilogsettingsconfiguration--)

## Seq Log Server

Seq is free for single developer usage

### Setup with Docker

``` Powershell
$PH=$(echo 'P@ssw0rd' | docker run --rm -i datalust/seq config hash) `

docker run `
  --name seq `
  -d `
  --restart unless-stopped `
  -e ACCEPT_EULA=Y `
  -e SEQ_FIRSTRUN_ADMINPASSWORDHASH="$PH" `
  -v D:\temp\Logs:/data `
  -p 8081:80 `
  -p 5341:5341 `
  datalust/seq:latest
```