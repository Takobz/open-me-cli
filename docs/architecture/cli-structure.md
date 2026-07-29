# CLI Project Structure

This document shows how the CLI project has been structured and responsibilities of different components.

## Folder Structure

cli
 | --> cmd
        | --> [Command Handlers Go files]
 | --> pkg
        | --> api
        | --> interfaces
        | --> models
 --> main.go

## What Each Directory Does

### cmd

This directory contains Go files that handle different commands that the cli exposes. Each `_handler.go` file has a function that takes an array of arguments as strings and returns a `CmdHandlerResult`.

### pkg

The pkg directory has the following directories:

- api - a module for calling the OpenME Rest API.
- interface - module for defining the api's implemented methods.
- models - module for api contructs used by the api.

### main.go file in root

This is the main entry point with `package main`. It is where the command line text is turned into a `CmdHandlerContext` struct, which is what command handlers in `cmd` directory understand how to use to execute a command.

## How A Command Is Handled

Details here...
