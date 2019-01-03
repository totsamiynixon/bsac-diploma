import moment from 'moment'
import store from '../store'
import EventBus from './eventBus'



export default {
  initNotifications: ipcRenderer => {
    let trainingTimeTimer = null
    function getClosestTrainTime() {
      if (store.getters['user/trainingTime']) {
        const currentTime = moment()
        const preferredTimeArr = store.getters['user/trainingTime']
        const differedArray = preferredTimeArr
          .map(time => {
            const _time = moment(time, 'HH:mm')
            const isInFuture = currentTime.diff(_time) > 0
            return moment()
              .set({
                hour: _time.get('hour'),
                minute: _time.get('minute'),
                second: 0,
                day: isInFuture ? _time.get('day') + 1 : _time.get('day'),
              })
              .diff(currentTime)
          })
          .map(res => (res <= 0 ? Number.MAX_VALUE : res))
        const time = moment(
          preferredTimeArr[differedArray.indexOf(Math.min(...differedArray))],
          'HH:mm',
        )
        if (currentTime.diff(time) > 0) {
          time.add(1, 'days')
        }
        return time
      }
      return null
    }

    function breakTimer() {
      if (trainingTimeTimer != null) {
        clearTimeout(trainingTimeTimer)
      }
    }
    function startTimerAndNotifyAt(notifyAtTime) {
      breakTimer()
      trainingTimeTimer = setTimeout(() => {
        ipcRenderer.send('notifier:notify-user-about-training')
        const closestTime = getClosestTrainTime()
        if (closestTime) startTimerAndNotifyAt(closestTime)
      }, notifyAtTime - new Date())
    }
    ipcRenderer.on('notifier:notify-user-about-training', () => {
      EventBus.emit('notifier:notify-user-about-training')
    })
    ipcRenderer.on('notifier:set-aside-timer', () => {
      const curDate = new Date()
      curDate.setSeconds(0, 0)
      startTimerAndNotifyAt(new Date(curDate.getTime() + 10 * 60000))
    })
    store.watch(
      () => store.getters['user/trainingTime'],
      value => {
        if (value != null && value.length > 0) {
          const closestTime = getClosestTrainTime()
          if (closestTime) startTimerAndNotifyAt(closestTime)
        } else {
          breakTimer()
        }
      },
    )
  },
}
