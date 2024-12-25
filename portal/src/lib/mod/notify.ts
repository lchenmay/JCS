import { glib } from '~/lib/glib'

export const add = (msg: string) => {
    msg_add({ msg: msg })
}
export const aSuc = (msg: string, exp = 400) => {
    msg_add({ msg: msg, expire: exp, bgcolor: "bg-green-500/80" })
}
export const aFail = (msg: string, exp = 1500) => {
    msg_add({ msg: msg, expire: exp, bgcolor: "bg-red-500/80" })
}
export const aEx = (msg: string, exp = 1500) => {
    msg_add({ msg: msg, expire: exp, bgcolor: "bg-orange-500/80" })
}


export const msg_add = (item: NotifyItem) => {
    if (!item.msg) item.msg = `${Date.now()}MSG`
    item.cdate = Date.now()
    if (!item.expire) item.expire = 3000
    if (!item.bgcolor) item.bgcolor = "bg-gray-500/70"
    item.expire = Date.now() + item.expire
    //rt.msgList.unshift(item)
    Refresh()
}

export const DefaultItem = (): NotifyItem => {
    return {
        msg: `TEST ${Date.now()}`,
    }
}

export const getcurMsg = () => {
    //if (rt.msgList.length) return rt.msgList[0]
    return DefaultItem()
}

export const getTimeleft = (item: NotifyItem) => { return item.expire! - Date.now() }

export const removeExpiredMessages = () => {
    //rt.msgList = rt.msgList.filter((item:any) => { return getTimeleft(item) > 0 });
}

export const Refresh = () => {
    removeExpiredMessages()
}


export const init = () => {
    setInterval(Refresh, 500)
}

export const onADDClick = () => {
    msg_add(DefaultItem())
}
