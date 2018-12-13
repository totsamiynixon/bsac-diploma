import moment from "moment";
export function Notifier(ipcRenderer, store) {
  let trainingTimeTimer = null;
  this.initNotifications = () => {
    ipcRenderer.on("notifier:set-aside-timer", () => {
      let curDate = new Date();
      curDate.setSeconds(0, 0);
      startTimerAndNotifyAt(new Date(curDate.getTime() + 10 * 60000));
    });
    store.watch(
      () => store.getters["features/settings/preferredTime"],
      value => {
        if (value != null && value.length > 0) {
          let closestTime = getClosestTrainTime();
          if (closestTime) startTimerAndNotifyAt(closestTime);
        } else {
          breakTimer();
        }
      }
    );
  };

  function getClosestTrainTime() {
    if (store.getters["features/settings/preferredTime"]) {
      let preferredTimeArr = store.getters["features/settings/preferredTime"];
      let differedArray = preferredTimeArr
        .map(time => {
          let _time = moment(time, "HH:mm");
          return moment()
            .set({
              hour: _time.get("hour"),
              minute: _time.get("minute"),
              second: 0
            })
            .diff(moment());
        })
        .map(res => (res <= 0 ? Number.MAX_VALUE : res));
      let time = moment(
        preferredTimeArr[differedArray.indexOf(Math.min(...differedArray))],
        "HH:mm"
      );
      return moment()
        .set({
          hour: time.get("hour"),
          minute: time.get("minute"),
          second: 0
        })
        .toDate();
    }
    return null;
  }
  function breakTimer() {
    if (trainingTimeTimer != null) {
      clearTimeout(trainingTimeTimer);
    }
  }
  function startTimerAndNotifyAt(notifyAtTime) {
    breakTimer();
    trainingTimeTimer = setTimeout(() => {
      ipcRenderer.send("notifier:notify-user-about-training");
      let closestTime = getClosestTrainTime();
      if (closestTime) startTimerAndNotifyAt(closestTime);
    }, notifyAtTime - new Date());
  }
}
