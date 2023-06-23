# Readme
## Certificate Create

```
openssl genrsa -out cms.arahk.com.key 4096
openssl req -key cms.arahk.com.key -out cms.arahk.com.csr -days 365 -new    
openssl x509 -days 365 -out cms.arahk.com.crt -key cms.arahk.com.key -in cms.arahk.com.csr -req  
openssl pkcs12 -inkey cms.arahk.com.key -out cms.arahk.com.pfx -export -in cms.arahk.com.crt 
```