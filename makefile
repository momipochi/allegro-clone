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


start:
	kind create cluster --config=kind/cluster.yaml
	helm install --values helm/values-v2.yaml consul hashicorp/consul --create-namespace --namespace consul

	timeout 30
	kubectl apply --filename auth-service
	kubectl apply -f proxy/proxy-defaults.yaml
	timeout 30
	kubectl apply --filename intentions
	timeout 5
	kubectl apply --filename api-gw/consul-api-gateway.yaml --namespace consul
	timeout 30	
	kubectl apply --filename api-gw/routes.yaml --namespace consul
	kubectl apply --filename api-gw/intentions.yaml --namespace consul
	kubectl apply --filename rabc-reference-grant

	# kubectl port-forward svc/grafana --namespace default 3000:3000 &
	# kubectl port-forward svc/consul-ui --namespace consul 8501:443

end:
	helm uninstall consul -n consul
	kind delete cluster
