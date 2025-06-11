import { glib } from '~/lib/glib'

export const loader = async (url:string,post:any,h:Function) => {
  post.session = runtime.session
  let rep = await glib.post(url, post)
  if(rep?.Er == 'OK'){
    h(rep)
  }
}

export const callSync = async (url:string,post:any) => {
  post.session = runtime.session
  let rep = await glib.post(url, post)
  return rep
}

export const asyncPost = async (url:string,post:any) => {
  post.session = runtime.session
  let rep = glib.post(url, post)
  return rep
}

