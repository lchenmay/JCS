import * as Graphics from '~/lib/util/graphics'
import * as GraphicsImpl from '~/lib/util/graphicsH5'
import * as types from '~/lib/bizLogics/types'
import { drawLine, fillRect } from '../util/graphicsPixi'

export const langs = ['en','zh']

const data = {
    en: {
        'Language':'Language',
        'Expand':'Expand',
        'Collapse':'Collapse',
        'New':'New',
        'Create':'Create',
        'Edit':'Edit',
        'Remove':'Remove',
        'Caption':'Caption',
        'Summary':'Summary',
        'Poster':'Poster'
    },
    zh: {
        'Language':'语言',
        'Expand':'展开',
        'Collapse':'收起',
        'New':'新建',
        'Create':'创建',
        'Edit':'修改',
        'Remove':'删除',
        'Caption':'标题',
        'Summary':'摘要',
        'Poster':'海报'
    }
}

export const translate = (lang:string) => (src:string) => {
  let items = data.en as {[key:string]: string}
  switch(lang){
    case 'zh':
      items = data.zh as {[key:string]: string}
      break
  }

  return items[src]
}
