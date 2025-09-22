docker build -t us-east1-docker.pkg.dev/baxendev-cloudservice/christian-revpro/week1-api:latest .
docker push us-east1-docker.pkg.dev/baxendev-cloudservice/christian-revpro/week1-api:latest
@REM gcloud run deploy revpro-week1-api \
@REM   --image="us-east1-docker.pkg.dev/baxendev-cloudservice/christian-revpro/week1-api:latest" \
@REM   --region="us-east1" \
@REM   --allow-unauthenticated
@REM gcloud run services replace service.yaml --region="us-east1"