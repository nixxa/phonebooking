import { API as devApi } from './api.dev'
import { API as prodApi } from './api.prod'

let API = devApi

if (process.env.NODE_ENV === 'production' || process.env.NODE_ENV === 'none') {
    API = prodApi
}

export default API
