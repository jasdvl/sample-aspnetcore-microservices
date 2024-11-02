# ASP.NET Core Microservices Integration Sample

![microservices](./assets/architecture-overview.png)

## Project Overview

**HomeAnalytica** is a sample application developed to demonstrate the integration of ASP.NET Core technologies in a microservices-based architecture. This project primarily showcases the technical interplay between microservices, gRPC, YARP (Yet Another Reverse Proxy), and Blazor.

The project serves as a technical sample and focuses on backend architecture and inter-service communication rather than extensive frontend design. The UI is minimal, with simple input fields to manually send sample data (temperature, humidity, energy usage) to the data collector service. The analytics service then returns statistical data, such as total consumption or average temperature, with potential for basic visualizations (e.g., bar charts) to illustrate core analytics features without elaborate UI design.

Future updates may include additional examples or technical refinements, but the focus will remain on demonstrating gRPC, microservices architecture, YARP routing, and Blazor integration.  
See the [TODO List](#todo-list) for planned updates and improvements.

**Note:**
Not for production. Store Secrets in Key Store etc.

The solution is divided into multiple projects to simulate key functionalities within an IoT data analytics platform:

### Key Components

1. **HomeAnalytica.Web (Blazor Web App)**
   - A Blazor Web App that provides a straightforward interface for users to input and view sample analytics data. The UI prioritizes simplicity to keep the focus on backend and architectural functionality.

2. **HomeAnalytica.Grpc.Contracts (gRPC Service)**
   - A gRPC service to facilitate efficient communication between microservices, allowing seamless data exchange and integration of insights from various components.

3. **HomeAnalytica.DataCollection (Data Collection Service)**
   - Simulates the collection and storage of sample data, such as temperature, humidity, and energy consumption. Users manually input sample data through the frontend.

4. **HomeAnalytica.Analytics (Analytics Service)**
   - Processes collected data to generate insights and return statistical recommendations based on sample usage patterns. 

5. **HomeAnalytica.Gateway.Yarp (Reverse Proxy)**
   - Acts as a reverse proxy to route requests between the Blazor frontend and backend microservices, enabling efficient load distribution and separation of concerns.

---

This sample project provides a practical example of integrating ASP.NET Core with microservices, gRPC, YARP, and Blazor, making it an ideal reference for understanding these technologies in action.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)
- [Docker](https://www.docker.com/) (optional, for containerization)

## Setting up the HTTPS Certificates (Windows)

To run the Docker container with HTTPS support, please create an SSL certificate and trust it by following these steps:

```bash
dotnet dev-certs https -ep %APPDATA%\ASP.NET\Https\HomeAnalytica.Analytics.pfx -p 8247c5bc-698a-42b7-5910-ec40578db4a5
dotnet dev-certs https -ep %APPDATA%\ASP.NET\Https\HomeAnalytica.DataCollection.pfx -p 9017c5bc-676d-49b7-8990-fe87578db4a5
dotnet dev-certs https -ep %APPDATA%\ASP.NET\Https\HomeAnalytica.Gateway.Yarp.pfx -p 4113c5ac-614d-49b7-8920-ff40578eb2b1
dotnet dev-certs https -ep %APPDATA%\ASP.NET\Https\HomeAnalytica.Web.pfx -p 8517c5bc-614d-49b7-8990-ff40578db4a5
dotnet dev-certs https --trust
```

## Getting Started

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

4. Run the application:

   ```bash
   dotnet run --project ./HomeAnalytica.Web
   ```

5. Open a web browser and navigate to [https://localhost:6200](https://localhost:6200) to view the application.

## Project Structure

- `HomeAnalytica.Web`: Blazor Web App

- `HomeAnalytica.Grpc.Contracts`: gRPC Service

- `HomeAnalytica.DataCollection`: Data Collection Microservice

- `HomeAnalytica.Analytics`: Analytics Microservice

- `HomeAnalytica.Gateway.Yarp`: Reverse Proxy

## TODO List

### Priority 1

### Priority 2

### Priority 3

## Branching Strategy

Since I am the sole developer on this project, I primarily work on the `main` branch. I prefer to keep things simple by committing directly to `main` for most tasks. However, if a new feature requires multiple related commits or substantial changes, I will create feature branches to manage those updates. Once the feature is complete, the branch will be merged back into `main`. My goal is to keep the main branch stable and up to date.
