//const WorkerLoaderPlugin = require("craco-worker-loader");

const { loaderByName, addBeforeLoader } = require("@craco/craco");

const WorkerLoaderPlugin = {
  overrideWebpackConfig: ({ pluginOptions, webpackConfig, context: { env } }) => {
    const workerLoader = {
      test: /\_worker\.(c|m)?js$/i,
      use: {
        loader: "worker-loader",
        options: env == 'production' 
          ? {...(pluginOptions || {}), inline:'fallback'} 
          : { 
          ...(pluginOptions || {})
          , filename: function(pathData) {
              return '[name].[hash].worker.js';
          }
        }
      }
    };
    addBeforeLoader(webpackConfig, loaderByName("babel-loader"), workerLoader);
    return webpackConfig;
  }
};

module.exports = function({ env }) {
  return {
    plugins: [{
     plugin: WorkerLoaderPlugin
    }],
  }
};