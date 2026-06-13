package cmd

import (
	"errors"
)

type CmdHandlerContext struct {
	cmd       string
	arguments map[string]string
}

type CmdHandlerResult struct {
	resultText string
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

		return &CmdHandlerResult{
			resultText: resultText,
		}, nil
	}

	cmdFunc := commandHandlers()[context.cmd]

	return cmdFunc(context.arguments), nil
}

func NewCommandContext(
	command string,
	arguments map[string]string) CmdHandlerContext {
	return CmdHandlerContext{
		cmd:       command,
		arguments: arguments,
	}
}

func knownCommand() map[string]string {
	return map[string]string{
		"print": "print something on the screen",
	}
}

func commandHandlers() map[string](func(arguments map[string]string) *CmdHandlerResult) {
	return map[string](func(arguments map[string]string) *CmdHandlerResult){
		"print": Print,
	}
}
