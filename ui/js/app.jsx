import React from 'react'
import { render } from 'react-dom'
import { HashRouter as Router, Route, Switch } from 'react-router-dom'

import PhonesPage from './pages/phones-page'

render(
    <Router>
        <Switch>
            <Route path="/" component={PhonesPage} exact />
        </Switch>
    </Router>,
    document.getElementById('app')
)
