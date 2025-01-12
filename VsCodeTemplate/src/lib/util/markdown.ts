
const escape_html = (html: string) => {
  return html.replace(/</g, '&lt;').replace(/>/g, '&gt;')
}

const check_url = (url: string) => {
  const regex = /^(http(s):\/\/.)[-a-zA-Z0-9@:%._+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_+.~#?&//=]*)$/i
  return regex.test(url) ? url : ''
}

const parse_md = (str: string) => {

  const bold = str.match(/\*{2}[^*].*?\*{2}/g)  // 惰性匹配
  if (bold) {
    for (let i = 0, len = bold.length; i < len; i++) {
      str = str.replace(bold[i], `<strong>${bold[i].substring(2, bold[i].length - 2)}</strong>`)
    }
  }

  const italic = str.match(/\*[^*].*?\*/g)
  if (italic) {
    for (let i = 0, len = italic.length; i < len; i++) {
      str = str.replace(italic[i], `<em>${italic[i].substring(1, italic[i].length - 1)}</em>`)
    }
  }

  const code = str.match(/`[^`]*`/g)
  if (code) {
    for (let i = 0, len = code.length; i < len; i++) {
      str = str.replace(code[i], `<code>${code[i].substring(1, code[i].length - 1)} </code>`)
    }
  }

  const url_regex = /\(.*\)/
  const title_regex = /\[.*\]/

  const img = str.match(/!\[.*\]\(.*\)/g)
  if (img) {
    for (let i = 0, len = img.length; i < len; i++) {
      let url = (img[i].match(url_regex)?.[0]) || ''
      let title = (img[i].match(title_regex)?.[0]) || ''
      title = escape_html(title.substring(1, title.length - 1))
      str = str.replace(img[i], `<img src="${url}" alt="${title}">`)
    }
  }

  const a = str.match(/\[.*?\]\(.*?\)/g)
  if (a) {
    for (let i = 0, len = a.length; i < len; i++) {
      let url = (a[i].match(url_regex)?.[0]) || ''
      let title = (a[i].match(title_regex)?.[0]) || ''
      url = url.substring(1, url.length - 1)
      title = escape_html(title.substring(1, title.length - 1))
      str = str.replace(a[i], `<a href="${url}" title="${title}">${title}</a>`)
    }
  }

  const tag = str.match(/\[\[(.*?)\]\]/g)
  if (tag) {
    for (let i = 0, len = tag.length; i < len; i++) {
      const tagPart = tag[i].substring(2, tag[i].length - 2).split(':')
      const word = tagPart.length === 2 ? tagPart[1] : tagPart[0]
      const lang = tagPart.length === 2 ? tagPart[0] : 'zh'
      str = str.replace(tag[i], `<a href="/community/${lang}:${word}" title="${word}" class="tag">${word}</a>`)
    }
  }

  const youtube = str.match(/^(https):\/\/?((www\.)?youtube\.com|youtu\.be)\/.+$/gi)
  if (youtube) {
    for (let i = 0, len = youtube.length; i < len; i++) {
      let youtubeEmbedUrl = youtube[i].replace('https://www.youtube.com/watch?v=', 'https://www.youtube.com/')
        .replace('https://youtube.com/shorts/', 'https://www.youtube.com/')
        .replace('https://www.youtube.com/', 'https://www.youtube.com/embed/')
        .replace('https://youtu.be/', 'https://www.youtube.com/embed/')
      if (youtube[i].includes('list')) youtubeEmbedUrl = youtube[i].replace('https://www.youtube.com/', 'https://www.youtube.com/embed/')
      str = str.replace(youtube[i],
        `<iframe src="${youtubeEmbedUrl}" frameborder="0" title="YouTube video player"
            allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen class="youtube-video"></iframe>`)
    }
  }

  const twitter = str.match(/^((https?):\/\/)?(www.)?(twitter|x)\.com(\/@?(\w){1,15})\/status\/[0-9]{19}(\?.+)?$/gi)
  if (twitter) {
    for (let i = 0, len = twitter.length; i < len; i++) {
      const twitterUrl = twitter[i].replace('x.com', 'twitter.com')
      str = str.replace(twitter[i], `<blockquote class="twitter-tweet"><a href="${twitterUrl}"></a></blockquote>`)
    }
  }

  const spotify = str.match(/^(https):\/\/?(open\.spotify\.com)\/(playlist|track|album)\/.+$/gi)
  if (spotify) {
    for (let i = 0, len = spotify.length; i < len; i++) {
      const spotifyEmbedUrl = spotify[i].replace('track', 'embed/track').replace('album', 'embed/album').replace('playlist', 'embed/playlist').split('?')[0] + '?utm_source=generator&theme=0'
      const isPlayList = spotifyEmbedUrl.includes('playlist') || spotifyEmbedUrl.includes('album')
      str = str.replace(spotify[i],
        `<iframe src="${spotifyEmbedUrl}" frameborder="0" allow="autoplay; clipboard-write; encrypted-media; fullscreen; picture-in-picture"
            allowfullscreen loading="lazy" class="spotify-embed" height="${isPlayList ? '380px' : '352px'}"></iframe>`)
    }
  }

  const itunes = str.match(/^(https):\/\/?(music\.apple\.com)\/[a-z]{2}\/(playlist|album)\/.+$/gi)
  if (itunes) {
    for (let i = 0, len = itunes.length; i < len; i++) {
      const itunesEmbedUrl = itunes[i].replace('music.apple.com', 'embed.music.apple.com')
      const isPlayList = itunesEmbedUrl.includes('playlist') || itunesEmbedUrl.includes('album')
      str = str.replace(itunes[i],
        `<iframe src="${itunesEmbedUrl}" frameborder="0" allow="autoplay *; encrypted-media *; fullscreen *; clipboard-write"
            sandbox="allow-forms allow-popups allow-same-origin allow-scripts allow-storage-access-by-user-activation allow-top-navigation-by-user-activation"
            class="itunes-embed" height="${isPlayList ? '450px' : '175px'}"></iframe>`)
    }
  }

  str = str.replace(/\\\$/g, "$") // md语法(\$..\$)显示为普通文字，不渲染成katex

  return str
}

const compile_md = (p: string[]) => {
  let matchArr
  let html = ''

  for (let i = 0, len = p.length; i < len; i++) {
    const text = p[i]
    const index = i

    matchArr = text.match(/^#\s/) ||           // Matches single # followed by a space
      text.match(/^##\s/) ||              // Matches ## followed by a space
      text.match(/^###\s/) ||             // Matches ### followed by a space
      text.match(/^####\s/) ||            // Matches #### followed by a space
      text.match(/^#####\s/) ||           // Matches ##### followed by a space
      text.match(/^######\s/) ||          // Matches ###### followed by a space
      text.match(/^\*{3,}/) ||            // Matches 3 or more asterisks (*)
      text.match(/^-{3,}/) ||             // Matches 3 or more hyphens (-)
      text.match(/^>\s/) ||               // Matches > followed by a space
      text.match(/^\*\s/) ||              // Matches * followed by a space
      text.match(/^\d*\.\s/) ||           // Matches optional digits followed by a dot and a space
      text.match(/^```/) ||               // Matches ```
      text.match(/^\|.*\|/) ||            // Matches |...|
      text.match(/^\$\$/) ||              // Matches $$
      text.match(/(^|[^\\])\$(?![\\$])([^$\\]*[^\\$ ])\$/);  // Matches $...$ (excluding escaped $)

    if (matchArr) {
      let temp = ''
      const re1 = /^>/
      const re2 = /^\*\s/
      const re3 = /^\d*\.\s/
      const re4 = /^```/
      const re5 = /^\|.*\|/
      const re6 = /^\$\$/
      const re7 = /\$(?![^$]*\s\$)([^$]*)\$/g

      switch (matchArr[0]) {
        case '# ':
          html += `<h1 id="link-${index}">${parse_md(text.substring(2))}</h1>`
          break
        case '## ':
          html += `<h2 id="link-${index}">${parse_md(text.substring(3))}</h2>`
          break
        case '### ':
          html += `<h3 id="link-${index}">${parse_md(text.substring(4))}</h3>`
          break
        case '#### ':
          html += `<h4 id="link-${index}">${parse_md(text.substring(5))}</h4>`
          break
        case '##### ':
          html += `<h5 id="link-${index}">${parse_md(text.substring(6))}</h5>`
          break
        case '###### ':
          html += `<h6 id="link-${index}">${parse_md(text.substring(7))}</h6>`
          break
        case text.match(/^\*{3,}/) && (text.match(/^\*{3,}/)?.[0]): // ***
          html += text.replace(/^\*{3,}/g, '<hr />')
          break
        case text.match(/^-{3,}/) && (text.match(/^-{3,}/)?.[0]): // ---
          html += text.replace(/^-{3,}/g, '<hr />')
          break
        case '> ':
          while (i < len && p[i].match(re1)) {
            const str = p[i]
            temp += parse_md(str.slice(1))
            temp += `<br />`
            i++
          }
          i--
          html += `<blockquote>${temp}</blockquote>`
          break
        case '* ':
          while (i < len && p[i].match(re2)) {
            const str = p[i]
            temp += `<li>${parse_md(str.slice(2))}</li>`
            i++
          }
          i--
          html += `<ul>${temp}</ul>`
          break
        case text.match(/^\d*\.\s/) && (text.match(/^\d*\.\s/)?.[0]):
          while (i < len && p[i].match(re3)) {
            const str = p[i]
            temp += `<li>${parse_md(str)}</li>`
            i++
          }
          i--
          html += `<ol>${temp}</ol>`
          break
        case '```':
          i++
          while (i < len && !re4.test(p[i])) {
            temp += escape_html(p[i])
            temp += '\n'
            i++
          }

          html += `<pre><code>${temp}</code></pre>`
          break
        case text.match(/^\|.*\|/) && (text.match(/^\|.*\|/)?.[0]): {
          const th_regex = /^\[th\]/
          let arr, j, jlen
          while (i < len && re5.test(p[i])) {
            arr = p[i].split('|')
            temp += `<tr>`
            for (j = 1, jlen = arr.length - 1; j < jlen; j++) {
              if (th_regex.test(arr[1])) {
                temp += `<th>${parse_md(arr[j])}</th>`
              } else {
                temp += `<td>${parse_md(arr[j])}</td>`
              }
            }
            temp += '</tr>'
            i++
          }

          html += `<table>${temp}</table>`
          break
        }
        case '$$':
           i++
           while (i < len && !re6.test(p[i])) {
             temp += escape_html(p[i])
             i++
        }

        //   html += `<p>${katex.renderToString(temp, { throwOnError: false })}</p>`
        //   break
        // case text.match(/(^|[^\\])\$(?![\\$])([^$\\]*[^\\$ ])\$/) && (text.match(/(^|[^\\])\$(?![\\$])([^$\\]*[^\\$ ])\$/)?.[0]): {
        //   let tex = p[i]
        //   const arr = p[i].match(re7) || []
        //   arr.forEach((match: any) => {
        //     tex = tex.replace(match, katex.renderToString(match.substring(1, match.length - 1), { throwOnError: false }))
        //   })

        //   html += `<p>${tex}</p>`
        //   break
        // }
      }
    } else if (text) {
      html += `<p>${parse_md(escape_html(text))}</p>`
    } else {
      //html += '<br />'
    }
  }

  return html
}

