package interfaces

import (
	"github.com/takobz/open-me-cli/cli/pkg/models"
)

type OpenMeApi interface {
	CreateUser(user models.CreateUserRequest) (*models.CreateUserResponse, error)
	GetAllUsers() (*models.GetAllUsersResponse, error)
	CreateOAuthProvider(userId string, provider models.CreateOAuthProviderRequest) (*models.CreateOAuthProviderResponse, error)
	GetOAuthProviders(userId string) (*models.GetOAuthProvidersResponse, error)
	GetOAuthProvider(userId string, providerId string) (*models.CreateOAuthProviderResponse, error)
}
