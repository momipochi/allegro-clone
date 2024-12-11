IMAGE_NAME=anderalextu/allegro-clone-consul:latest
DOCKERFILE_PATH=./Dockerfile

build:
	docker build -f $(DOCKERFILE_PATH) -t $(IMAGE_NAME) .

push:
	docker push $(IMAGE_NAME)

clean:
	docker image rm -f $(IMAGE_NAME)

CONSUL_K8S=./k8s.yml

deploy:
	kubectl apply -f $(CONSUL_K8S)

undeploy:
	kubectl delete -f $(CONSUL_K8S)