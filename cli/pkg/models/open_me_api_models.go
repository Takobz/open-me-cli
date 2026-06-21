package models

type CreateUserRequest struct {
	DisplayName string `json:"displayName"`
	Email       string `json:"email"`
}

type CreateUserResponse struct {
	Id          string `json:"userId"`
	DisplayName string `json:"displayName"`
	Email       string `json:"email"`
}
