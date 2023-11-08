# React Boilerplate

### Requirements

+ `node`  
+ `npm`

### Installation

+ `npm install`  
+ `typings install`

If `typings install` does not work, consider adding `./node_modules/.bin` to $PATH.  
Otherwise `npm i -g typings`.  
Instead of `npm install` you can use `yarn`.  
To install `yarn` just `npm i -g yarn`.  

### Start development

+ `npm run dev` to start all transpilers.  
+ `npm run dev:core` to start core transpiler.  
+ `npm run dev:frontend` to start frontend transpiler.  
+ `npm run compile` to compile.  

### Project structure

`build` contains the transpiled code for development stages.  
`dist` contains the transpiled code for production stages.  
`src/core` contains the application logic source code. Entrypoint `index.tsx`.  
`src/frontend` contains the source for the frontend javascript. Source starting from `index.tsx`.  