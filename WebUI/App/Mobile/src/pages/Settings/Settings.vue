<template>
  <f7-page>
    <f7-navbar title="Настройки" back-link="Back"></f7-navbar>
<f7-list>
  <f7-list-item link="/settings/profession/" title="Профессия">
      <slot name="after-list">
        Бухгалтер
      </slot>
  </f7-list-item>
  <f7-list-item  link="/settings/preferred-time/" title="Предпочтительное время"></f7-list-item>
</f7-list>
  </f7-page>
</template>
<script>
</script>
