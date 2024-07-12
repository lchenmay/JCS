import * as vue from 'vue'

import * as check from '~/lib/util/check'
import * as ws from '~/lib/util/ws'
import * as bin from '~/lib/util/bin'
import * as markdown from '~/lib/util/markdown'
import * as graphics from '~/lib/util/graphics'
import * as fetchs from '~/lib/util/fetch'
import * as misc from '~/lib/util/misc'

import * as auth from '~/lib/mod/auth'
import * as notify from '~/lib/mod/notify'
import * as route from '~/lib/mod/route'
import * as panel from '~/lib/mod/panel'

import * as runtime from '~/lib/store/runtime'

import * as cm_gchain from '~/lib/shared/gchain/CustomMor'
import * as om_gchain from '~/lib/shared/gchain/OrmMor'

import * as cm_ctc from '~/lib/shared/ctc/CustomMor'
import * as om_ctc from '~/lib/shared/ctc/OrmMor'

export const glib = {
  vue: vue,

  check: check,
  ws: ws,
  bin: bin,
  markdown: markdown,
  g: graphics,
  misc:misc,

  auth: auth,
  notify: notify,
  route: route,
  panel: panel,

  runtime: runtime,

  Mor: {
    gchain: { ...cm_gchain, ...om_gchain },
    ctc: { ...cm_ctc, ...om_ctc },
  },

  post: fetchs.post,
  get: fetchs.get,
  send: ws.trySend,

  setRT: runtime.setRT,
  getRT: runtime.getRT,
  sleep: misc.sleep,

}
