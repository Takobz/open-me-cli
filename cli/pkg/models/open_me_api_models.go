package models

import (
	"fmt"
	"strings"
)

type APIErrorResponse struct {
	StatusCode    int      `json:"statusCode"`
	ErrorMessages []string `json:"errorMessages"`
}

func (e *APIErrorResponse) Error() string {
	return fmt.Sprintf("api error (status %d): %s", e.StatusCode, strings.Join(e.ErrorMessages, ", "))
}

type CreateUserRequest struct {
	DisplayName string `json:"displayName"`
	Email       string `json:"email"`
}

type CreateUserResponse struct {
	Id          string `json:"userId"`
	DisplayName string `json:"displayName"`
	Email       string `json:"email"`
}

type GetAllUsersResponse struct {
	Users []CreateUserResponse `json:"users"`
}

type CreateOAuthProviderRequest struct {
	OAuthProviderName string `json:"oAuthProviderName"`
}

type CreateOAuthProviderResponse struct {
	Id   string `json:"id"`
	Name string `json:"name"`
}

type GetOAuthProvidersResponse struct {
	OAuthProviders []CreateOAuthProviderResponse `json:"oAuthProviders"`
}
