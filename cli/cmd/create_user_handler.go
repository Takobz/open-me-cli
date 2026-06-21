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

type CreateUserHandler struct {
	openMeApi interfaces.OpenMeApi
}

func (handler *CreateUserHandler) CreateUser(arguments []string) *CmdHandlerResult {
	createUserFlagSet := flag.NewFlagSet("create-user", flag.ExitOnError)

	displayName := createUserFlagSet.String(
		"display-name",
		"",
		"(Required) username to identify you",
	)

	email := createUserFlagSet.String(
		"email",
		"",
		"(Required) email that belongs to the user",
	)

	createUserFlagSet.Parse(arguments)

	if *displayName == "" || *email == "" {
		fmt.Print("Both --username and --email flags are required for user creation \n")
		os.Exit(1)
	}

	createdUser := handler.createUserViaAPI(
		*displayName,
		*email,
	)

	jsonData, err := json.Marshal(createdUser)
	if err != nil {
		panic(err)
	}

	return &CmdHandlerResult{
		ResultText: string(jsonData),
	}
}

func CreateUserCommandHadler() *CreateUserHandler {
	return &CreateUserHandler{
		openMeApi: &api.OpenMeApiImpl{},
	}
}

func (handler *CreateUserHandler) createUserViaAPI(
	displayName string,
	email string,
) *models.CreateUserResponse {
	user := models.CreateUserRequest{
		DisplayName: displayName,
		Email:       email,
	}

	createdUser, err := handler.openMeApi.CreateUser(user)
	if err != nil {
		panic(err)
	}

	return createdUser
}
