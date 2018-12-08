import {
  store
} from '../store'
import {
  ipcRenderer
} from "electron"
export const NotificationsRunner = {
  initNotifications: initNotifications
}

let trainingTimeInterval = null;
let settings = null;


function stringTimeToTimeObj(str) {
  let timeArray = str.split(":");
  return {
    hours: Number.parseInt(timeArray[0]),
    minutes: Number.parseInt(timeArray[1])
  };
}

function getCurrentTimeObj() {
  var date = new Date();
  return {
    hours: date.getHours(),
    minutes: date.getMinutes()
  };
}

function calculateClosestTime() {
  let closestTime = settings.preferredTrainingTime.find(t => {
    let timeObj = stringTimeToTimeObj(t);
    var currentTime = getCurrentTimeObj();
    if (timeObj.hours > currentTime.hours || (timeObj.hours ==
        currentTime.hours && timeObj.minutes > currentTime.minutes)) {
      return true;
    }
    return false;
  });
  if (closestTime == null) {
    return stringTimeToTimeObj(settings.preferredTrainingTime[0]);
  }
  return stringTimeToTimeObj(closestTime);
}


function initNotifications() {
  console.log(store);
  store.watch(
    () => store.getters['features/settings/settings'], value => {
      settings = value;
      runNotifications();
    }
  )
  store.watch(
    () => store.getters['features/settings/profession'], value => {
      settings = store.getters['features/settings/settings'];
      runNotifications();
    }
  )
  store.watch(
    () => store.getters['features/settings/preferredTime'], value => {
      settings = store.getters['features/settings/settings'];
      runNotifications();
    }
  )
  if (store.getters["features/settings/settings"] != null) {
    settings = store.getters["features/settings/settings"];
    runNotifications();
  }
}

function runNotifications() {
  if (trainingTimeInterval != null) {
    clearInterval(trainingTimeInterval);
  }
  let currentTime = getCurrentTimeObj();
  let closestTime = calculateClosestTime();
  trainingTimeInterval = setInterval(() => {
    let currentTime = getCurrentTimeObj();
    if (closestTime.hours == currentTime.hours &&
      closestTime.minutes == currentTime.minutes) {
      ipcRenderer.send('notify-user-about-training');
      runNotifications();
    }
  }, 500)
}