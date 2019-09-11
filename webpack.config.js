var path = require("path");

const isProduction = process.argv.indexOf("-p") >= 0;
console.log(
    "Bundling for " + (isProduction ? "production" : "development") + "..."
);

module.exports = {
    mode: "development",
    entry: "./src/App.fsproj",
    output: {
        path: path.join(__dirname, "./public"),
        filename: "bundle.js",
    },
    devServer: {
        contentBase: "./public",
        port: 8080,
    },
    module: {
        rules: [{
            test: /\.fs(x|proj)?$/,
            use: "fable-loader"
        },
        {
            test: /\.(scss|css)$/,
            use: [
            isProduction ? MiniCssExtractPlugin.loader : "style-loader",
            "css-loader",
            "sass-loader"
            ]
        }]
    }
}