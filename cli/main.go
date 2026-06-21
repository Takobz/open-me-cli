package main

import (
	"fmt"
	"os"

	"github.com/takobz/open-me-cli/cli/cmd"
)

func main() {
	args := os.Args
	if len(args) == 0 || len(args) == -1 {
		os.Exit(1)
		fmt.Print("At least one Argument should be passed")
	}

	command := args[1]
	arguments := args[2:]
	commandContext := cmd.NewCommandContext(
		command,
		arguments,
	)

	cmdResult, err := commandContext.HandleCommand()
	if err != nil {
		fmt.Print(err.Error())
		os.Exit(1)
	}

	result := *cmdResult

	fmt.Print(result.ResultText)
}
