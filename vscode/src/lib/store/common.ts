import { glib } from '~/lib/glib'

export const loader = async (url:string,post:any,h:Function) => {
  post.session = runtime.session
  let rep = await glib.post(url, post)
  if(rep?.Er == 'OK')
    h(rep)
}
