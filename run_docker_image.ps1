docker container stop apidocumentationapp
docker container rm apidocumentationapp
docker run -d -p 8080:80 --name apidocumentationapp apidocumentation