# Storage Backend: File-based for local persistence
storage "consul" {
  address = "127.0.0.1:8500"  # Consul address
  path    = "vault/"          # The path to store Vault data in Consul
}
# Listener: Use the mounted certificates
listener "tcp" {
  address     = "0.0.0.0:8200"
  tls_cert_file = "/vault/certs/vault.crt"
  tls_key_file  = "/vault/certs/vault.key"
}

service_registration "consul" {
  address      = "127.0.0.1:8500"
}


# Enable UI for local testing
ui = true

# API and cluster address
 api_addr = "https://127.0.0.1:8200"
 cluster_addr = "https://127.0.0.1:8201"

# Telemetry for monitoring
telemetry {
  prometheus_retention_time = "24h"
}
