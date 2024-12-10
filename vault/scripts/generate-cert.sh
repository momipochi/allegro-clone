#!/usr/bin/dumb-init /bin/sh

VAULT_ADDR="http://127.0.0.1:8200"
CERT_DIR="/vault/certs"

mkdir -p "$CERT_DIR"

echo "Starting Vault in dev mode to initialize PKI..."
vault server -dev &

# Wait for Vault to start
sleep 5

# Set Vault address
export VAULT_ADDR=$VAULT_ADDR

echo "Enabling PKI secrets engine..."
vault secrets enable pki

echo "Configuring root certificate authority..."
vault write pki/root/generate/internal \
    common_name="vault.local" \
    ttl=8760h

echo "Configuring default certificate TTL..."
vault write pki/config/urls \
    issuing_certificates="$VAULT_ADDR/v1/pki/ca" \
    crl_distribution_points="$VAULT_ADDR/v1/pki/crl"

echo "Generating server certificate..."
CERT_OUTPUT=$(vault write pki/issue/vault.local \
    common_name="vault.local" \
    ttl=720h)

# Extract certificate and key from the output
echo "$CERT_OUTPUT" | grep -oP '(?<=certificate": ")[^"]+' > "$CERT_DIR/vault.crt"
echo "$CERT_OUTPUT" | grep -oP '(?<=private_key": ")[^"]+' > "$CERT_DIR/vault.key"

echo "Certificate and key generated:"
ls -l "$CERT_DIR"

echo "Stopping Vault dev server..."
pkill vault

echo "PKI setup complete."