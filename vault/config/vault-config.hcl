
service_registration "consul" {
  address      = "127.0.0.1:8500"
}
listener "tcp" {
  address     = "127.0.0.1:8200"
  tls_disable = 1
  tls_cert_file = "/vault/certs/vault.crt"
  tls_key_file  = "/vault/certs/vault.key"
}

storage "raft" {
  path    = "/vault/data"  
  node_id = "node1"       
}


api_addr = "https://127.0.0.1:8200"
cluster_addr = "https://127.0.0.1:8201"
ui = true
