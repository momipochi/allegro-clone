FROM hashicorp/vault:latest

# Set Vault configuration directory
ENV VAULT_CONFIG_DIR=/vault/config

# Create directories for config and certs
RUN mkdir -p $VAULT_CONFIG_DIR /vault/certs

# Copy Vault configuration into the container
COPY config/vault-config.hcl $VAULT_CONFIG_DIR/vault-config.hcl

# Expose Vault HTTP port
EXPOSE 8200

# Run Vault with the specified configuration
CMD ["server", "-config=/vault/config/vault-config.hcl"]
