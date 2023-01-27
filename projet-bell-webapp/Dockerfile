FROM node:lastest as build
WORKDIR /usr/local/app
COPY ./ /usr/local/app

RUN npm install -g @angular/cli

RUN npm run build

##EXPOSE 80:4200

CMD /project/node_modules/.bin/ng serve --host 0.0.0.0 
