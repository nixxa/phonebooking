import React from 'react'
import { Snackbar } from '@material-ui/core'

const withErrorHandling = (Wrapped) => {
    return class extends React.Component {
        state = {
            error: null
        }

        onError = (error) => {
            this.setState({ error: error })
        }

        handleClose = () => {
            this.setState({ error: null })
        }

        render () {
            const { error } = this.state
            return (
                <div>
                    <Wrapped errorCallback={this.onError} {...this.props} />
                    <Snackbar
                        anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
                        open={error != null}
                        onClose={this.handleClose}
                        message={error} />
                </div>
            )
        }
    }
}

export default withErrorHandling
