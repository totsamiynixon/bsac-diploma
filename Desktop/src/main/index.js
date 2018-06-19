import {
  app,
  BrowserWindow,
  ipcMain,
  remote,
  Menu
} from 'electron'

/**
 * Set `__static` path to static files in production
 * https://simulatedgreg.gitbooks.io/electron-vue/content/en/using-static-assets.html
 */
if (process.env.NODE_ENV !== 'development') {
  global.__static = require('path').join(__dirname, '/static').replace(/\\/g,
    '\\\\')
}

let mainWindow;
const winURL = process.env.NODE_ENV === 'development' ?
  `http://localhost:9080` :
  `file://${__dirname}/index.html`

function createWindow() {
  /**
   * Initial window options
   */
  mainWindow = new BrowserWindow({
    height: 1000,
    useContentSize: false,
    width: 1600
  })

  mainWindow.loadURL(winURL);

  mainWindow.on('closed', () => {
    mainWindow = null
  })
}

app.on('ready', function() {
  createWindow();
  buildMenu();

})

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit()
  }
})

app.on('activate', () => {
  if (mainWindow === null) {
    createWindow()
  }
})
ipcMain.on('notify-user-about-training', (event, data) => {
  mainWindow.show();
  mainWindow.webContents.send("notify-user-about-training");
});

/**
 * Auto Updater
 *
 * Uncomment the following code below and install `electron-updater` to
 * support auto updating. Code Signing with a valid certificate is required.
 * https://simulatedgreg.gitbooks.io/electron-vue/content/en/using-electron-builder.html#auto-updating
 */

/*import {
  autoUpdater
} from 'electron-updater'

autoUpdater.on('update-downloaded', () => {
  autoUpdater.quitAndInstall()
})

app.on('ready', () => {
  if (process.env.NODE_ENV === 'production') autoUpdater.checkForUpdates()
})*/

function buildMenu() {
  const template = [{
    label: "Настройки",
    submenu: [{
        label: "Открыть инструменты разработчика",
        click: function() {
          mainWindow.webContents.openDevTools();
        }
      },
      {
        label: "Выйти",
        click: function() {
          app.quit();
        }
      }
    ]
  }];
  const menu = Menu.buildFromTemplate(template);
  Menu.setApplicationMenu(menu);
}