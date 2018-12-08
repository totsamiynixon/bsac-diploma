<template>
    <f7-page with-subnavbar>
        <f7-navbar back-link="Back" title="Tab Routes" sliding>
                <f7-nav-right>
      <f7-button round @click="openSheet()" icon-f7="more"></f7-button>
      </f7-nav-right>
    </f7-navbar>
        </f7-navbar>
        <f7-toolbar tabbar>
            <f7-link tab-link href="#tab1">Tab 1</f7-link>
            <f7-link tab-link href="#tab2">Tab 2</f7-link>
            <f7-link tab-link href="#tab3">Tab 3</f7-link>
        </f7-toolbar>
        <f7-tabs swipeable>
            <f7-tab id="tab1">
                <tab1/>
            </f7-tab>
            <f7-tab id="tab2">
                <tab2/>
            </f7-tab>
            <f7-tab id="tab3">
                <tab3/>
            </f7-tab>
        </f7-tabs>
            <f7-sheet id="settings-sheet">
  <select name="fruits">
      <option value="apple" selected>Apple</option>
      <option value="pineapple">Pineapple</option>
      <option value="pear">Pear</option>
      <option value="orange">Orange</option>
      <option value="melon">Melon</option>
      <option value="peach">Peach</option>
      <option value="banana">Banana</option>
    </select>
</f7-sheet>
    </f7-page>
</template>

<script>
import Tab1 from "./tab1Content";
import Tab2 from "./tab2Content";
import Tab3 from "./tab3Content";
export default {
  data() {
    return {};
  },
    methods:{
    openSheet(){
      this.$f7.sheet.open("#settings-sheet", true);
    }
    },
  components: {
    Tab1,
    Tab2,
    Tab3
  }
};
</script>
