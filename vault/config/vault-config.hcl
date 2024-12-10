
service_registration "consul" {
  address      = "http://consul:8500"
  
  
}
listener "tcp" {
  address = "[::]:8200"
  tls_disable = "false"
  tls_cert_file = "/vault/certs/vault.crt"
  tls_key_file  = "/vault/certs/vault.key"
}

storage "raft" {
  path    = "/vault/data"  
  node_id = "node1"       
}

node_name = "Vault server"
api_addr = "http://vault:8200"
cluster_addr = "https://vault:8201"
ui = true
