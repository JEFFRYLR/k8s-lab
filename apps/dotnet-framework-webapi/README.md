# Web API con .NET Framework 4.8

## ¿Por qué Visual Studio 2026 no muestra las plantillas de Web API de .NET Framework?

Visual Studio 2026 está orientado principalmente a **.NET 9/10** (el nuevo .NET unificado y multiplataforma).
Las plantillas de **ASP.NET Web Application (.NET Framework)** ya no aparecen de forma predeterminada porque Microsoft las considera "plantillas heredadas".

### Razones principales

| Motivo | Detalle |
|--------|---------|
| **Cambio de paradigma** | Microsoft unificó .NET Framework y .NET Core en ".NET 5+" (luego 6, 7, 8, 9, 10). El nuevo SDK es multiplataforma. |
| **Workload no instalado** | Las plantillas de .NET Framework solo se incluyen al instalar el workload **"Desarrollo de ASP.NET y web"** *y* el componente opcional de herramientas de .NET Framework. |
| **Filtros de búsqueda** | El cuadro de diálogo "Nuevo proyecto" filtra por defecto para mostrar solo plantillas de .NET moderno. |

---

## Cómo crear un proyecto Web API de .NET Framework 4.8

### Opción 1 – Desde Visual Studio 2026

1. Abre Visual Studio 2026 y ve a **Herramientas → Obtener herramientas y características…**
2. En el instalador, selecciona el workload **"Desarrollo de ASP.NET y web"**.
3. Dentro de ese workload, en "Componentes individuales", asegúrate de marcar:
   - ✅ `.NET Framework 4.8 SDK`
   - ✅ `.NET Framework 4.8 targeting pack`
4. Haz clic en **Modificar** y espera que termine la instalación.
5. Vuelve a **Archivo → Nuevo → Proyecto**.
6. En el filtro de plantillas, cambia el desplegable de **"Todos los frameworks"** a **.NET Framework**.
7. Busca **"ASP.NET Web Application (.NET Framework)"** y selecciónala.
8. Elige **Web API** y asegúrate de que el framework sea **4.8**.

### Opción 2 – Desde la línea de comandos (dotnet CLI no aplica para .NET Framework)

.NET Framework **no usa** el SDK de `dotnet` CLI. La forma más sencilla sin Visual Studio es usar
las plantillas de NuGet o crear el proyecto manualmente (ver estructura en este repositorio).

### Opción 3 – Clonar esta plantilla de ejemplo

Este directorio contiene un proyecto de ejemplo listo para usar:

```bash
# Clonar el repositorio
git clone https://github.com/JEFFRYLR/k8s-lab.git
cd k8s-lab/apps/dotnet-framework-webapi

# Abrir la solución en Visual Studio
start src/MiWebApi.sln
```

---

## Estructura del proyecto

```
dotnet-framework-webapi/
├── README.md                      ← Este archivo
├── k8s/
│   ├── deployment.yaml            ← Deployment de Kubernetes
│   └── service.yaml               ← Service de Kubernetes
└── src/
    └── MiWebApi/
        ├── MiWebApi.csproj        ← Proyecto .NET Framework 4.8
        ├── Global.asax
        ├── Web.config
        ├── App_Start/
        │   └── WebApiConfig.cs
        ├── Controllers/
        │   └── ProductosController.cs
        ├── Models/
        │   └── Producto.cs
        └── Properties/
            └── AssemblyInfo.cs
```

---

## Ejecutar localmente

1. Abre `src/MiWebApi.sln` en Visual Studio 2026.
2. Presiona **F5** o **Ctrl+F5**.
3. Navega a `https://localhost:{puerto}/api/productos`.

---

## Construir la imagen Docker

> ⚠️ Las imágenes de .NET Framework **solo corren en contenedores Windows**.

```bash
cd apps/dotnet-framework-webapi/src
docker build -t mi-webapi-framework:latest .
```

---

## Desplegar en Kubernetes (nodos Windows)

```bash
kubectl apply -f apps/dotnet-framework-webapi/k8s/
kubectl get pods -n dotnet-framework
kubectl get svc  -n dotnet-framework
```

---

## ¿Por qué usar .NET Framework 4.8 hoy?

- Tienes código legado que depende de bibliotecas que **solo existen para .NET Framework**.
- Necesitas integraciones con **COM**, **WCF** full, o APIs específicas de Windows.
- Migración gradual: primero contenerizas en Windows y luego migras a .NET moderno.

Para proyectos nuevos se recomienda **ASP.NET Core Web API (.NET 8+)**.
