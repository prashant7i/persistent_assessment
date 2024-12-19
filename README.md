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

1. **Containerize:** Create Dockerfile in PersistentAssessmentApi.
2. **Push to GCR:** Tag and push image to Google Container Registry.
3. **Deploy to GKE:** Create GKE cluster and deploy using deployment/service configs. 
   (Or use Google App Engine for easier deployment)

**License:** MIT License
