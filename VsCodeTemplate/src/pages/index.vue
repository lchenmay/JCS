<template>

<button type="submit" @click="createws()">创建连接</button>
<button type="submit" @click="shutdown()">关闭连接</button>
<input type="text" v-model="text"/>
<button type="submit" @click="send()">发送消息</button>
<div>{{ msg }}</div>

<div>
  <img src="https://media.licdn.com/dms/image/v2/D5612AQHEYhhmnaZ7xA/article-cover_image-shrink_720_1280/article-cover_image-shrink_720_1280/0/1719879430792?e=1734566400&v=beta&t=wcJ42pgJO32u1z3Qo50UYOhCx7luhwCRN8XJ6WQwLjk" >
</div>

</template>
  
<script setup ts>
import { glib } from '~/lib/glib'

let ws = new WebSocket("ws://127.0.0.1:5045")
const msg = glib.vue.ref('Hello World!')
const text = glib.vue.ref('')

function createws(){
  ws = new WebSocket("ws://127.0.0.1:5045")
  
  ws.onopen = function(evt) { 
    console.log("Connection open ...");
  }

  ws.onmessage = function(evt) {
    console.log( "Received Message: " + evt.data)
    msg.value = "Received Message: " + evt.data
  }

  ws.onclose = function(evt) {
    console.log("Connection closed.")
  }

  ws.onerror = function(evt) {
    console.log( "Error Message: " + evt.data)
  }
}

function send(){
  console.log( "Send Message")
  ws.send(text.value)
}

function shutdown(){
  console.log("Press shutdown.")
  ws.close()
}

</script>
  