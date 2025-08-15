# catalogorecarga

![.NET](https://img.shields.io/badge/.NET-7.0-blue)  
![License](https://img.shields.io/badge/License-MIT-green)  

Aplicación de escritorio en **Windows Forms** para gestionar recargas, consumiendo una **Web API** y conectándose a una **base de datos SQL Server**. Permite listar, agregar, actualizar y desactivar recargas de manera sencilla.  

---

## Tabla de Contenido

- [Características](#características)  
- [Tecnologías](#tecnologías)  
- [Prerequisitos](#prerequisitos)  
- [Instalación](#instalación)  
- [Uso](#uso)  
- [API Ejemplos](#api-ejemplos)  
- [Repositorios Relacionados](#repositorios-relacionados)  
- [Capturas](#capturas)  
- [Licencia](#licencia)  

---

## Características

- Listar todas las recargas disponibles.  
- Agregar nuevas recargas.  
- Modificar recargas existentes.  
- Desactivar recargas.  
- Conexión directa a la **Web API** que gestiona la lógica de negocio.  

---

## Tecnologías

- C# Windows Forms  
- .NET 7  
- SQL Server  
- ASP.NET Core Web API  

---

## Prerequisitos

Antes de ejecutar la aplicación, asegúrate de tener:  

- Visual Studio 2022 o superior  
- .NET 7 SDK instalado  
- SQL Server en tu máquina o remoto  
- La Web API corriendo y accesible  

---

## Instalación

1. Clonar el repositorio principal (Windows Forms):  
```bash
git clone https://github.com/tuUsuario/SysRecargasApp.git
cd SysRecargasApp
```

2. Configurar la conexión a la base de datos en el archivo de configuración de la Web API y de la aplicación Windows Forms.  

3. Ejecutar el script SQL para crear la tabla y datos de ejemplo:  
```sql
CREATE TABLE Recargas (
    ID_Recarga INT PRIMARY KEY IDENTITY(1,1),
    Descripcion VARCHAR(100) NOT NULL,
    monto DECIMAL(10,2) NOT NULL,
    estadoRecarga VARCHAR(10) NOT NULL DEFAULT 'Activo'
);

-- Datos de ejemplo
INSERT INTO Recargas (Descripcion, monto) VALUES
('Recarga Claro Q10', 10.00),
('Recarga Claro Q20', 20.00),
('Recarga Claro Q50', 50.00),
('Recarga Tigo Q10', 10.00),
('Recarga Tigo Q20', 20.00),
('Recarga Tigo Q50', 50.00),
('Recarga Movistar Q10', 10.00),
('Recarga Movistar Q20', 20.00),
('Recarga Movistar Q50', 50.00),
('Recarga Internacional $5', 39.00),
('Recarga Internacional $10', 78.00),
('Recarga Internacional $15', 117.00),
('Paquete Datos Claro 500MB', 15.00),
('Paquete Datos Claro 1GB', 25.00),
('Paquete Datos Tigo 500MB', 15.00),
('Paquete Datos Tigo 1GB', 25.00),
('Paquete Datos Movistar 500MB', 15.00),
('Paquete Datos Movistar 1GB', 25.00),
('Paquete Redes Sociales 1 día', 5.00),
('Paquete Redes Sociales 7 días', 25.00);
```

4. Ejecutar la solución en Visual Studio.  

---

## Uso

1. Inicia la **Web API** desde Visual Studio o mediante `dotnet run`.  
2. Abre la aplicación (Windows Forms).  
3. Desde la interfaz, podrás listar, agregar, editar y desactivar recargas.  

---

## Capturas

**Base de Datos - Tabla Recargas con datos de ejemplo**  
<img width="597" height="706" alt="Base de Datos Recargas" src="https://github.com/user-attachments/assets/30a525e3-e483-4a84-b776-d6e554bb95e7" />  

**Pantalla Web API**  
<img width="800" alt="Pantalla Web API" src="https://github.com/user-attachments/assets/b8c03392-e2df-40a0-b6c4-cb292aa0449f" />  

**Pantalla Principal Windows Form**  
<img width="800" alt="Pantalla Principal Windows Form" src="https://github.com/user-attachments/assets/085d883e-4ea7-4737-b54c-e3a77cddeb7e" />  

**Pantalla de Detalle de Recarga**  
<img width="800" alt="Pantalla de Detalle de Recarga" src="https://github.com/user-attachments/assets/fa8d86d3-709a-4b25-8314-3e123dae4e96" />  

**Pantalla de Editar Recarga**  
<img width="800" alt="Pantalla de Editar Recarga" src="https://github.com/user-attachments/assets/c5665a63-38c2-4cc4-9bc7-c388c5a4d1f2" />  

---

## Licencia

Este proyecto está licenciado bajo la **MIT License**. Consulta el archivo [LICENSE](LICENSE) para más detalles.  

