// 创建一个 Proxy 以监听 showpanel 的变化
const handler = {
    set: (target, property, value) => {
        if (property === 'showpanel') {
            if (value) {
                renderPanel();
            } else {
                removePanel();
            }
        }
        target[property] = value;
        return true;
    }
};

globalThis.panelrt = new Proxy({showpanel: false}, handler);
const panelstates = globalThis.panelrt

// 函数：创建 <div> 元素
const createPanelDiv = () => {
    const panelDiv = document.createElement('div');
    panelDiv.id = 'panel';
    return panelDiv;
};

// 函数：设置样式
const setPanelStyle = (panelDiv) => {
    panelDiv.style.position = 'fixed';
    panelDiv.style.left = '50%';
    panelDiv.style.top = '50%';
    panelDiv.style.transform = 'translate(-50%, -50%)';
    panelDiv.style.width = '300px';
    panelDiv.style.height = '250px';
    panelDiv.style.backgroundColor = '#f0f0f0';
    panelDiv.style.boxShadow = '0 0 10px rgba(0, 0, 0, 0.5)';
    panelDiv.style.padding = '20px';
    panelDiv.style.boxSizing = 'border-box';
    panelDiv.style.zIndex = '1000';
};

// 函数：设置内容
const setPanelContent = (panelDiv) => {
    const closeButton = document.createElement('button');
    closeButton.innerHTML = 'Close';
    closeButton.style.position = 'absolute';
    closeButton.style.top = '10px';
    closeButton.style.right = '10px';
    closeButton.addEventListener('click', () => {
        panelstates.showpanel = false;
    });

    panelDiv.innerHTML = '<p>Powered by gha.in</p>';
    panelDiv.appendChild(closeButton);
};

// 函数：将面板添加到 body
const appendPanelToBody = (panelDiv) => {
    document.body.appendChild(panelDiv);
};

// 函数：初始化面板
const renderPanel = () => {
    if (!document.getElementById('panel')) {
        const panelDiv = createPanelDiv();
        setPanelStyle(panelDiv);
        setPanelContent(panelDiv);
        appendPanelToBody(panelDiv);
    }
};

// 函数：移除面板
const removePanel = () => {
    const panelDiv = document.getElementById('panel');
    if (panelDiv) {
        panelDiv.remove();
    }
};

const tglPanel = () => {
    panelstates.showpanel = !panelstates.showpanel
}


// 等待 DOM 完全加载后初始化
document.addEventListener('DOMContentLoaded', () => {
    panelstates.showpanel = false;
    if (panelstates.showpanel) {
        renderPanel();
    }

});