import { defineComponent } from 'vue'

const _rt = rtxx

export const compTitle = defineComponent({
  name: 'compTitle',
  props: {
    msg: String
  },
  setup(props) {
    return () => (
      <div>
        <h1>host.WSurl: {_rt.host.wsurl}</h1>
        <h1>props.msg: {props.msg}</h1>
        <h2>{_rt.msgList.length > 0 ? _rt.msgList[0].msg : ""}</h2>
      </div>
    )
  }
})

export const CreateOgCard = (clink: gchain.CLINK) => {
  return defineComponent({
    name: 'CreateOgCard',
    inheritAttrs: false,
    render() {
      return (
        <div>
          <div v-if={clink.id >= 0}>
            <div class="flex flex-col self-center w-[600px] p-[20px] shadow-2xl rounded-lg">
             <a href={`/t/${clink.p.HashTiny}`}>{ clink.p.HashTiny }</a>
              <div v-if={clink.p.OgTitle}>
                <a class="text-[#336699] font-semibold" href={clink.p.Src}>{clink.p.OgTitle}</a>
              </div>

              <div v-if={clink.p.OgDesc}>
                {clink.p.OgDesc}
              </div>
              <div class="mt-[20px]" v-if={clink.p.OgImg}>
                <a href={clink.p.Src}> {clink.p.Src}  <img class="w-[560px]" src={clink.p.OgImg} /></a>       </div>

              <div class="my-[20px]" v-if={clink.p.Data}>
                {clink.p.Data}
              </div>
              <div v-if="clink.p.DomainName">
                (This site has been identified as one of our business partner.
                When you share the generator link,
                the effectiveness of your promotional efforts will be tracked and any owed compensation will be provided to our
                affiliate partner.)
                <div>(partner info content place holder)</div>
              </div>
            </div>
          </div>
        </div>
      )
    }
  })
}