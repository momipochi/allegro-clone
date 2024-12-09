# Use the official Consul image as the base
FROM consul:latest

# Optional: You can copy custom configuration files into the container
# Copy a custom configuration file into the Consul container
# COPY consul-config.json /consul/config/

# Expose the ports Consul uses
EXPOSE 8500 8600/udp

# Optional: Run Consul agent with custom configurations (if needed)
CMD ["agent", "-dev", "-client=0.0.0.0"]
