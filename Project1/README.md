# BAXENs ArmaReforger Admin Tools MVP

This project follows a design from a previously implemented system stack.
The purpose of this API is to provide support for recording player information
from game servers to allow player lookup at a later date.

## API Endpoints

### Health Check
- **GET** `/health`
  - Returns a simple "Hello World" message to verify the API is running.

### Server Endpoints
- **POST** `/server/`
  - Registers a new server.
  - **Body:** `ServerRecordDto`
    ```json
    {
      "ipAddress": "127.0.0.1",
      "port": 2302,
      "name": "Test Server"
    }
    ```

- **PUT** `/server/`
  - Updates an existing server.
  - **Body:** `UpdateServerDto`
    ```json
    {
      "oldServer": {
        "ipAddress": "127.0.0.1",
        "port": 2302,
        "name": "Old Server"
      },
      "newServer": {
        "ipAddress": "127.0.0.1",
        "port": 2302,
        "name": "New Server"
      }
    }
    ```

- **DELETE** `/server/`
  - Deletes a server.
  - **Body:** `ServerRecordDto`
    ```json
    {
      "ipAddress": "127.0.0.1",
      "port": 2302,
      "name": "Test Server"
    }
    ```

### Player Endpoints
- **POST** `/player/serverConnection`
  - Registers a player connection to a server.
  - **Body:** `PlayerConnectionDto`
    ```json
    {
      "biIdentity": "player-unique-id",
      "playerName": "PlayerName",
      "playerIpAddress": "192.168.1.100",
      "connectionTime": "2024-06-01T12:00:00Z",
      "action": "connect",
      "serverIpAddress": "127.0.0.1",
      "serverPort": 2302
    }
    ```

- **GET** `/player/name`
  - Searches for player names.
  - **Body:** `NameDto`
    ```json
    {
      "name": "PlayerName"
    }
    ```


