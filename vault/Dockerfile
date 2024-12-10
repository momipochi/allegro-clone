FROM hashicorp/vault:latest

# Set Vault configuration directory
ENV VAULT_CONFIG_DIR=/vault/config

# Create directories for config and certs


# Copy Vault configuration into the container
COPY config/vault-config.hcl $VAULT_CONFIG_DIR/vault-config.hcl
# Expose Vault HTTP port
COPY scripts/init-vault.sh /vault/scripts/init-vault.sh


EXPOSE 8200

# ENTRYPOINT [ "/vault/scripts/init-vault.sh" ]

# Run Vault with the specified configuration
CMD ["server","-dev","-dev-listen-address=0.0.0.0:8201"]
