# App test evolution

# Ejecutar en local

## Paso 1:  Crear un usuario en sql server para crear la base de datos
**1)** Ir a **Microsoft sql server management** y crear un usuario
**2)** Ir al archivo que se encuentra en *sql/database.sql*
**3)** crear un archivo script desde sql server y ejecutar lo que se encuentra dentro del archivo *database.sql*
**4)** Ir al archivo *appsettings.json* para cambiar la cadena de conexión.
**"connectionString":** *"Data Source=**localhost**;Initial Catalog=**database**;User Id=**user**;Password=**password**;"*

## Paso 2: Restaurar los paquetes

```
dotnet restore
```

## Paso 3: Ejecutar la API
```
dotnet run
```
# Ejecutar online
Autenticarse
[https://testevolutionsas.herokuapp.com/api/user/authenticate](https://testevolutionsas.herokuapp.com/api/user/authenticate)

Para revisar todas las peticiones ingrese a los controladores de la API.
# Base de datos online
Cadena de conexión : sql5049.site4now.net
Usuario: DB_A561BC_testevolution_admin
Contraseña: test123456
