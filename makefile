CONSUL_MAKE=./consul
AUTH_SERVICE_MAKE=./auth-service

build:
	make -C $(CONSUL_MAKE) build
	make -C $(AUTH_SERVICE_MAKE) build

clean:
	make -C $(CONSUL_MAKE) clean
	make -C $(AUTH_SERVICE_MAKE) clean