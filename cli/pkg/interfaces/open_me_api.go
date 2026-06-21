package interfaces

import (
	"github.com/takobz/open-me-cli/cli/pkg/models"
)

type OpenMeApi interface {
	CreateUser(user models.CreateUserRequest) (*models.CreateUserResponse, error)
	GetAllUsers() (*models.GetAllUsersResponse, error)
}
