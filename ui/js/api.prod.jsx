const apiUrl = `http://${window.location.hostname}:5000/api`

const handleErrors = resp => {
    if (!resp.ok) {
        resp.json().then(r => console.log(r))
        throw Error(resp.statusText)
    } else {
        return resp
    }
}

export const API = {
    getPhones: () => {
        return fetch(`${apiUrl}/phones/all`).then(handleErrors).then(response => response.json())
    },
    bookPhone: (model, person) => {
        return fetch(`${apiUrl}/phones/book`, {
            method: 'POST',
            mode: 'cors',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            body: JSON.stringify({ model, email: person })
        })
            .then(handleErrors)
            .then(response => response.json())
    },
    releasePhone: model => {
        return fetch(`${apiUrl}/phones/release`, {
            method: 'POST',
            mode: 'cors',
            cache: 'no-cache',
            headers: {
                'Content-Type': 'application/json; charset=utf-8'
            },
            body: JSON.stringify({ model })
        })
            .then(handleErrors)
            .then(response => response.json())
    }
}
