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

import * as host from '~/lib/store/host'
import * as runtime from '~/lib/store/runtime'

import * as cm from '~/lib/shared/CustomMor'
import * as om from '~/lib/shared/OrmMor'

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

  host: host,
  runtime: runtime,

  Mor: {
    []: { ...cm, ...om }
  },

  post: fetchs.post,
  get: fetchs.get,
  send: ws.trySend,

  setRT: runtime.setRT,
  getRT: runtime.getRT,
  sleep: misc.sleep,

}
