export default {
    target: 'static',
    buildModules:[
        "@nuxtjs/vuetify"
    ],
    modules:[
        "@nuxtjs/axios"
    ],
    components:true,
    build: {
        extend(config, ctx) {
          if (ctx.isDev) {
            config.devtool = ctx.isClient ? 'source-map' : 'inline-source-map'
          }
        }
      }
}
