export function Notifier(ipcRenderer, store) {
  let that = this;
  this.setAsideToTime = null;
  this.trainingTimeInterval = null;
  this.initNotifications = () => {
    ipcRenderer.on("notifier:set-aside-timer", () => {
      that.setAsideToTime = getTimeAsObj(
        new Date(new Date().getTime() + 10 * 60000)
      );
      console.log(that.setAsideToTime);
    });
    store.watch(
      () => store.getters["features/settings/preferredTime"],
      value => {
        if (value != null && value.length > 0) {
          runNotifications();
        } else {
          stopNotifications();
        }
      }
    );
  };

  function stopNotifications() {
    if (that.trainingTimeInterval != null) {
      clearInterval(that.trainingTimeInterval);
    }
    that.setAsideToTime = null;
  }
  function runNotifications() {
    stopNotifications();
    let closestTime = calculateClosestTime();
    that.trainingTimeInterval = setInterval(() => {
      let currentTime = getTimeAsObj(new Date());
      if (
        closestTime.hours == currentTime.hours &&
        closestTime.minutes == currentTime.minutes
      ) {
        ipcRenderer.send("notifier:notify-user-about-training");
        runNotifications();
      }
    }, 60000);
  }

  function stringTimeToTimeObj(str) {
    if (!str) {
      return;
    }
    let timeArray = str.split(":");
    return {
      hours: Number.parseInt(timeArray[0]),
      minutes: Number.parseInt(timeArray[1])
    };
  }

  function getTimeAsObj(date) {
    return {
      hours: date.getHours(),
      minutes: date.getMinutes()
    };
  }

  function calculateClosestTime() {
    var currentTime = getTimeAsObj(new Date());
    if (
      that.setAsideToTime != null &&
      (that.setAsideToTime.hours > currentTime.hours ||
        (that.setAsideToTime.hours == currentTime.hours &&
          that.setAsideToTime.minutes > currentTime.minutes))
    ) {
      return setAsideToTime;
    }
    let closestTime = store.getters["features/settings/preferredTime"].find(
      t => {
        let timeObj = stringTimeToTimeObj(t);
        if (
          timeObj.hours > currentTime.hours ||
          (timeObj.hours == currentTime.hours &&
            timeObj.minutes > currentTime.minutes)
        ) {
          return true;
        }
        return false;
      }
    );
    if (closestTime == null) {
      return stringTimeToTimeObj(
        store.getters["features/settings/preferredTime"][0]
      );
    }
    return stringTimeToTimeObj(closestTime);
  }
}
