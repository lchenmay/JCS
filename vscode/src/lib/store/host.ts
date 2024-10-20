

export const checkDomain = (domain:string) => {
    return [domain].includes(window.location.hostname)
}

export const initHost = () => {

    let portBackend = 5045

    const hostname = window.location.hostname

    const host: Host = {
        hostname: hostname,
        api: `${window.location.protocol}//${hostname}`,
        wsurl: `wss://${hostname}/ws/`,
        discordAPPID: "1254790111913181274",
        discordRedirect: `${window.location.protocol}//${window.location.host}/redirect/DISCORD`,
    }

    switch (host.hostname) {
        case 'localhost':
            host.hostname = 'localhost'
            host.api = 'http://localhost:' + portBackend
            host.wsurl = 'ws://localhost:' + portBackend
            break
        case '127.0.0.1':
            host.hostname = '127.0.0.1'
            host.api = 'http://localhost:' + portBackend
            host.wsurl = 'ws://localhost:' + portBackend
            break
    }

    return host
}
