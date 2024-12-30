export const decode = (src: string) => {
    let s = src.replace(/\\n/g,'\n')
    s = s.replace(/\\r/g,'\r')
    s = s.replace(/\\\\/g,'\\')
    return s
  }