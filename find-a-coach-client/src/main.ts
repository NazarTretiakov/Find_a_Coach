import { createApp } from 'vue'

import App from './App.vue'
import router from './plugins/router.ts'
import { createPinia } from 'pinia'
import vuetify from './plugins/vuetify.ts'

const app = createApp(App)

app.use(router)
app.use(createPinia())
app.use(vuetify)

app.mount('#app')