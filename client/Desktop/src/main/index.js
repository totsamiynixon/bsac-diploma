import {
  app,
  BrowserWindow,
  ipcMain,
  ipcRenderer,
  remote,
  Menu,
  Notification
} from "electron";
import Notifier from "node-notifier";
// import { autoUpdater } from "electron-updater";

/**
 * Set `__static` path to static files in production
 * https://simulatedgreg.gitbooks.io/electron-vue/content/en/using-static-assets.html
 */
if (process.env.NODE_ENV !== "development") {
  global.__static = require("path")
    .join(__dirname, "/static")
    .replace(/\\/g, "\\\\");
}

let mainWindow;
const winURL =
  process.env.NODE_ENV === "development"
    ? `http://localhost:9080`
    : `file://${__dirname}/index.html`;

function createWindow() {
  /**
   * Initial window options
   */
  mainWindow = new BrowserWindow({
    height: 1000,
    useContentSize: false,
    width: 1600
  });

  mainWindow.loadURL(winURL);

  mainWindow.on("closed", () => {
    mainWindow = null;
  });
}

app.on("ready", function() {
  createWindow();
  buildMenu();
});

app.on("window-all-closed", () => {
  if (process.platform !== "darwin") {
    app.quit();
  }
});

app.on("activate", () => {
  if (mainWindow === null) {
    createWindow();
  }
});

ipcMain.on("notifier:notify-user-about-training", (event, data) => {
  Notifier.notify(
    {
      title: "Пришло время тренировки!",
      message:
        "Кликните, чтобы приступить, или закройте, чтобы отложить на 10 минут",
      sound: true,
      wait: true
    },
    function(err, response) {
      mainWindow.webContents.send("notifier:set-aside-timer");
    }
  );
  Notifier.on("click", function(notifierObject, options) {
    mainWindow.show();
    mainWindow.webContents.send("notifier:notify-user-about-training");
  });
});

function buildMenu() {
  const template = [
    {
      label: "Настройки",
      submenu: [
        {
          label: "Выйти",
          click: function() {
            app.quit();
          }
        }
      ]
    }
  ];
  if (process.env.NODE_ENV !== "production") {
    template.submenu.push({
      label: "Открыть инструменты разработчика",
      click: function() {
        mainWindow.webContents.openDevTools();
      }
    });
  }
  const menu = Menu.buildFromTemplate(template);
  Menu.setApplicationMenu(menu);
}
