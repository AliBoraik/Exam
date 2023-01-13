FROM node:19.4.0-alpine
WORKDIR /core
ENV PATH /app/node_modules/.bin:$PATH
COPY . .
RUN npm install
RUN npm run build
CMD ["npm", "run", "serve"]
