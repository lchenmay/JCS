declare global {


// [Stroke]
export type Stroke = {
points:any[],
strokeSize:float32,
color:uint32
}

// [ActionWhiteboard]
export type ActionWhiteboard = {
enum: number,
val: any
}

// [FactWhiteboard]
export type FactWhiteboard = {
action:ActionWhiteboard,
actor:string,
clientId:number,
serverId:number,
clientTimestamp:DateTime,
serverTimestamp:DateTime
}

// [FactBroadcast]
export type FactBroadcast = {
enum: number,
val: any
}

}

export {}
