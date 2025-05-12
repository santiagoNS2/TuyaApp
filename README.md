# 🚀 TuyaApp - Prueba Técnica Backend .NET 8

Este proyecto es una solución para la prueba técnica de desarrollador backend en **.NET 8** con arquitectura **hexagonal (puertos y adaptadores)**, que gestiona clientes y órdenes, incluyendo:

- ✳️ Separación por capas: Domain, Application, Infrastructure, WebApi
- ✅ Repositorios, servicios, controladores REST y DTOs
- 🧪 Pruebas unitarias y de integración
- 📦 Swagger con documentación XML
- 💾 Persistencia real en SQL Server y pruebas aisladas con InMemory

---

## 🧱 Estructura del proyecto
TuyaApp/
├── Domain/ ← Entidades y interfaces

├── Application/ ← Servicios y DTOs

├── Infrastructure/ ← EF Core y repositorios

├── WebApi/ ← API REST con Swagger

├── Tests/ ← xUnit + Moq + Integration tests

---
## 📋 Requisitos para ejecutar

Asegúrate de tener lo siguiente instalado:

🔧 **Herramientas necesarias:**

- [Visual Studio 2022+](https://visualstudio.microsoft.com/es/) con el SDK de .NET 8
- [SQL Server LocalDB](https://learn.microsoft.com/es-es/sql/database-engine/configure-windows/sql-server-express-localdb) o una instancia SQL Server
- [Git](https://git-scm.com/) (opcional)

✅ **Paquetes NuGet usados:**

- `Microsoft.EntityFrameworkCore.SqlServer`
- `Swashbuckle.AspNetCore`
- `Microsoft.EntityFrameworkCore.InMemory` (para tests)
- `xUnit`, `Moq`, `Microsoft.AspNetCore.Mvc.Testing`

---

## 💻 Instrucciones para correrlo en Visual Studio

1. Clona el repositorio o abre la solución en Visual Studio:
   git clone https://github.com/santiagoNS2/TuyaApp.git

2. Asegúrate de que el proyecto de inicio sea `TuyaApp.WebApi`.

3. Revisa la cadena de conexión en:

`WebApi/appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=TuyaDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```
Cambia Server=localhost si tu instancia SQL Server tiene otro nombre.

4. Abre la Consola del Administrador de paquetes:

  * Herramientas → Administrador de paquetes NuGet → Consola

5. Ejecuta migraciones:
   Update-Database -StartupProject WebApi -Project Infrastructure
6. Presiona F5 o haz clic en Iniciar.
7. Se abrirá automáticamente Swagger UI en tu navegador:

📘 Endpoints disponibles (Swagger)
GET /api/customer → lista todos los clientes

POST /api/customer → crea un nuevo cliente

GET /api/customer/{id}

PUT /api/customer/{id}

DELETE /api/customer/{id}

GET /api/customer/cc/{cc}

POST /api/order → crea orden para un cliente

PUT /api/order/{id}/cancel → cancela orden

GET /api/order → lista todas las órdenes con cliente

GET /api/order/{id} → detalle de orden + cliente

🧪 Pruebas
✅ Unitarias (TuyaApp.Tests)
Validan comportamiento del OrderService con Moq.

✅ De integración
Usan WebApplicationFactory y InMemoryDatabase para simular HTTP real.

Verifican que los endpoints funcionen como un cliente real lo haría.

▶️ Cómo ejecutarlas:
Menú → Prueba > Ejecutar todas las pruebas

O abre Explorador de pruebas → Ejecutar

✍️ Autor
Santiago Naranjo Sánchez
GitHub
Desarrollador Backend .NET
