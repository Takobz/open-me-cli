package models

type CreateUserRequest struct {
	DisplayName string `json:"displayName"`
	Email       string `json:"email"`
}

type CreateUserResponse struct {
	Id          string `json:"id"`
	DisplayName string `json:"displayName"`
	Email       string `json:"email"`
}
