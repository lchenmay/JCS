declare global {


// [Stroke]
export type Stroke = {
points:any[],
strokeSize:number,
color:string
}

// [ActionWhiteboard]
export type ActionWhiteboard = {
e: number,
val: any
}

// [FactWhiteboard]
export type FactWhiteboard = {
action:ActionWhiteboard,
actor:string,
clientId:number,
serverId:number,
clientTimestamp:Date,
serverTimestamp:Date
}

// [FactBroadcast]
export type FactBroadcast = {
e: number,
val: any
}

}

export {}
