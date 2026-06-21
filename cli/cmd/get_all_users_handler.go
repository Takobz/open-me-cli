package cmd

import (
	"encoding/json"

	"github.com/takobz/open-me-cli/cli/pkg/api"
	"github.com/takobz/open-me-cli/cli/pkg/interfaces"
)

type GetAllUsersHandler struct {
	openMeApi interfaces.OpenMeApi
}

func (handler *GetAllUsersHandler) GetAllUsers(arguments []string) *CmdHandlerResult {
	users, err := handler.openMeApi.GetAllUsers()
	if err != nil {
		panic(err)
	}

	jsonString, err := json.Marshal(users)
	if err != nil {
		panic(err)
	}

	return &CmdHandlerResult{
		ResultText: string(jsonString),
	}
}

func CreateAllUsersHandler() *GetAllUsersHandler {
	return &GetAllUsersHandler{
		openMeApi: &api.OpenMeApiImpl{},
	}
}
