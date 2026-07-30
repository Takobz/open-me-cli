package api

import (
	"bytes"
	"encoding/json"
	"io"
	"net/http"

	"github.com/takobz/open-me-cli/cli/pkg/models"
)

type OpenMeApiImpl struct {
}

func (api *OpenMeApiImpl) CreateUser(user models.CreateUserRequest) (*models.CreateUserResponse, error) {
	res := sendHttpRequest("POST", "http://localhost:5151/user", user)

	var createdUser models.CreateUserResponse
	var err error
	if res.Status == "200 OK" {
		body, err := io.ReadAll(res.Body)
		if err == nil {
			err = json.Unmarshal(body, &createdUser)
			return &createdUser, nil
		}
	}

	return &createdUser, err
}

func (api *OpenMeApiImpl) GetAllUsers() (*models.GetAllUsersResponse, error) {
	res := sendHttpRequest("GET", "http://localhost:5151/user", nil)

	var users models.GetAllUsersResponse
	var err error
	if res.Status == "200 OK" {
		body, err := io.ReadAll(res.Body)
		if err == nil {
			err = json.Unmarshal(body, &users)
			return &users, nil
		}
	}

	return &users, err
}

func (api *OpenMeApiImpl) CreateOAuthProvider(
	userId string,
	provider models.CreateOAuthProviderRequest,
) (*models.CreateOAuthProviderResponse, error) {
	res := sendHttpRequest("POST", "http://localhost:5151/user/"+userId+"/providers", provider)

	var createdProvider models.CreateOAuthProviderResponse
	var err error
	if res.Status == "201 Created" || res.Status == "200 OK" {
		body, err := io.ReadAll(res.Body)
		if err == nil {
			err = json.Unmarshal(body, &createdProvider)
			return &createdProvider, nil
		}
	}

	return &createdProvider, err
}

func (api *OpenMeApiImpl) GetOAuthProviders(userId string) (*models.GetOAuthProvidersResponse, error) {
	res := sendHttpRequest("GET", "http://localhost:5151/user/"+userId+"/providers", nil)

	var providers models.GetOAuthProvidersResponse
	var err error
	if res.Status == "200 OK" {
		body, err := io.ReadAll(res.Body)
		if err == nil {
			err = json.Unmarshal(body, &providers)
			return &providers, nil
		}
	}

	return &providers, err
}

func (api *OpenMeApiImpl) GetOAuthProvider(
	userId string,
	providerId string,
) (*models.CreateOAuthProviderResponse, error) {
	res := sendHttpRequest("GET", "http://localhost:5151/user/"+userId+"/providers/"+providerId, nil)

	var provider models.CreateOAuthProviderResponse
	var err error
	if res.Status == "200 OK" {
		body, err := io.ReadAll(res.Body)
		if err == nil {
			err = json.Unmarshal(body, &provider)
			return &provider, nil
		}
	}

	return &provider, err
}

/*
* For now this only supports JSON content type
 */
func sendHttpRequest(
	httpVerb string,
	url string,
	data any,
) *http.Response {
	var req *http.Request
	var err error
	if data != nil {
		jsonData, err := json.Marshal(data)
		if err != nil {
			panic(err)
		}

		req, err = http.NewRequest(httpVerb, url, bytes.NewBuffer(jsonData))
		req.Header.Set("Content-Type", "application/json")
	} else {
		req, err = http.NewRequest(httpVerb, url, nil)
	}

	if err != nil {
		panic(err)
	}

	client := &http.Client{}
	res, err := client.Do(req)
	if err != nil {
		panic(err)
	}

	return res
}
