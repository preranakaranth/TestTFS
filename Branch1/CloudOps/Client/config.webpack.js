let webpack = require('webpack');
let path = require('path');

module.exports = {
    context: __dirname,
    devtool: "eval",
    resolve: {
        extensions: [
            "",
            ".webpack.js",
            ".web.js",
            ".ts",
            ".tsx",
            ".js"
        ]
    },
    entry: {
        main: "./src/frontend/index.tsx"
    },
    output: {
        publicPath: "/js/",
        path: path.join("..", "wwwroot", "js"),
        filename: "site.bundle.js"
    },
    target: "web",
    module: {
        loaders: [{
                test: /\.tsx?$/,
                exclude: /(node_modules|bower_components)/,
                loader: "ts-loader"
            },
            {
                test: /\.css$/,
                loader: 'style!css!'
            },
            {
                test: /\.scss$/,
                loader: 'style!css!sass!'
            },
            {
                test: /\.jpg$/,
                loader: 'url-loader?mimetype=image/jpeg&limit=100000&name=images/img-[name].[ext]'
            },
            {
                test: /\.json$/,
                loader: 'json'
            }
        ]
    }
};