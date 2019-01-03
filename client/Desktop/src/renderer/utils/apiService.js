import axios from 'axios'
export default {
  getAllExercises: params =>
    new Promise((resolve, reject) =>
      axios
        .get('/api/exercises/getAll', {
          params: {
            query: params.query,
            byCriterias: params.byCriterias,
            byExercises: params.byExercises,
          },
        })
        .then(response => resolve(response.data), reject),
    ),
  getExercise: id =>
    new Promise((resolve, reject) =>
      axios
        .get('/api/exercises/get', { params: { id } })
        .then(response => resolve(response.data)),
    ),

  getAllProfessions: () =>
    new Promise((resolve, reject) =>
      axios
        .get('/api/professions/getAll')
        .then(response => resolve(response.data), reject),
    ),

  getUserTraining: isGrouped =>
    new Promise((resolve, reject) =>
      axios
        .get(
          isGrouped
            ? '/api/userTrainings/get/grouped'
            : '/api/usertrainings/get',
        )
        .then(response => resolve(response.data), reject),
    ),

  completeUserTraining: userTrainingId =>
    new Promise((resolve, reject) =>
      axios
        .post('/api/usertrainings/complete', {
          userTrainingId,
        })
        .then(response => resolve(response.data), reject),
    ),

  updateUserTrainingRating: (userTrainingId, rating) =>
    new Promise((resolve, reject) =>
      axios
        .post('/api/usertrainings/rating', {
          userTrainingId,
          rating,
        })
        .then(response => resolve(response.data), reject),
    ),
  getFullUserInfo: () =>
    new Promise((resolve, reject) =>
      axios
        .get('/api/account/fullinfo')
        .then(response => resolve(response.data), reject),
    ),
  signIn: (email, password) =>
    new Promise((resolve, reject) =>
      axios
        .post('/api/account/sign-in', {
          email,
          password,
        })
        .then(response => resolve(response.data), reject),
    ),
  signUp: (email, password, confirmPassword) =>
    new Promise((resolve, reject) =>
      axios
        .post('/api/account/sign-up', {
          email,
          password,
          confirmPassword,
        })
        .then(response => resolve(response.data), reject),
    ),
  changeProfession: professionId =>
    new Promise((resolve, reject) =>
      axios
        .post('/api/settings/saveProfession', {
          professionId,
        })
        .then(response => resolve(response.data), reject),
    ),
  savePreferredTime: payload =>
    new Promise((resolve, reject) =>
      axios
        .post('/api/settings/savePrefferedTime', payload)
        .then(response => resolve(response.data), reject),
    ),
}
