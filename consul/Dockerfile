# Use the official Consul image as the base
FROM  hashicorp/consul:latest

# Optional: You can copy custom configuration files into the container
# Copy a custom configuration file into the Consul container
# COPY consul-config.json /consul/config/

COPY config/consul-config.hcl /consul/config/consul-config.hcl

# Expose the ports Consul uses
EXPOSE 8500 8600/udp

# Optional: Run Consul agent with custom configurations (if needed)
CMD ["agent", "-client=0.0.0.0"]
