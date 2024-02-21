# MasterCompany

Descubre **MasterCompany**, tu  herramienta de gestión de recursos humanos. Simplifica tu proceso de gestión de recursos humanos con **MasterCompany**.

En esta rama se encontrara la api, la cual esta desarrollada en ASP .NET CORE API 8 y una metodologia en capas Controlador-Servicio-Modelo. Al igual que se implementaron los DTO como clases creadas por mi para el mejor manejo de los datos. Tambien se creo una carpeta llamada Utility, donde hay una clase llamada Util, en esta se encuentra los diferentes metodos que se invocan 1 o mas veces por los servicios, con la idea de no duplicar acciones dentro de los servicios y manejarlos de una manera mas entendible al separarlos.

- Los servicios a su vez estan compuestos por una interfaz y una clase que hereda de esta, donde se colocan todos los metodos que se usaran en el controlador.
En el program.cs podemos apreciar que las politicas CORS permiten acceso a cualquiera que se intente conectar al mismo, ya que no se especifico si deben haber retricciones.
- La api se desplego en un app service de azure, con la intencion de facilitar su consumo en la parte de angular, al no tener que subir ambas aplicaciones, aqui esta el link del swagger: https://mastercompanyapi.azurewebsites.net/swagger/index.html.
- Uno de los registros poseia una fecha invalida " 2014-05-901 ", para esto se valido la misma devolviendo el siguiente mensaje: Fecha Invalida 2014-05-901; ya que no se especificaba que hacer en este caso.

