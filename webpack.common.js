const path = require('path')
const webpack = require('webpack')
const HtmlWebpackPlugin = require('html-webpack-plugin')
const CleanWebpackPlugin = require('clean-webpack-plugin')
const ManifestPlugin = require('webpack-manifest-plugin')
const ExtractCssPlugin = require('mini-css-extract-plugin')

const paths = {
    DIST: path.resolve(__dirname, 'artefacts', 'ui'),
    SRC: path.resolve(__dirname, 'ui'),
    JS: path.resolve(__dirname, 'ui', 'js')
}

module.exports = {
    entry: path.join(paths.JS, 'app.jsx'),
    output: {
        path: path.resolve(__dirname, paths.DIST),
        filename: '[name].[chunkhash].js'
    },
    plugins: [
        new CleanWebpackPlugin([paths.DIST]),
        new webpack.HashedModuleIdsPlugin(),
        new ManifestPlugin(),
        new ExtractCssPlugin({ filename: '[name].[chunkhash].css' }),
        new HtmlWebpackPlugin({
            template: path.join(paths.SRC, 'index.html')
        })
    ],
    optimization: {
        splitChunks: {
            chunks: 'all'
        },
        runtimeChunk: false
    },
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                exclude: /node_modules/,
                use: [
                    'babel-loader'
                ]
            },
            {
                test: /\.css$/,
                use: [
                    {
                        loader: ExtractCssPlugin.loader
                    },
                    {
                        loader: 'css-loader',
                        options: {
                            sourceMap: false,
                            modules: true,
                            localIdentName: '[local]'
                        }
                    }
                ]
            },
            {
                test: /\.(png|jpg|gif)$/,
                use: {
                    loader: 'file-loader',
                    options: {
                        name: '[name].[ext]',
                        outputPath: 'images/'
                    }
                }
            }
        ]
    },
    resolve: {
        extensions: [ '.js', '.jsx' ]
    }
}
