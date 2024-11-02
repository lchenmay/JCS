import * as bin from "~/lib/util/bin";
import { BinIndexed } from "~/lib/util/bin";
import { glib } from '~/lib/glib'


export const wsjsonHandler = (msg: Object) => { }

export const wsbinHandler = async (msg: Blob) => {
    const bi: BinIndexed = { bin: await msg.arrayBuffer(), index: 0 }



    let headertype
    // 解析头部，进入switch。
    headertype = "moment"


    switch (true) {
        case headertype == 'moment':
            //runtime['moment'] = {id:1234,Caption:"ABCD"}
            break
    }

    const getRandomNumber = (max = 5000) => {
        return Math.floor(Math.random() * max)
    }

}