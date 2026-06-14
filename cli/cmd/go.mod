module github.com/takobz/open-me-cli/cli/cmd

go 1.26.3

replace github.com/takobz/open-me-cli/cli/pkg/models => ../pkg/models

replace github.com/takobz/open-me-cli/cli/pkg/interfaces => ../pkg/interfaces

replace github.com/takobz/open-me-cli/cli/pkg/api => ../pkg/api

require (
	github.com/takobz/open-me-cli/cli/pkg/api v0.0.0-00010101000000-000000000000
	github.com/takobz/open-me-cli/cli/pkg/interfaces v0.0.0-00010101000000-000000000000
	github.com/takobz/open-me-cli/cli/pkg/models v0.0.0-00010101000000-000000000000
)
