/** @type {import('tailwindcss').Config} */
module.exports = {
  darkMode: 'class',
  content: ['./index.html', './src/**/*.{vue,js,ts,jsx,tsx}'],
  theme: {
    screens: {
      sm: '600px',
      lg: '1024px',
      xl: '1280px',
    },
    fontFamily: { sans: ['-apple-system', 'Noto Sans SC', '微软雅黑', "Microsoft YaHei"] },
    extend: {
      fontFamily: {
        simhei: ["WenQuanYi Micro Hei",'SimHei','STHeiti'],
        noto: ["Noto Sans CJK", "Noto Sans", "Noto", "Noto Sans SC","Noto Sans TC","Noto Sans JP"],
        emoji: ['Noto Emoji','微软雅黑']
      },
      colors: {
        fx_black: '#202020',
        news_blackdark: '#F4F4F4',
      }
    }
  }
}