export const markdown__html = (str: string) => {

  let res = ""

  let lines = str.split(/\r?\n/)
  let state = ""
  let buffer = []
  for (let i = 0, len = lines.length; i < len; i++) {
    let line = lines[i]
    let html = ""

    if(state == "" && line.startsWith("$$")){
      state = "$$"
      continue
    }
    else if(state == "$$" && line.startsWith("$$")){

      let s = buffer.join("")

      console.log(s)

      let tex = encodeURI(s)

      console.log(tex)

      let alt = s.replaceAll('"'," ").replaceAll("'"," ").replaceAll('<'," ").replaceAll(">"," ")
      let img = 
        "<img class='img-inline' src='http://latex.codecogs.com/gif.latex?" + tex +"' alt='" + alt + "'>"
      let p = "<p>" + img + "&nbsp;</p>"

      res += p

      buffer = []
      state = ""
      continue
    }
    else if(state == "$$"){
      buffer.push(line)
      continue
    }

    line.matchAll(/\$.+?\$/g).forEach((item) => {
        let s = item + ""
        let tex = encodeURI(s.substring(1,s.length - 1))
        let alt = s.substring(1,s.length - 1).replaceAll('"'," ").replaceAll("'"," ").replaceAll('<'," ").replaceAll(">"," ")
        let img = 
          "<img class='img-inline' src='http://latex.codecogs.com/gif.latex?" + tex +"' alt='" + alt + "'>"
        line = line.replace(s,img)        
    })

    line.matchAll(/\!\[.*?\]\(.+?\)/g).forEach((item) => {
      let s = item + ""
      let txt = s.match(/\[.+?\]/) + ""
      if(txt.length >= 2)
        txt = txt.substring(1,txt.length - 1)
      let src = s.match(/\(.+?\)/) + ""
      if(src.length >= 2)
        src = src.substring(1,src.length - 1)
      let media = "<br><img src='" + src + "' alt='" + txt + "'><br>"
      if(src.endsWith(".mp3"))
        media = "<br><audio controls><source src='" + src + "' type='audio/mp3'></audio><br>" 
      line = line.replace(s,media)        
    })

    line.matchAll(/\[.+?\]\(.+?\)/g).forEach((item) => {
      let s = item + ""
      let txt = s.match(/\[.+?\]/) + ""
      if(txt.length >= 2)
        txt = txt.substring(1,txt.length - 1)
      let src = s.match(/\(.+?\)/) + ""
      if(src.length >= 2)
        src = src.substring(1,src.length - 1)
      let a = "<a href='" + src + "' target='_blank'>" + txt + "</a>"
      line = line.replace(s,a)        
    })
        
    line.matchAll(/\*\*.*?\*\*/g).forEach((item) => {
      let s = item + ""
      let txt = s.substring(2,s.length - 2)
      let b = "<b>" + txt +"</b>"
      line = line.replace(s,b)        
    })
  
    if(line.startsWith("---"))
      html = "<hr>"
    else if(line.startsWith("##### "))
      html = "<div class='caption-5'>" + line.substring(6,line.length) + "</div>"
    else if(line.startsWith("#### "))
      html = "<div class='caption-4'>" + line.substring(5,line.length) + "</div>"
    else if(line.startsWith("### "))
      html = "<div class='caption-3'>" + line.substring(4,line.length) + "</div>"
    else if(line.startsWith("## "))
        html = "<div class='caption-2'>" + line.substring(3,line.length) + "</div>"
    else if(line.startsWith("# "))
      html = "<div class='caption-1'>" + line.substring(2,line.length) + "</div>"
    else
      html = "<p>" + line + "&nbsp;</p>"

    res += html
  }

  return res
}

export const markdown__html_ = (str: string) => {
  if (!str) str=""
  try {
    const p: any[] = str.split(/\r?\n/)
    return compile_md(p)
  }
  catch {
    compile_md([str])
  }
  return ""

}

// Table of Contents
export const html__toc = (html: string) => {
  const parser = new DOMParser();
  const doc = parser.parseFromString(html, 'text/html');
  const headings = doc.querySelectorAll('h1, h2, h3, h4, h5, h6');
  const counter: number[] = [0];
  let tocHtml: string = '';

  headings.forEach((heading: any) => {
    const level: number = parseInt(heading.tagName.charAt(1));
    const text: string = heading.textContent!;
    const id: string = heading.id;

    // Update counter for current level
    while (counter.length < level) {
      counter.push(0);
    }
    counter[level - 1]++;

    // Reset counter for lower levels
    for (let i = level; i < counter.length; i++) {
      counter[i] = 0;
    }

    // Construct numbering
    const numbering: string = counter.slice(0, level).join('.');
    const listItem: string = `<li><a href="#${id}">${numbering} ${text}</a></li>`;

    // Add to TOC
    tocHtml += listItem;
  });

  return tocHtml;
}
