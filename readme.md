# Docker
To run container enter below command.
```
docker run --name cms.arahk -p 8986:8986 -e "ConnectionStrings:Default=Server=host.docker.internal;Database=CMSDB;User Id=SA;Password=Password@123;TrustServerCertificate=true" -e "ASPNETCORE_CERT_PFX_PATH=/app/cms.arahk.com.pfx" -e "ASPNETCORE_CERT_PASSKEY=Password123" -e "ASPNETCORE_CERT_THUMBPRINT=3DF009A556F314FF81EB8E459AFAEE8CEC0ABFD5" -d cms.arahk.com:1.0
```

# Kubernetes
## 1. Create Namespace
User kubectl command

```
kubectl create namespace {{name of namespace}} --output yaml
```

*Use '-o yaml' flag to show yaml content to create namespace*

**Or Apply**
```
kubectl apply -f .k8s/1-namespace.yaml
```
--- 
<br/>

## 2. Create Secret
Use Kubectl command
```
kubectl create secret generic appsecret --type=string --from-literal=connection_string='{{secret value}}' --from-literal=azure_ad_clientid='{{secret value}}' --from-literal=azure_ad_tenantid='{{secret value}}' --output yaml
```
**Or Apply**
```
kubectl apply -f .k8s/2-secret.yaml
```
---
<br/>

## 3. Create Deployment
Use Kubectl command
```
kubectl create deployment {{name of deployment}} --image=arahk-cms:latest --port=7014 --output yaml --namespace {{name of namesspace}}
```

*Use '-o yaml' flag to show yaml content to create deployment*


**Or Apply**
```
kubectl apply -f .k8s/3-deployment.yaml
```

---
<br/>

## 4. Create Service
Use Kubectl command
```
kubectl create service clusterip {{name of service}} --tcp=7014:7014 --output yaml --namespace {{name of namesspace}}
```

*Use '-o yaml' flag to show yaml content to create service*


**Or Apply**
```
kubectl apply -f .k8s/4-service.yaml
```

---
<br/>

## 5. Create Ingress
Because Ingress has many detail to configures. Then we might apply follow file below for more accurate.
```
kubectl apply -f .k8s/5-ingress.yaml
```