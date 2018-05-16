<template>
  <f7-page>
    <f7-navbar>
      <f7-nav-left>
        <f7-link icon-if-ios="f7:menu"
                 icon-if-md="material:menu"
                 panel-open="left"></f7-link>
      </f7-nav-left>
      <f7-nav-title>Тренировка</f7-nav-title>
      <f7-nav-right>
        <f7-link>Начать</f7-link>
      </f7-nav-right>
    </f7-navbar>
    <f7-block v-for="(item, index) in items"
              :key="index">
      <h3>{{item.header}}</h3>
      <ul class="list media-list"
          style="padding:0">
        <li v-for="exercise in item.exercises"
            :key="exercise.title">
          <a href="/training"
             class="item-link item-content">
            <div class="item-media"
                 style="width:30%"><img :src="exercise.avatar"
                   style="width:100%" /></div>
            <div class="item-inner">
              <div class="item-title-row">
                <div class="item-title">{{exercise.title}}</div>
              </div>
              <div class="item-subtitle">{{exercise.countOfRepeats}}</div>
              <div class="item-subtitle">Не выполнено</div>
              <div class="item-text">{{exercise.subtitle}}</div>
            </div>
          </a>
        </li>
      </ul>
    </f7-block>
  </f7-page>
</template>

<script>
export default {
  data() {
    return {
      items: [
        {
          header: "Низкая сложность",
          exercises: [
            {
              avatar: "https://www.wikireading.ru/img/143620_73_i_042.png",
              title: "Наклоны",
              countOfRepeats: 10,
              subtitle:
                "oчeнь пpocтoe yпpaжнeниe, koтopoe знakomo нam eщe c дeтcтвa"
            }
          ]
        },
        {
          exercises: [
            {
              avatar:
                "https://i.ytimg.com/vi/vJmNWi2EMew/hqdefault.jpg?sqp=-oaymwEYCNIBEHZIVfKriqkDCwgBFQAAiEIYAXAB&rs=AOn4CLBEpvl3mFwNzV-JpynmS5WcO-tSbQ",
              title: "Отжиманя",
              countOfRepeats: 10,
              subtitle:
                "Постарайтесь соблюдать технику выполнения. Вдох на во время опускания туловища, выдох на подъеме"
            },
            {
              avatar: "http://sportwiki.to/images/3/3f/Sil_men_prised.jpg",
              title: "Приседания",
              countOfRepeats: 10,
              subtitle:
                "Одно из базовых силовых упражнений (в том числе в пауэрлифтинге и культуризме)"
            }
          ],
          header: "Средняя сложность"
        }
      ]
    };
  }
};
</script>
