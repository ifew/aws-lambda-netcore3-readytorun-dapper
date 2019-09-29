# Example C# .NET Core 3.0 Release and ReadyToRun with connect MySQL via Dapper Library

## Preparing

1. Import Database from database.sql
2. Add config database in your Environment Variable as "DB_CONNECTION" like this
```
DB_CONNECTION=Server=localhost;Port=3306;Uid=root;Pwd=1234;Database=lab;CharSet=utf8;ConvertZeroDateTime=True;
```

## Command for run
```bash
$ sh run.sh [Runtime]
```

Example for MacOS
```bash
$ sh run.sh osx-64
```

Example for Linux
```bash
$ sh run.sh linux-64
```

Example for Windows
```bash
$ sh run.sh win-64
```

## System Require for Linux

- `sudo apt-get install clang-3.9`
- `sudo apt-get install libunwind-dev`
- `sudo apt-get install libcurl4-openssl-dev`
- `sudo apt-get install zlib1g-dev`
- `sudo apt-get install libkrb5-dev`
- `export CppCompilerAndLinker=clang-3.9`
