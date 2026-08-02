# Command and Usage

List commands we have and example usage here.

## Configuration

The CLI calls the Open ME API at `http://localhost:5151` unless the `OPEN_ME_API_URL` environment variable is set. Set it to test against a deployed instance, e.g.:

```bash
OPEN_ME_API_URL=https://my-deployed-instance.com ./open-me-cli get-all-users
```
