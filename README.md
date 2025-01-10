
# ASP.NET Core Microservices Integration Sample

![microservices](./assets/architecture-overview.png)

## Screenshots

Check out the [screenshots](./screenshots.md) for a visual overview.

## Project Overview

**HomeAnalytica** is a sample application developed to demonstrate the integration of ASP.NET Core technologies in a microservices-based architecture. 
The project highlights the technical interplay between Blazor, YARP (Yet Another Reverse Proxy), and microservices using REST/HTTP and gRPC.  
This application serves as a technical showcase, focusing primarily on the backend architecture and communication between the Blazor server and the microservices, rather than on extensive frontend design.  
In the UI, users can create sensor devices, which are stored in a PostgreSQL database via REST/HTTP. Additionally, sample data (e.g., temperature, humidity, and energy usage) can be submitted via gRPC to a MongoDB database. These data points can be visualized through line and bar charts.  

Future updates will primarily focus on refactoring efforts, such as addressing compiler warnings and improving code comments.  
See the [TODO List](#todo-list) for planned updates and improvements.

**Note:**
This code is intended for demonstration purposes only and is not suitable for production use. Sensitive information, such as secrets and credentials, should be stored securely using a dedicated key management solution or secret store.

The solution is divided into multiple projects to simulate key functionalities within an IoT data analytics platform:

### Key Components

1. **HomeAnalytica.Web (Blazor Web App)**  
    A Blazor Web App that provides a user-friendly interface for managing sensor devices and viewing sample sensor data. The UI is designed with simplicity in mind, allowing the focus to remain on backend and architectural functionality.  
    Apex Charts was integrated for creating interactive charts.  
    See also https://apexcharts.github.io/Blazor-ApexCharts/

2. **HomeAnalytica.Gateway.Yarp (Reverse Proxy)**
   - Acts as a reverse proxy to route requests between the Blazor frontend and backend microservices, enabling efficient load distribution and separation of concerns.

3. **HomeAnalytica.Grpc.Contracts (Protobuf Definitions)**
   - A library containing Protobuf definitions for gRPC communication between the Blazor Web App and the Data Collection microservice.

4. **HomeAnalytica.DataRegistry (Data Registry Microservice)**
   - Storage of sensor devices and related. 

5. **HomeAnalytica.DataCollection (Data Collection Microservice)**
   - Simulates the collection and storage of sample data, such as temperature, humidity, and energy consumption. Users manually input sample data through the frontend.


---

This sample project provides a practical example of integrating ASP.NET Core with microservices, gRPC, YARP, and Blazor.

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Docker](https://www.docker.com/) (optional, for containerization)

## Setting up the HTTPS Certificates (Windows)

To run the Docker container with HTTPS support for `HomeAnalytica.web` (the Blazor Web App), please create the following SSL certificates and trust them by following these steps:

```bash
dotnet dev-certs https
dotnet dev-certs https -ep %APPDATA%\ASP.NET\Https\HomeAnalytica.Web.pfx -p 8517c5bc-614d-49b7-8990-ff40578db4a5
dotnet dev-certs https --trust
```

## Getting Started

### Option 1: Start the Application with dotnet Commands

1. Clone the repository:

    ```bash
    git clone https://github.com/jasdvl/sample-aspnetcore-microservices.git
    ```

2. Navigate to the project directory:

    ```bash
    cd sample-aspnetcore-microservices/src
    ```

3. Restore dependencies:

    ```bash
    dotnet restore
    ```

Start the database service:

```
docker-compose up --build -d sensor-data-db
```

Run the application:

```
dotnet run --project ./HomeAnalytica.Web
```

### Option 2: Start All Services with Docker Compose

Clone the repository if you haven’t already:

```
git clone https://github.com/jasdvl/sample-aspnetcore-microservices.git
```

Navigate to the project directory:

```
cd sample-aspnetcore-microservices/src
```

Start all services (database and microservices) with Docker Compose:

```
docker-compose up --build -d
```

This will build and start all services defined in the docker-compose.yml file, including the database and all microservices.

## TODO List

- Fix compiler warnings
- Add comments where they are still missing

## Branching Strategy

Since I am the sole developer on this project, I primarily work on the `main` branch. I prefer to keep things simple by committing directly to `main` for most tasks. However, if a new feature requires multiple related commits or substantial changes, I will create feature branches to manage those updates. Once the feature is complete, the branch will be merged back into `main`. My goal is to keep the main branch stable and up to date.
