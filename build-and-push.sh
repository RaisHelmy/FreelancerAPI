# Configuration
IMAGE_NAME="freelancer-api"
TAG="latest"
REGISTRY="docker.io/raishelmy"

echo "🏗️  Building multi-platform Docker image..."
docker buildx build \
  --platform linux/amd64,linux/arm64 \
  --tag $REGISTRY/$IMAGE_NAME:$TAG \
  --push \
  .

echo "✅ Multi-platform build and push completed!"
echo "📋 Image supports both AMD64 (x64) and ARM64 architectures"
echo "📋 To deploy on x64 server, run:"
echo "   docker pull $REGISTRY/$IMAGE_NAME:$TAG"
echo "   docker run -d -p 5001:8081 \\"
echo "     -e DB_HOST=your-sql-server \\"
echo "     -e DB_PASSWORD=your-password \\"
echo "     --name freelancer-api \\"
echo "     $REGISTRY/$IMAGE_NAME:$TAG"