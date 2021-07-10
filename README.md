# CSC492 Senior Design Project - REST API
### This REST API connects the *Go Ahead, Make My Movie Night* desktop application to a database storing both users and their saved movie list.
---
The API's `DataController` class can make the following requests:
#### `GET` User: `{host-url}/{application-hash}`

This request calls the controller method `GetUserAsync` which takes in a unique hash identifier from the desktop application and returns a User object containing the associated UID retrieved from the database.
If no such user exists in the database, this event will trigger the `CreateUserAsync` method in the `DataProcessor` class to generate a new entry in the database associated with the hash, before returning the newly generated UID.
#### `GET` Movie: `{host-url}/data/{UID}/movie`

This request calls the controller method `GetMovieAsync` which takes in the unique UID of the user and retrieves from the database the first movie up next for them to watch and returns a Movie object with the IMDb movie identifier in the response body. Upon completion returns a `202 Accepted` response.
#### `POST` Movie: `{host-url}/data/{UID}/{IMDb-movie-id}`

This request calls the controller method `AddMovieAsync` which takes in the unique UID and IMDb movie ID and stores the movie title in the database alongside the UID. Upon completion, returns a `200 Ok` response if successful or a `400 Bad Request` response.
#### `DELETE` Movie: `{host-url}/data/{UID}/{IMDb-movie-id}`

This request calls the controller method `DeleteMovieAsync` which takes in the unique UID and IMDb movie ID and removes the entry from the database. Upon completion, returns a `200 Ok` response if successful or a `404 Not Found` response.
