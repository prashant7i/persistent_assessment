# PersistentAssessment

.NET solution to check if a string contains all letters of the alphabet.

**Projects:**

* PersistentAssessmentApi (API)
* PersistentAssessmentApiTest (Unit Tests)

**Getting Started:**

1. **Prerequisites:**
   - .NET 6.0 SDK 
   - (Optional) Visual Studio/VS Code
   - (Optional) Git

2. **Clone (Optional):**
   `git clone git@github.com:prashant7i/persistent_assessment.git`

3. **Build:**
   `dotnet build PersistentAssessment.sln`

4. **Run API:**
   `dotnet run --project PersistentAssessmentApi` 
   (Access: https://localhost:7187 or https://localhost:7187)

5. **Run Tests (Optional):**
   `dotnet test PersistentAssessmentApiTest`

**API:**

* **POST /CheckAlphabet**
  - Input: JSON string (e.g., `{ "input": "The quick brown..." }`)
  - Output:
    - 200 OK: `{ "value": true/false }`
    - 400 Bad Request: (for empty/null input)

**Deployment to GCP (Optional):**

1. **Containerize:**
Use Docker to create a container for the PersistentAssessmentApi project. Create a Dockerfile in the PersistentAssessmentApi project directory.

```bash
# Use .NET runtime as base
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PersistentAssessmentApi/PersistentAssessmentApi.csproj", "PersistentAssessmentApi/"]
COPY ["PersistentAssessmentApiTest/PersistentAssessmentApiTest.csproj", "PersistentAssessmentApiTest/"]
RUN dotnet restore "PersistentAssessmentApi/PersistentAssessmentApi.csproj"
COPY . .
WORKDIR "/src/PersistentAssessmentApi"
RUN dotnet publish -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "PersistentAssessmentApi.dll"]
```

2. **Push to GCR:** 
Tag and push image to Google Container Registry.

```bash
# Tag the Docker image
docker tag persistent-assessment gcr.io/[your-project-id]/persistent-assessment

# Push to Google Container Registry
docker push gcr.io/[your-project-id]/persistent-assessment
```

3. **Deploy to GKE:** Create GKE cluster and deploy using deployment/service configs. 
   (Or use Google App Engine for easier deployment)

   a. **Create a Kubernetes Cluster** 
A Kubernetes cluster is a group of virtual machines (VMs) managed by GKE that run your containerized application.
In GCP, you can create a cluster using the Google Cloud Console, the gcloud CLI, or the GCP API.

   b. **Prepare Kubernetes Configuration Files**

      * **Deployment YAML file**: Specifies how to deploy the container (e.g., replicas, container image to use, environment variables).
      * **Service YAML file**: Defines how the application is exposed (e.g., as a LoadBalancer or ClusterIP service).

`deployment.yaml:`

```bash
apiVersion: apps/v1
kind: Deployment
metadata:
  name: persistent-assessment
spec:
  replicas: 2
  selector:
    matchLabels:
      app: persistent-assessment
  template:
    metadata:
      labels:
        app: persistent-assessment
    spec:
      containers:
      - name: persistent-assessment
        image: gcr.io/[your-project-id]/persistent-assessment
        ports:
        - containerPort: 80
```
`service.yaml`
```bash
apiVersion: v1
kind: Service
metadata:
  name: persistent-assessment-service
spec:
  type: LoadBalancer
  selector:
    app: persistent-assessment
  ports:
  - protocol: TCP
    port: 80
    targetPort: 80
```

&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; c. **Deploy the Application**
   
* Use `kubectl`, the Kubernetes CLI tool, to apply the YAML files to the cluster:

```bash
kubectl apply -f deployment.yaml
kubectl apply -f service.yaml
```

* Kubernetes will pull the container image from Google Container Registry (GCR), create pods, and expose the application.

    d. **Access the Application**

* If using a LoadBalancer service, Kubernetes assigns an external IP address. You can use this IP to access the application.
* Use kubectl get services to check the external IP assigned to your service.
  
**License:** MIT License
