$CONSUL_HTTP_TOKEN = kubectl get --namespace consul secrets/consul-bootstrap-acl-token --template='{{.data.token}}' | ForEach-Object { [System.Text.Encoding]::UTF8.GetString([Convert]::FromBase64String($_)) }
Write-Output $CONSUL_HTTP_TOKEN
