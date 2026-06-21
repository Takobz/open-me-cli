# Open ME CLI

A CLI tool that can be used to have all your data in place from different sources using OAuth.

## Quick Start Guide

### Running Inside CLI Directory

This currently the CLI runs locally and requires one to have Go and .NET Installed.  
  
Below are the currently supported CLI commands by running directly in [cli](./cli/) directory:

```bash
# then run the executable - simple print to see if all is okay
# expected output: I love Busi
go run main.go print --text "I love Busi"

# The following commands depend on the API being up locally

# Run API first in /app/src/adapters/in/OpenME.CLI.WEB
dotnet run 

# creating user - in memory
# this will create a user
# expected output: {"userId":"90a3a020-1153-441d-8031-5aa0ef29486f","displayName":"khutso","email":"khutso@email.com"} 
go run main.go create-user --display-name khutso --email khutso@email.com

# gets all users created in memory
# expected output: {"users":[{"userId":"90a3a020-1153-441d-8031-5aa0ef29486f","displayName":"khutso","email":"khutso@email.com"}]}
go run main.go get-all-users
```

### Building And Running Executable

To run the executable, one would need to build the cli first, see steps below:  

```bash
# navigate into the cli dir and run:
go build -o build/open-me-cli

# go into the build dir
cd ./build

# then run the executable - simple print to see if all is okay
# expected output: I love Busi
./open-me-cli print --text "I love Busi"

# The following commands depend on the API being up locally

# Run API first in /app/src/adapters/in/OpenME.CLI.WEB
dotnet run 

# create user
# expected output: {"userId":"90a3a020-1153-441d-8031-5aa0ef29486f","displayName":"khutso","email":"khutso@email.com"} 
./open-me-cli create-user --display-name khutso --email khutso@email.com

# get all users
# expected output: {"users":[{"userId":"90a3a020-1153-441d-8031-5aa0ef29486f","displayName":"khutso","email":"khutso@email.com"}]}
./open-me-cli get-all-users

```

## Documetation

- [Use Cases](./docs/use-cases/use-cases-list.md)
- [Code Structure Design](./docs/architecture/ports-and-adapter.md)
