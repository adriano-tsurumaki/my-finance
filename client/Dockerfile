# Use official Node.js image
FROM node:18-alpine

# Set working directory
WORKDIR /app

# Install dependencies
COPY package.json package-lock.json* ./
RUN npm install

# Copy the rest of the application code
COPY . .

# Expose Next.js default port
EXPOSE 3000

# Start Next.js in development mode
CMD ["npm", "run", "dev"]