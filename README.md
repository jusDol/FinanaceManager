Dokumentacja dla projektu FinanceManagerAPI
Opis projektu
Projekt FinanceManagerAPI to aplikacja webowa oparta na architekturze REST API, która zarządza kategoriami finansowymi. Umożliwia dodawanie nowych kategorii, ich edytowanie, usuwanie oraz pobieranie listy dostępnych kategorii. API jest zbudowane przy użyciu ASP.NET Core i oferuje JWT do autoryzacji.

Technologie
.NET 6: Główna platforma, na której oparty jest projekt.
ASP.NET Core MVC: Wzorzec architektoniczny wykorzystywany do budowy aplikacji webowej.
Entity Framework Core: Technologia ORM (Object-Relational Mapping) służąca do pracy z bazą danych SQL.
JWT (JSON Web Tokens): Metoda autoryzacji za pomocą tokenów, która umożliwia autentykację użytkowników.

Endpoints
1. POST /api/auth/login
Autoryzacja użytkownika, zwraca token dostępowy (JWT) oraz token odświeżający.

Parametry:
Username: Nazwa użytkownika.
Password: Hasło.
Przykładowe zapytanie:
http
POST /api/auth/login
Content-Type: application/json

{
    "username": "Justyna",
    "password": "haslomaslo"
}
Przykładowa odpowiedź:
json
{
    "Token": "jwt-access-token-here",
    "RefreshToken": "jwt-refresh-token-here"
}
2. POST /api/auth/refresh-token
Wymienia token odświeżający na nowy token dostępowy (JWT).

Parametry:
RefreshToken: Token odświeżający, który będzie używany do uzyskania nowego tokena dostępowego.
Przykładowe zapytanie:
http
POST /api/auth/refresh-token
Content-Type: application/json

{
    "refreshToken": "jwt-refresh-token-here"
}
Przykładowa odpowiedź:
json
{
    "AccessToken": "new-jwt-access-token",
    "RefreshToken": "new-jwt-refresh-token"
}
3. GET /api/categories/get-categories
Pobiera listę wszystkich kategorii.

Przykładowe zapytanie:
http
GET /api/categories/get-categories
Przykładowa odpowiedź:
json
[
    {
        "Id": 1,
        "Name": "Food"
    },
    {
        "Id": 2,
        "Name": "Transportation"
    }
]
