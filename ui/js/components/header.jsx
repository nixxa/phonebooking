import React from 'react'
import classNames from 'classnames'
import { withStyles } from '@material-ui/core/styles'
import { AppBar, Toolbar, Typography } from '@material-ui/core'

const styles = theme => ({
    appBar: {
        zIndex: theme.zIndex.drawer + 1,
        transition: theme.transitions.create(['width', 'margin'], {
            easing: theme.transitions.easing.sharp,
            duration: theme.transitions.duration.leavingScreen
        })
    },
    leftPadding: {
        paddingLeft: 2 * theme.spacing.unit
    },
    flex: {
        flex: 1
    }
})

const Header = props => {
    const { classes } = props
    return (
        <AppBar position="absolute"
            className={classes.appBar}>
            <Toolbar disableGutters={true}>
                <Typography variant="title" color="inherit" className={classNames(classes.flex, classes.leftPadding)}>
                    Phone Booking
                </Typography>
            </Toolbar>
        </AppBar>
    )
}

export default withStyles(styles, { withTheme: true })(Header)
