### Desc
Vue + Vite + TSX + Tailwindcss

### ENV Setup
1. Install ```bun``` from https://bun.sh/.
2. Install ```VS Code``` https://code.visualstudio.com/ or other IDEs.
3. Restart ```CMD``` Env to get the latest ```PATH```.
4. ```git clone https://github.com/abcxxx/fx.git```
5. in ```fx``` folder, run ```bun install```.
6. ```bun dev``` to run vite server at http://localhost:5173 or http://127.0.0.1:5173.
* To run ts script, run ```bun XXX.ts```

### Folders and Files
* /public: static files for html. 
* /src
    * /pcs: Components for Page.
        * See .vue file for ```Standard Vue Component```
        * See .tsx file for ```Functional Component```.
    * /pages: Folder based pageview route (generateRoutes.cjs). Routes will be auto generated when run ```bun dev``` or ```bun build```.
    * /layouts: Wrapper layout named as ```XXXX1.vue``` for pages named as ```pages/XXXX1/**.vue```.
    * /lib/glib.ts: index file which used to import all modules.
    * /lib/store: Variables with states. Runtime, etc.
    * /lib/mod: Biz Logic.