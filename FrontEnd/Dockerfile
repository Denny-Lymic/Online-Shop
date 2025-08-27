# Build vite project
FROM node:22.17.0 AS build

WORKDIR /app

COPY package*.json .
RUN npm ci

COPY . .

RUN npm run build

# Nginx stage
FROM nginx:alpine

COPY nginx/default.conf /etc/nginx/conf.d/default.conf
COPY nginx/gzip.conf /etc/nginx/conf.d/

COPY --from=build /app/dist /usr/share/nginx/html
