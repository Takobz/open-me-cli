package cmd

import (
	"errors"
	"fmt"
	"os"
)

type CmdHandlerContext struct {
	cmd       string
	arguments []string
}

type CmdHandlerResult struct {
	ResultText string
}

func (context *CmdHandlerContext) HandleCommand() (*CmdHandlerResult, error) {
	cliCommands := knownCommand()

	if context.cmd == "" {
		return nil, errors.New("Cmd can't be empty")
	}

	_, cmdExists := cliCommands[context.cmd]
	if !cmdExists {
		resultText := "Command " + context.cmd + " is not known, see below for known commands\n"
		for key, value := range cliCommands {
			resultText += key + " : " + value + "\n"
		}

		fmt.Print(resultText)
		os.Exit(1)
	}

	cmdFunc := commandHandlers()[context.cmd]

	return cmdFunc(context.arguments), nil
}

func NewCommandContext(
	command string,
	arguments []string) CmdHandlerContext {
	return CmdHandlerContext{
		cmd:       command,
		arguments: arguments,
	}
}

func knownCommand() map[string]string {
	return map[string]string{
		"print":       "print something on the screen",
		"create-user": "creates user in the open me cli",
	}
}

func commandHandlers() map[string](func(arguments []string) *CmdHandlerResult) {
	return map[string](func(arguments []string) *CmdHandlerResult){
		"print":       Print,
		"create-user": CreateUserCommandHadler().CreateUser,
	}
}
