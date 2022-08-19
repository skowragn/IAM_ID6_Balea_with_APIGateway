# Introduction 

The IAM POC solution with:

1) IDS6 as identity (the newest version with ASP.NET Core 6)
    https://duendesoftware.com/
    
2) Balea authorization usage: 
    https://balea.readthedocs.io/en/latest/ 
    
3) API Gateway/BFFs (with Ocelot and/or YARP usage)
   https://tanzu.vmware.com/developer/blog/build-api-gateway-csharp-yarp/
   https://microsoft.github.io/reverse-proxy/
   https://docs.microsoft.com/en-us/dotnet/architecture/microservices/multi-container-microservice-net-applications/implement-api-gateways-with-ocelot

# Getting Started

Setup as the start projects:
1) Assets.Management.Api
2) Assets.Management.Web
3) Identity.Service
4) Gateway (YARP) or Ocelot-Gateway (setup in the Assets.Management.Web -> PatientsController -> Index now)
