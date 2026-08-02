package cmd

import (
	"encoding/json"
	"flag"
	"fmt"
	"os"

	"github.com/takobz/open-me-cli/cli/pkg/api"
	"github.com/takobz/open-me-cli/cli/pkg/interfaces"
)

type GetOAuthProviderHandler struct {
	openMeApi interfaces.OpenMeApi
}

func (handler *GetOAuthProviderHandler) GetOAuthProvider(arguments []string) *CmdHandlerResult {
	getOAuthProviderFlagSet := flag.NewFlagSet("get-oauth-provider", flag.ExitOnError)

	userId := getOAuthProviderFlagSet.String(
		"user-id",
		"",
		"(Required) id of the user the oauth provider belongs to",
	)

	providerId := getOAuthProviderFlagSet.String(
		"provider-id",
		"",
		"(Required) id of the oauth provider to get",
	)

	getOAuthProviderFlagSet.Parse(arguments)

	if *userId == "" || *providerId == "" {
		fmt.Print("Both --user-id and --provider-id flags are required to get an oauth provider \n")
		os.Exit(1)
	}

	provider, err := handler.openMeApi.GetOAuthProvider(*userId, *providerId)
	if err != nil {
		panic(err)
	}

	jsonString, err := json.Marshal(provider)
	if err != nil {
		panic(err)
	}

	return &CmdHandlerResult{
		ResultText: string(jsonString),
	}
}

func CreateGetOAuthProviderHandler() *GetOAuthProviderHandler {
	return &GetOAuthProviderHandler{
		openMeApi: api.NewOpenMeApi(),
	}
}
