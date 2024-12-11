CONSUL_MAKE=./consul
AUTH_SERVICE_MAKE=./auth-service

build:
	make -C $(CONSUL_MAKE) build
	make -C $(AUTH_SERVICE_MAKE) build

clean:
	make -C $(CONSUL_MAKE) clean
	make -C $(AUTH_SERVICE_MAKE) clean

push:
	make -C $(CONSUL_MAKE) push
	make -C $(AUTH_SERVICE_MAKE) push

login:
	docker login

deploy:
	
	make -C $(CONSUL_MAKE) deploy
	make -C $(AUTH_SERVICE_MAKE) deploy

undeploy:
	make -C $(CONSUL_MAKE) undeploy
	make -C $(AUTH_SERVICE_MAKE) undeploy


up: login build push deploy

down: undeploy clean
