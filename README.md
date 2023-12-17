# BookApp
You can browse this project by url: https://bookappclient.azurewebsites.net.
To start the project, first open and run BookAppServer.sln, this will create the database and populate all the tables. Then, through Visual Studio Code, open the BookAppClient folder and enter "npm install" command in the terminal, this will install all the dependencies. After this you can launch the client application and view the application at url "http://localhost:4200/". in app you can login with existing account with username: "Admin" and password: "Password1000" to view administrator abilities or login as user with username: "User" and password: "Password1000".
# Docker
To start project in docker, in docker-compose.yml file, replace value of ASPNETCORE_Kestrel__Certificates__Default__Password on your certificate password, if you dont know your certificate password, open cmd and write these command one by one <br>
"dotnet dev-certs https --clean", <br>
"dotnet dev-certs https -ep %USERPROFILE%\.aspnet\https\onlinestoreserver.pfx -p awesomepass", <br>
"dotnet dev-certs https --trust"<br>
then just open cmd in root folder and write "docker-compose up --build" command. After container will started, you can view app by url "http://localhost:8082/". 
# Endpoints
## BookController
### (Get) https://localhost:5001/api/books
#### Description:  
Gets all books by parameters
#### Parameters:
1) TitleFilter - filters result by title
2) IncludeAuthor - determines whether include author to result
3) PageSize - determines count of books to include into result
4) PageNumber - determines result page number
#### Response headers:
1) X-Pagination - information about page size, page number, total count, recorded in json format. Example: {"CurrentPage":1,"TotalPages":1,"PageSize":6,"TotalCount":4,"HasPrevious":false,"HasNext":false}

### (Get) https://localhost:5001/api/books/{book id}
#### Description:  
Gets book by id

### (Get) https://localhost:5001/api/books/byAuthor/{author id}
#### Description:  
Gets books by author id and parameters
#### Parameters:
the same parameters from https://localhost:5001/api/books 
#### Response headers:
1) X-Pagination

### (Post) https://localhost:5001/api/books
#### Description:  
creates newly book, only members with "Admin" role can create books
#### Request body:
{  
  "Title": "",  
  "Description": "",  
  "Genres": "",  
  "AuthorId": ,  
  "Photo": "",  
  "FilePath": "",  
}
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

### (Post) https://localhost:5001/api/books/createBooks
#### Description:  
creates newly books, only members with "Admin" role can create books
#### Request body:
[  
  {  
    "Title": "",  
    "Description": "",  
    "Genres": "",  
    "AuthorId": ,  
    "Photo": "",  
    "FilePath": "",  
  },  
  ...  
]
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

### (Put) https://localhost:5001/api/books/{book id}
#### Description:  
updates book by id
#### Request body:
{  
  "Title": "",  
  "Description": "",  
  "Genres": "",  
  "AuthorId": ,  
  "Photo": "",  
  "FilePath": "",  
}
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

### (Delete) https://localhost:5001/api/books/{book id}
#### Description:  
deletes existing book by id.
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

## AuthorController
### (Get) https://localhost:5001/api/authors
#### Description:  
Gets all authors by parameters
#### Parameters:
1) NameFilter - filters result by title
3) PageSize - determines count of authors to include into result
4) PageNumber - determines result page number
#### Response headers:
1) X-Pagination - information about page size, page number, total count, recorded in json format. Example: {"CurrentPage":1,"TotalPages":1,"PageSize":6,"TotalCount":4,"HasPrevious":false,"HasNext":false}

### (Get) https://localhost:5001/api/authors/{author id}
#### Description:  
Gets author by id

### (Get) https://localhost:5001/api/authors/byAuthor/{author id}
#### Description:  
Gets books by author id and parameters
#### Parameters:
the same parameters from https://localhost:5001/api/authors 
#### Response headers:
1) X-Pagination

### (Post) https://localhost:5001/api/authors
#### Description:  
creates newly author, only members with "Admin" role can create authors
#### Request body:
{  
  "Name": "",  
  "Bio": ""  
}
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

### (Post) https://localhost:5001/api/authors/{author id}
#### Description:  
creates newly books for specified author, only members with "Admin" role can use this end point
#### Request body:
[  
  {  
    "Title": "",  
    "Description": "",  
    "Genres": "",  
    "AuthorId": ,  
    "Photo": "",  
    "FilePath": "",  
  },  
  ...  
]
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

### (Put) https://localhost:5001/api/authors/{author id}
#### Description:  
updates author by id.
#### Request body:
{  
  "Name": "",  
  "Bio": ""  
}
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

### (Delete) https://localhost:5001/api/authors/{author id}
#### Description:  
deletes existing author by id.
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

## CommentController
### (Get) https://localhost:5001/api/comments
#### Description:
returns comments by parameters
#### Parameters:
1) SearchedText - search comments by text
3) PageSize - determines count of comments to include into result
4) PageNumber - determines result page number

### (Get) https://localhost:5001/api/comments/byBook/{book id}
#### Description:
returns comments by book id and parameters
#### Parameters:
1) SearchedText - search comments by text
3) PageSize - determines count of comments to include into result
4) PageNumber - determines result page number

### (Get) https://localhost:5001/api/comments/GetByUser/{user id}
#### Description:
returns comments by user id and parameters
#### Parameters:
1) SearchedText - search comments by text
3) PageSize - determines count of comments to include into result
4) PageNumber - determines result page number
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

### (Post) https://localhost:5001/api/comments/{book id}
#### Description:
adds comment for specified book.
#### Request body:
{Plain text}
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

### (Put) https://localhost:5001/api/comments/{comment id}
#### Description:
updates comment by id.
#### Request body:
{Plain text}
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

### (Delete) https://localhost:5001/api/comments/{comment id}
#### Description:
deletes comment by id.
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

## UserBookController
### (Post) https://localhost:5001/api/userbooks/{book id}
#### Description:
Adds specified book to favorities list
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

### (Get) https://localhost:5001/api/userbooks
#### Description:
gets books from favorities list
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

### (Get) https://localhost:5001/api/userbooks/{book id}
#### Description:
checks wether the specified book in favorities list
#### Request Headers:
1) Authorization - header for authorization jwt token. Example "Bearer {your jwt token}"

## AuthenticationController
### (Post) https://localhost:5001/api/auth
#### Description:
registers newly user
#### Request Body:
{ "userName":"", "password":"", "email":"", "phoneNumber":"" }

### (Post) https://localhost:5001/api/auth/login
#### Description:
auhenticate user, returns jwt token if success
#### Request Body:
{ "userName":"das", "password":"dsad" }
