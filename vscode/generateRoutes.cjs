const fs = require('fs');
const path = require('path');

function parseURLPath(path) {

  if (path === '/') {
    return [];
  }
  const matches = path.match(/\/([^/]+)/g);
  if (matches) {
    const result = matches.map(match => match.replace('/', ''));
    return result;
  }
  return [];
}

function setupMetaInfo(pro) {
  let resultMeta = {
    layout:"jcs"
  }
  return resultMeta
}

async function generateRoutes(rootDir) {
  const routes = [];

  function traverseDirectory(currentDir, parentPath = '') {
    const files = fs.readdirSync(currentDir);
    files.forEach((file) => {
      const filePath = path.join(currentDir, file);
      const fileStat = fs.statSync(filePath);

      if (fileStat.isDirectory()) {
        const directoryPath = path.join(parentPath, file);
        traverseDirectory(filePath, directoryPath);
      } else if (file.endsWith('.vue')) {
        const componentName = path.basename(file, '.vue').split('-').pop();
        const filepath = `/${parentPath}/${componentName}`.replace(/\\/g, '/')
        const pathArray = parseURLPath(filepath)
        // if(!(pathArray.length == 2 && pathArray.slice(-1)[0] == 'index' && (pathArray[0] == 'news' || pathArray[0] == 'otc' ||pathArray[0] == 'ex' ||pathArray[0] == 'rol' ||pathArray[0] == 'nm'))){
          let route = {
            path: undefined,
            name: undefined,
            component: undefined,
            meta:  undefined,
            props: true,
          }
          route.path = `/${parentPath}/${componentName}`.replace(/\\/g, '/')
          if((pathArray[0] == 'news' || pathArray[0] == 'otc' ||pathArray[0] == 'ex' || ||pathArray[0] == 'nm') &&  pathArray.length != 2){
            route.path = `/${parentPath}/${componentName}`.replace(/\\/g, '/').replace(/^\/(news|otc|ex|nm)/, '')
          }
          if(pathArray.slice(-1)[0] == 'index' && pathArray.length != 1){
            route.path = route.path.replace(/\/index$/, '')
          }
          route.component = `import("~/pages/${parentPath}/${componentName}.vue")`.replace(/\\/g, '/')
          route.name = route.path.replace(/\//g, '-').replace(/^-|-$/g, ''); // 提取文件名最后一个 - 后的部分作为组件名
          route.meta = setupMetaInfo(pathArray[0]);

          const fileName = path.basename(file, '.vue');

          ['id','type','all'].forEach(pattern => {
            if(fileName.includes("[" + pattern + "]")){
              route.name = route.name.replace('/\[{' + pattern + '}]/', pattern);
              route.path = route.path.replace('/\/\[{' + pattern + '}]/', '/:' + pattern);
            }
          })

          routes.push(route);
        // }
      }
    });
  }

  // Add error.vue for handling route navigation failures
  traverseDirectory(rootDir);
  const errorPath = path.join(rootDir, '/error.vue');
  if (fs.existsSync(errorPath)) {
    const errorRoute = {
      path: '/:catchAll(.*)',
      name: 'error',
      component: 'import("~/pages/error.vue")'.replace(/\\/g, '/'),
      meta: {},
      props: true,
    };
    errorRoute.meta = { layout:"blank" };
    routes.push(errorRoute);
  }

  return routes;
}

// 获取当前文件的目录
const baseDir = path.dirname(__filename);

const pagesDir = path.join(baseDir, 'src/pages');

generateRoutes(pagesDir).then((generatedRoutes) => {
  const routesExport = `import { RouteRecordRaw } from 'vue-router';\n\n`;

  const routesDeclaration = `
  const routes: Array<RouteRecordRaw> = [
    ${generatedRoutes.map(
      (route) =>{
        const layoutPath = route.meta.layout?`()=>import ( "/src/layouts/${route.meta.layout}.vue" )`:`()=>import ( "/src/layouts/blank.vue" )`

        return  `{
      path: '${route.path}',
      component: ${layoutPath},
      meta: ${JSON.stringify(route.meta)},
      children: [{
        path: "",
        name: '${route.name}',
        component:()=> ${route.component},
        meta: ${JSON.stringify(route.meta)},
        props: ${JSON.stringify(route.props)}
      }]
    }`
      }
    ).join(',\n    ')}
  ];

  export default routes;
  `;

  const outputFilePath = path.join(baseDir, 'src/generatedRoutes.ts');
  fs.writeFileSync(outputFilePath, routesExport + routesDeclaration, 'utf-8');
});
