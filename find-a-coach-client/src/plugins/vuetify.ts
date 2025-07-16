import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import { md3 } from 'vuetify/blueprints'

export default createVuetify({
  components,
  directives,
  blueprint: md3,
  theme: {
    defaultTheme: 'light',
  },
})