# vault.hcl

# Enable Consul as the storage backend
storage "consul" {
  address = "localhost:8500"  # Consul address
  path    = "vault/"          # The path to store Vault data in Consul
}

# Enable the Consul listener for Vault's API
listener "tcp" {
  address     = "0.0.0.0:8200"
  cluster_address = "0.0.0.0:8201"
  tls_disable = 1  # Disable TLS for simplicity in testing (enable TLS in production)
}

# Disable mlock to allow Vault to run as a non-root user in development mode
disable_mlock = true

# Enable UI (optional)
ui = true
