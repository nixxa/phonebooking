import React from 'react'
import { withStyles } from '@material-ui/core/styles'
import { Typography, Table, TableBody, TableHead, TableCell, TableRow,
    Divider, Paper } from '@material-ui/core'

import Layout from '../components/layout'
import withErrorHandling from '../components/with-error-handling'
import PhoneActions from '../containers/phone-actions'
import API from '../api'

const styles = theme => ({
    root: {
        width: '100%',
        marginTop: theme.spacing.unit * 3,
        overflowX: 'auto'
    },
    table: {
        minWidth: 700
    },
    iconButton: {
        marginRight: theme.spacing.unit
    },
    button: {
        marginTop: theme.spacing.unit
    },
    loading: {
        margin: theme.spacing.unit * 3
    }
})

class PhonesPage extends React.Component {
    state = {
        rows: null,
        model: null
    }

    async componentDidMount () {
        try {
            const phones = await API.getPhones()
            this.setState({ rows: phones })
        } catch (error) {
            if (this.props.errorCallback) {
                this.props.errorCallback(error)
            }
        }
    }

    handleBookPhone = async (model, email) => {
        try {
            const phones = await API.bookPhone(model, email)
            this.setState({ rows: phones })
        } catch (error) {
            if (this.props.errorCallback) {
                this.props.errorCallback(error)
            }
        }
    }

    handleReleasePhone = async model => {
        try {
            const phones = await API.releasePhone(model)
            this.setState({ rows: phones })
        } catch (error) {
            if (this.props.errorCallback) {
                this.props.errorCallback(error)
            }
        }
    }

    renderTable (rows) {
        const { classes } = this.props
        return (
            <Paper className={classes.root}>
                <Table className={classes.table}>
                    <TableHead>
                        <TableRow>
                            <TableCell>Model Name</TableCell>
                            <TableCell>Technology / Bands</TableCell>
                            <TableCell>Booked At</TableCell>
                            <TableCell>Booked By</TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {
                            rows.map((phone, index) => {
                                return (
                                    <TableRow key={index}>
                                        <TableCell><Typography>{ phone.model }</Typography></TableCell>
                                        <TableCell>
                                            <Typography noWrap paragraph><Typography variant="body2">Technology:</Typography> { phone.tech }</Typography>
                                            <Typography noWrap paragraph><Typography variant="body2">2G:</Typography> {phone.bands.g2}</Typography>
                                            <Typography noWrap paragraph><Typography variant="body2">3G:</Typography> { phone.bands.g3 }</Typography>
                                            <Typography><Typography variant="body2">4G:</Typography> { phone.bands.g4 }</Typography>
                                        </TableCell>
                                        <TableCell>
                                            <Typography>
                                                {
                                                    phone.bookedAt &&
                                                    new Date(Date.parse(phone.bookedAt)).toLocaleString()
                                                }
                                            </Typography>
                                        </TableCell>
                                        <TableCell><Typography>{ phone.bookedBy }</Typography></TableCell>
                                        <TableCell>
                                            <PhoneActions
                                                booked={phone.bookedAt !== null}
                                                model={phone.model}
                                                bookPhone={this.handleBookPhone}
                                                releasePhone={this.handleReleasePhone} />
                                        </TableCell>
                                    </TableRow>
                                )
                            })
                        }
                    </TableBody>
                </Table>
            </Paper>
        )
    }

    render () {
        const { rows } = this.state
        const { classes } = this.props
        return (
            <Layout>
                <Typography variant="headline" gutterBottom>
                    Available Phones
                </Typography>
                <Divider />
                {
                    !rows &&
                    <Typography className={classes.loading}>Loading...</Typography>
                }
                {
                    rows && this.renderTable(rows)
                }
            </Layout>
        )
    }
}

export default withStyles(styles, { withTheme: true })(withErrorHandling(PhonesPage))
