package api

import (
	"bytes"
	"encoding/json"
	"fmt"
	"io"
	"net/http"

	"github.com/takobz/open-me-cli/cli/pkg/models"
)

type OpenMeApiImpl struct {
}

func (api *OpenMeApiImpl) CreateUser(user models.CreateUserRequest) (*models.CreateUserResponse, error) {
	res := sendHttpRequest("POST", "http://localhost:5151/user", user)

	var createdUser models.CreateUserResponse
	if err := parseResponseBody(res, &createdUser, http.StatusOK); err != nil {
		return &createdUser, err
	}

	return &createdUser, nil
}

func (api *OpenMeApiImpl) GetAllUsers() (*models.GetAllUsersResponse, error) {
	res := sendHttpRequest("GET", "http://localhost:5151/user", nil)

	var users models.GetAllUsersResponse
	if err := parseResponseBody(res, &users, http.StatusOK); err != nil {
		return &users, err
	}

	return &users, nil
}

func (api *OpenMeApiImpl) CreateOAuthProvider(
	userId string,
	provider models.CreateOAuthProviderRequest,
) (*models.CreateOAuthProviderResponse, error) {
	res := sendHttpRequest("POST", "http://localhost:5151/user/"+userId+"/providers", provider)

	var createdProvider models.CreateOAuthProviderResponse
	if err := parseResponseBody(res, &createdProvider, http.StatusOK, http.StatusCreated); err != nil {
		return &createdProvider, err
	}

	return &createdProvider, nil
}

func (api *OpenMeApiImpl) GetOAuthProviders(userId string) (*models.GetOAuthProvidersResponse, error) {
	res := sendHttpRequest("GET", "http://localhost:5151/user/"+userId+"/providers", nil)

	var providers models.GetOAuthProvidersResponse
	if err := parseResponseBody(res, &providers, http.StatusOK); err != nil {
		return &providers, err
	}

	return &providers, nil
}

func (api *OpenMeApiImpl) GetOAuthProvider(
	userId string,
	providerId string,
) (*models.CreateOAuthProviderResponse, error) {
	res := sendHttpRequest("GET", "http://localhost:5151/user/"+userId+"/providers/"+providerId, nil)

	var provider models.CreateOAuthProviderResponse
	if err := parseResponseBody(res, &provider, http.StatusOK); err != nil {
		return &provider, err
	}

	return &provider, nil
}

/*
* Reads and closes the response body, then:
*   - on 400 unmarshals the body into an APIErrorResponse and returns it as an error
*   - on an expected status unmarshals the body into the given model
*   - on any other status returns an error with the unexpected status
 */
func parseResponseBody(res *http.Response, model any, expectedStatuses ...int) error {
	defer res.Body.Close()

	body, err := io.ReadAll(res.Body)
	if err != nil {
		return err
	}

	if res.StatusCode == http.StatusBadRequest {
		var apiErr models.APIErrorResponse
		if err := json.Unmarshal(body, &apiErr); err != nil {
			return err
		}

		return &apiErr
	}

	for _, status := range expectedStatuses {
		if res.StatusCode == status {
			return json.Unmarshal(body, model)
		}
	}

	return fmt.Errorf("unexpected response status: %s", res.Status)
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
