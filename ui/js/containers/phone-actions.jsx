import React from 'react'
import PropTypes from 'prop-types'
import { IconButton, Icon, Dialog, DialogActions, DialogContent,
    DialogContentText, DialogTitle, TextField, Button } from '@material-ui/core'

class PhoneActions extends React.Component {
    static propTypes = {
        booked: PropTypes.bool.isRequired,
        model: PropTypes.string.isRequired,
        bookPhone: PropTypes.func.isRequired,
        releasePhone: PropTypes.func.isRequired
    }

    state = {
        dlgOpen: false,
        email: ''
    }

    handleDlgOpen = () => {
        if (!this.props.booked) {
            this.setState({ dlgOpen: true })
        } else {
            this.props.releasePhone(this.props.model)
        }
    }

    handleDlgClose = () => {
        this.setState({
            dlgOpen: false,
            email: ''
        })
    }

    handleBookPhone = () => {
        this.setState({ dlgOpen: false, email: '' })
        this.props.bookPhone(this.props.model, this.state.email)
    }

    handleEmailChange = (evt) => {
        this.setState({
            email: evt.target.value
        })
    }

    render () {
        const { booked } = this.props
        return (
            <div>
                <IconButton onClick={this.handleDlgOpen}>
                    <Icon>{ booked ? 'lock_open' : 'lock' }</Icon>
                </IconButton>
                <Dialog
                    open={this.state.dlgOpen}
                    onClose={this.handleDlgClose}
                    aria-labelledby="form-dialog-title">
                    <DialogTitle id="form-dialog-title">Subscribe</DialogTitle>
                    <DialogContent>
                        <DialogContentText>
                            To book the phone for test purpose, please enter your email address here.
                        </DialogContentText>
                        <TextField
                            autoFocus
                            margin="dense"
                            id="email"
                            label="Email Address"
                            type="email"
                            value={this.state.email}
                            onChange={this.handleEmailChange}
                            fullWidth />
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={this.handleDlgClose} color="primary">Cancel</Button>
                        <Button onClick={this.handleBookPhone} color="primary">Book</Button>
                    </DialogActions>
                </Dialog>
            </div>
        )
    }
}

export default PhoneActions
