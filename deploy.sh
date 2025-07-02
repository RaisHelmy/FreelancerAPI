#!/bin/bash

# Configuration - Update these values
IMAGE_NAME="docker.io/raishelmy/freelancer-api:latest"
DB_HOST="192.168.100.138,1433"  # Your SQL Server host
DB_PASSWORD="YourStrong@Passw0rd"  # Your SQL Server password
APP_PORT="5001"  # Port to expose the application

echo "🚀 Deploying Freelancer API..."

# Stop and remove existing container
echo "🛑 Stopping existing container..."
docker stop freelancer-api 2>/dev/null || true
docker rm freelancer-api 2>/dev/null || true

# Pull latest image
echo "📥 Pulling latest image..."
docker pull $IMAGE_NAME

# Run new container
echo "🔄 Starting new container..."
docker run -d \
  --name freelancer-api \
  --restart unless-stopped \
  -p $APP_PORT:8081 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e DB_HOST=$DB_HOST \
  -e DB_NAME=FreelancerApiDb \
  -e DB_USER=sa \
  -e DB_PASSWORD=$DB_PASSWORD \
  $IMAGE_NAME

echo "✅ Deployment completed!"
echo "🌐 Application available at: http://localhost:$APP_PORT"
echo "📖 API Documentation: http://localhost:$APP_PORT/swagger"

# Show container status
echo "📊 Container status:"
docker ps | grep freelancer-api