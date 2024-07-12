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
      rtxx['moment'] = {id:1234,Caption:"ABCD"}
      break
  }

  const getRandomNumber = (max=5000) => {
    return Math.floor(Math.random() * max)
  }

  /// Example of insert data to Runtime.
  console.log("fake data write to bizList...")

  const msgbiz:BIZ = {
    id:getRandomNumber(),
    createdat:new Date,
    updatedat:new Date,
    sort:getRandomNumber(),
    p:{}as pBIZ

  }
  rtxx.bizList.push(msgbiz)

  /// end Example

  bi.index+=4
  const jsonstr=glib.Mor.bin__Msg(bi)
  console.log(jsonstr)


  const hex = bin.ArrayBuffer__hex(bi.bin)
  console.log(hex)
}