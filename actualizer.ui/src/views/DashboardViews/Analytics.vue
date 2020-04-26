<template>
  <div>
    <div class="tabs is-boxed">
      <ul>
        <li v-for="(tab) in tabs" :key="tab.name" :class="[currentTab.name === tab.name  ? 'is-active' : '']">
          <a v-on:click="currentTab = tab"> 
            <span class="icon is-small"><i class="fas fa-image" aria-hidden="true"></i></span>
            <span> {{tab.name}} </span>
          </a>
        </li>
      </ul>
    </div>
    <component v-bind:is="currentTab.component" class="tab"></component>

  </div>
</template>

<script>
//import Sentiment from "@/components/Sentiment.vue";
// eslint-disable-next-line no-unused-vars
import tabs from '../tabs/AnalyticsTabs.js';

export default {
  name: 'Analytics',
  data() {
    return {
      tabs: tabs,
      currentTab: tabs[0]
    };
  },
  methods: {
  }
};
</script>