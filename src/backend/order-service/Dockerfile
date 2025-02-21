# First stage: Build the JAR
FROM gradle:jdk23 AS builder
WORKDIR /app
COPY . .
RUN ./gradlew bootJar

# Second stage: Run the JAR
FROM openjdk:23-jdk
WORKDIR /app
COPY --from=builder /app/build/libs/*.jar java.jar
EXPOSE 8040
EXPOSE 8888
ENTRYPOINT ["java", "-jar", "java.jar"]
HEALTHCHECK --interval=1s --timeout=1s --retries=60 CMD curl --fail http://localhost:8080/actuator/health || exit 1