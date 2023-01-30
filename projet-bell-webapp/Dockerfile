# Stage 1
FROM node:18.13.0 as node
RUN mkdir -p /app
WORKDIR /app
COPY ./package.json /app/
RUN npm install
COPY . /app/
EXPOSE 80
RUN npm run build --prod
# Stage 2
FROM nginx:alpine
COPY --from=node /app/dist/projet-bell-webapp /usr/share/nginx/html
