const phones = [
    {
        model: 'Samsung Galaxy S9',
        tech: 'GSM / CDMA / HSPA / EVDO / LTE',
        bands: {
            g2: 'GSM 850 / 900 / 1800 / 1900 - SIM 1 & SIM 2 (dual-SIM model only)',
            g3: 'HSDPA 850 / 900 / 1700(AWS) / 1900 / 2100 - Global, USA',
            g4: 'LTE band 1(2100), 2(1900), 3(1800), 4(1700/2100), 5(850), 7(2600), 8(900), 12(700), 13(700), 17(700), 18(800), 19(800), 20(800), 25(1900), 26(850), 28(700), 32(1500), 38(2600), 39(1900), 40(2300), 41(2500), 66(1700/2100) - Global'
        },
        bookedAt: null,
        bookedBy: null
    }
]

export const API = {
    getPhones: () => {
        return new Promise((resolve, reject) => {
            resolve(phones)
        })
    },
    bookPhone: (model, person) => {
        const row = phones.find(x => x.model === model)
        if (row) {
            row.bookedAt = new Date().toLocaleString()
            row.bookedBy = person
        }
        return new Promise((resolve, reject) => {
            resolve(phones)
        })
    },
    releasePhone: model => {
        const row = phones.find(x => x.model === model)
        if (row) {
            row.bookedAt = null
            row.bookedBy = null
        }
        return new Promise((resolve, reject) => {
            resolve(phones)
        })
    }
}
