package cmd

func Print(arguments map[string]string) *CmdHandlerResult {
	return &CmdHandlerResult{
		resultText: "Hello",
	}
}
