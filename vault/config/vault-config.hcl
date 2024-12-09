# Storage Backend: File-based for local persistence
storage "file" {
  path = "/vault/data"
}

# Listener: Use the mounted certificates
listener "tcp" {
  address     = "0.0.0.0:8200"
  tls_cert_file = "/vault/certs/vault.crt"
  tls_key_file  = "/vault/certs/vault.key"
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
