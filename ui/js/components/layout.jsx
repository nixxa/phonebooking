import React from 'react'
import { withStyles } from '@material-ui/core/styles'

import Header from './header'

const styles = theme => ({
    root: {
        flexGrow: 1,
        zIndex: 1,
        overflow: 'hidden',
        position: 'relative',
        display: 'flex'
    },
    content: {
        flexGrow: 1,
        backgroundColor: theme.palette.background.default,
        padding: theme.spacing.unit * 3,
        minWidth: 0
    },
    toolbar: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'flex-end',
        padding: '0 8px',
        ...theme.mixins.toolbar
    }
})

const Layout = props => {
    const { classes } = props
    return (
        <div className={classes.root}>
            <Header />
            <main className={classes.content}>
                <div className={classes.toolbar} />
                { props.children }
            </main>
        </div>
    )
}

export default withStyles(styles, { withTheme: true })(Layout)
