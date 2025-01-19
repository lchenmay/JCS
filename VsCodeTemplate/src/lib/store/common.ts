import { glib } from '~/lib/glib'
import { EuComplex_empty } from '../shared/CustomMor'

export const loader = async (url:string,post:any,h:Function) => {
  post.session = runtime.session
  post.lang = runtime.lang
  let rep = await glib.post(url, post)
  if(rep?.Er == 'OK')
    h(rep)

  runtime.session = rep.session
  localStorage.setItem("runtime.session",runtime.session)
  if(rep.session == "")
    runtime.user = EuComplex_empty()

}
