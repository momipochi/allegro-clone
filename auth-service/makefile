IMAGE_NAME=anderalextu/allegro-clone-auth-service:latest
DOCKERFILE_PATH=./auth-service/Dockerfile

build:
	docker build -f $(DOCKERFILE_PATH) -t $(IMAGE_NAME) .
	
push:
	docker push $(IMAGE_NAME)
	
clean:
	docker image rm -f $(IMAGE_NAME)

AUTH_SERVICE_K8S=./k8s.yml

deploy:
	kubectl apply -f $(AUTH_SERVICE_K8S)
	
undeploy:
	kubectl delete -f $(AUTH_SERVICE_K8S)