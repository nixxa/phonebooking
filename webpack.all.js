const merge = require('webpack-merge')
const path = require('path')
const common = require('./webpack.common.js')

module.exports = merge(common, {
    mode: 'none',
    devtool: 'inline-source-map',
    devServer: {
        contentBase: path.resolve(__dirname, 'ui')
    }
})
