docker build -t us-east1-docker.pkg.dev/baxendev-cloudservice/christian-revpro/week1-api:latest .;
docker push us-east1-docker.pkg.dev/baxendev-cloudservice/christian-revpro/week1-api:latest;
gcloud run deploy revpro-week1-api \
  --image="us-east1-docker.pkg.dev/baxendev-cloudservice/christian-revpro/week1-api:latest" \
  --region="us-east1" \
  --allow-unauthenticated
gcloud run services replace service.yaml --region="us-east1"