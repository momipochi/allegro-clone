IMAGE_NAME=allegro-clone-auth-service
CONTAINER_NAME=allegro-clone-auth-service-container
DOCKERFILE_PATH=./auth-service/Dockerfile

build:
	docker build -f ${DOCKERFILE_PATH} -t ${IMAGE_NAME} .

clean:
	docker rm -f $(CONTAINER_NAME)
	docker rmi -f $(IMAGE_NAME)