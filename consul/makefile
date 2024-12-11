IMAGE_NAME=allegro-clone-consul
CONTAINER_NAME=allegro-clone-consul-container
DOCKERFILE_PATH=./Dockerfile

build:
    docker build -f ${DOCKERFILE_PATH} -t ${IMAGE_NAME}

clean:
	docker rm -f $(CONTAINER_NAME)
	docker rmi -f $(IMAGE_NAME)