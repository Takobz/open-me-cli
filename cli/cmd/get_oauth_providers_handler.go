package cmd

import (
	"encoding/json"
	"flag"
	"fmt"
	"os"

	"github.com/takobz/open-me-cli/cli/pkg/api"
	"github.com/takobz/open-me-cli/cli/pkg/interfaces"
)

type GetOAuthProvidersHandler struct {
	openMeApi interfaces.OpenMeApi
}

func (handler *GetOAuthProvidersHandler) GetOAuthProviders(arguments []string) *CmdHandlerResult {
	getOAuthProvidersFlagSet := flag.NewFlagSet("get-oauth-providers", flag.ExitOnError)

	userId := getOAuthProvidersFlagSet.String(
		"user-id",
		"",
		"(Required) id of the user to get oauth providers for",
	)

	getOAuthProvidersFlagSet.Parse(arguments)

	if *userId == "" {
		fmt.Print("The --user-id flag is required to get oauth providers \n")
		os.Exit(1)
	}

	providers, err := handler.openMeApi.GetOAuthProviders(*userId)
	if err != nil {
		panic(err)
	}

	jsonString, err := json.Marshal(providers)
	if err != nil {
		panic(err)
	}

	return &CmdHandlerResult{
		ResultText: string(jsonString),
	}
}

func CreateGetOAuthProvidersHandler() *GetOAuthProvidersHandler {
	return &GetOAuthProvidersHandler{
		openMeApi: &api.OpenMeApiImpl{},
	}
}
