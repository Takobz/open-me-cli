package cmd

import (
	"encoding/json"
	"flag"
	"fmt"
	"os"

	"github.com/takobz/open-me-cli/cli/pkg/api"
	"github.com/takobz/open-me-cli/cli/pkg/interfaces"
	"github.com/takobz/open-me-cli/cli/pkg/models"
)

type CreateOAuthProviderHandler struct {
	openMeApi interfaces.OpenMeApi
}

func (handler *CreateOAuthProviderHandler) CreateOAuthProvider(arguments []string) *CmdHandlerResult {
	createOAuthProviderFlagSet := flag.NewFlagSet("create-oauth-provider", flag.ExitOnError)

	userId := createOAuthProviderFlagSet.String(
		"user-id",
		"",
		"(Required) id of the user to create the oauth provider for",
	)

	providerName := createOAuthProviderFlagSet.String(
		"provider-name",
		"",
		"(Required) name of the oauth provider",
	)

	createOAuthProviderFlagSet.Parse(arguments)

	if *userId == "" || *providerName == "" {
		fmt.Print("Both --user-id and --provider-name flags are required for oauth provider creation \n")
		os.Exit(1)
	}

	createdProvider := handler.createOAuthProviderViaAPI(
		*userId,
		*providerName,
	)

	jsonData, err := json.Marshal(createdProvider)
	if err != nil {
		panic(err)
	}

	return &CmdHandlerResult{
		ResultText: string(jsonData),
	}
}

func CreateOAuthProviderCommandHandler() *CreateOAuthProviderHandler {
	return &CreateOAuthProviderHandler{
		openMeApi: api.NewOpenMeApi(),
	}
}

func (handler *CreateOAuthProviderHandler) createOAuthProviderViaAPI(
	userId string,
	providerName string,
) *models.CreateOAuthProviderResponse {
	provider := models.CreateOAuthProviderRequest{
		OAuthProviderName: providerName,
	}

	createdProvider, err := handler.openMeApi.CreateOAuthProvider(userId, provider)
	if err != nil {
		panic(err)
	}

	return createdProvider
}
