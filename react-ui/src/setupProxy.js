const { createProxyMiddleware } = require('http-proxy-middleware');

module.exports = function(app) {
  app.use(
    '/product',
    createProxyMiddleware({
      target: 'https://18.224.190.254:5003',
      changeOrigin: true,
      secure: false,
    })
  );

  app.use(
      '/roles',
      createProxyMiddleware({
        target: 'https://127.0.0.1:5005',
        changeOrigin: true,
        secure: false,
      })
  )
};