package cmd

import (
	"flag"
	"fmt"
	"os"
)

func Print(arguments []string) *CmdHandlerResult {
	printCommand := flag.NewFlagSet("print", flag.ExitOnError)

	textFlag := printCommand.String(
		"text",
		"No Text was provided",
		"Text to echo to terminal",
	)

	printCommand.Parse(arguments)

	if *textFlag == "" {
		fmt.Println("No text was provided with --text flag")
		os.Exit(1)
	}

	return &CmdHandlerResult{
		ResultText: *textFlag,
	}
}
