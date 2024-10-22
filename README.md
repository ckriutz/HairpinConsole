# HairpinConsole

A test console app that connects to its own webapi

## Create a Self-Signed Certificate

**This is purely a test. Do not do this in production!**

```bash
# generate the private key
openssl genrsa -out tls.key 2048

# create the CSR
openssl req -new -key tls.key -out tls.csr

# create the cert
openssl x509 -req -days 365 -in tls.csr -signkey tls.key -out tls.crt

# convert to PFX
openssl pkcs12 -export -out tls.pfx -inkey tls.key -in tls.crt -passout pass:YourPassword
```