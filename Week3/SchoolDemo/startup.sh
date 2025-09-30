#!/bin/bash

# .NET Development Startup Script
# Automates the full development workflow: restore, build, migrate, test, and run

# Configuration
SOLUTION_PATH="${PWD}"  # Parent directory containing .sln file
API_PROJECT_PATH="./School.Api"  # Adjust to your API project path
TEST_PROJECT_PATH="./School.Test"  # Adjust to your test project path
TEST_COVERAGE_SCRIPT="./test-report.sh"  # Path to your existing test coverage script
SWAGGER_URL="http://localhost:5202/swagger"  # Adjust port if needed

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
CYAN='\033[0;36m'
NC='\033[0m' # No Color

# Function to print section headers
print_header() {
    echo ""
    echo -e "${CYAN}========================================${NC}"
    echo -e "${CYAN}$1${NC}"
    echo -e "${CYAN}========================================${NC}"
}

# Function to handle errors
handle_error() {
    echo -e "${RED}❌ Error: $1${NC}"
    echo -e "${RED}🛑 Stopping execution due to failure${NC}"
    exit 1
}

# Function to check if a command succeeded
check_success() {
    if [ $? -ne 0 ]; then
        handle_error "$1"
    fi
    echo -e "${GREEN}✅ $2${NC}"
}

# Main script starts here
echo -e "${BLUE}🚀 .NET Development Startup Script${NC}"
echo -e "${BLUE}====================================${NC}"
echo ""

# Step 1: Restore the solution
print_header "📦 Step 1: Restoring Solution"
cd "$SOLUTION_PATH" || handle_error "Solution directory not found"
dotnet restore
check_success "Failed to restore solution" "Solution restored successfully"

# Step 2: Build the application
print_header "🔨 Step 2: Building Application"
dotnet build --no-restore
check_success "Failed to build application" "Application built successfully"

# Step 3: Build the unit tests
print_header "🧪 Step 3: Building Unit Tests"
dotnet build "$TEST_PROJECT_PATH" --no-restore
check_success "Failed to build unit tests" "Unit tests built successfully"

# Step 4: Check for model changes and create migration if needed
print_header "🗄️  Step 4: Checking for Database Changes"
cd "$API_PROJECT_PATH" || handle_error "API project directory not found"

echo -e "${YELLOW}Checking if migration is needed...${NC}"

# Try to create a migration with a timestamp name
MIGRATION_NAME="AutoMigration_$(date +%Y%m%d_%H%M%S)"
dotnet ef migrations add "$MIGRATION_NAME" --no-build > /dev/null 2>&1

if [ $? -eq 0 ]; then
    echo -e "${GREEN}✅ New migration created: $MIGRATION_NAME${NC}"
else
    echo -e "${YELLOW}ℹ️  No model changes detected - skipping migration${NC}"
fi

# Step 5: Update the database
print_header "📊 Step 5: Updating Database"
dotnet ef database update --no-build
check_success "Failed to update database" "Database updated successfully"

# Step 6: Run unit tests with coverage
print_header "🧪 Step 6: Running Unit Tests with Coverage"
cd "$SOLUTION_PATH" || handle_error "Cannot navigate back to solution directory"

if [ ! -f "$TEST_COVERAGE_SCRIPT" ]; then
    handle_error "Test coverage script not found at: $TEST_COVERAGE_SCRIPT"
fi

chmod +x "$TEST_COVERAGE_SCRIPT"
bash "$TEST_COVERAGE_SCRIPT" "$SOLUTION_PATH"
check_success "Unit tests failed" "Unit tests passed with coverage report generated"

# Step 7: Run the API
print_header "🌐 Step 8: Starting API Server"
echo -e "${YELLOW}Starting API on $SWAGGER_URL...${NC}"
echo -e "${YELLOW}Press Ctrl+C to stop the server${NC}"
echo ""

# Open Swagger in browser (do this before starting the API so it's ready when the server starts)
sleep 2  # Give the server a moment to start

echo -e "${BLUE}🌐 Opening Swagger documentation in browser...${NC}"
if command -v xdg-open > /dev/null; then
    # Linux
    (sleep 3 && xdg-open "$SWAGGER_URL") &
elif command -v open > /dev/null; then
    # macOS
    (sleep 3 && open "$SWAGGER_URL") &
elif command -v start > /dev/null; then
    # Windows (Git Bash, WSL)
    (sleep 3 && start "$SWAGGER_URL") &
else
    echo -e "${YELLOW}⚠️  Could not automatically open browser. Please navigate to: $SWAGGER_URL${NC}"
fi

# Run the API (this will block until Ctrl+C)
dotnet run --no-build

# If we get here, the server was stopped
echo ""
echo -e "${GREEN}👋 API server stopped${NC}"
