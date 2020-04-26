<template>
  <div>
    <div class="columns container analytics-container">
      <div class="column is-half">
        <h1 class="title is-6">Keyword Extraction</h1>
        <small>
          Keywords uses Microsoft ML to remove stopwords, then groups the commonly used words based on a threshold set
          by you, the default is 2.
          <br />
          Use the 'Exclude' field to enter additional words you would like to exclude.
        </small>
        <div class="is-flex">
          <b-select v-model="selected_font" size="is-small">
            <option v-for="(font, index) in fonts" :key="index" :value="font">{{ font }}</option>
          </b-select>
        </div>
      </div>
      <div class="column is-half">
        <form @submit.prevent="getKeywords">
          <b-field label="Threshold" horizontal>
            <b-input
              type="number"
              aria-placeholder="word threshold number"
              v-model="threshold"
              size="is-small"
            ></b-input>
          </b-field>
          <b-field label="Stopwords" horizontal>
            <b-checkbox v-model="stopword"></b-checkbox>
          </b-field>
          <!-- <b-field label="Exclude" horizontal>
            <b-input maxlength="200" type="text" size="is-small"></b-input>
          </b-field> -->
          <div class="columns">
            <div class="column has-text-centered">
              <b-button class="is-primary" native-type="submit" size="is-small">get keywords</b-button>
            </div>
          </div>
        </form>
      </div>
    </div>
    <div class="columns keyword-content">
      <div v-if="!analytics.keywords">
        <b-message>Get some keywords!</b-message>
      </div>

      <div class="wordcloud box" v-else-if="keywordsLength <= 150 && keywordsLength !== 0">
        <progress
          class="progress is-info"
          :value="progressArray.length"
          :max="keywordsLength"
          v-if="(progressArray.length - 1) !== keywordsLength"
        ></progress>
        <template class="column is-9">
          <VueWordCloud
            @update:progress="updateProgressbar"
            :words="wordcloud.words"
            :color="
              ([, weight]) =>
                weight > wordcloud.colors[4]
                  ? '#ff4e68'
                  : weight > wordcloud.colors[3]
                  ? '#3bc4c7'
                  : weight > wordcloud.colors[2]
                  ? '#3b9fe8'
                  : weight > wordcloud.colors[1]
                  ? '#ffd077'
                  : weight > wordcloud.colors[0]
                  ? 'Green'
                  : 'Teal'
            "
            :font-family="selected_font"
          />
          <!-- either im using this wrong, or something else but its really getting bogged down with the constant emit of events -->
        </template>
      </div>

      <div v-else>
        <b-message>
          <p v-if="analytics.keywords.length === 0">
            Looks like that threshold is too high..
          </p>
          <p v-else>
            Looks like theres too many many words to generate a wordcloud quickly ({{ analytics.keywords.length }}), try
            upping the threshold.
          </p>
        </b-message>
      </div>
      <ul class="column is-3 keyword-list" v-if="keywordsLength">
        <li v-for="(keyword, index) in analytics.keywords" :key="index">
          {{ keyword.word }}: {{ keyword.count }}
          <hr />
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
var arraystat = require('arraystat');

import { mapActions, mapState, mapGetters } from 'vuex';
//import arraystat from '@/scripts/arraystat.js';

export default {
  name: 'keywords',

  data() {
    return {
      stopword: true,
      threshold: 2,
      progressArray: [],
      selected_font: 'Finger Paint',
      selected_rotation: 'deg',
      arraystat: arraystat,
      fonts: [
        'Abril Fatface',
        'Annie Use Your Telescope',
        'Bahiana',
        'Baloo Bhaijaan 2',
        'Barrio',
        'Allan',
        'Finger Paint',
        'Fredericka the Great',
        'Gloria Hallelujah',
        'Indie Flower',
        'Life Savers',
        'Nothing You Could Do',
        'Pacifico',
        'Quicksand',
        'Righteous',
        'Sacramento',
        'Shadows Into Light',
        'Marker Felt',
        'Helvetica'
      ]
    };
  },
  methods: {
    getKeywords() {
      this.ActualizerKeyword({
        stopword: this.stopword,
        threshold: this.threshold,
        documents: this.comments.results
      });
      this.progressArray = [];
    },
    updateProgressbar(event) {
      if (event !== null) {
        if (this.progressArray.includes(event.completedWords)) {
          return;
        }
        this.progressArray.push(event.completedWords);
      }
    },
    ...mapActions({ ActualizerKeyword: 'ActualizerKeyword' })
  },
  computed: {
    ...mapState({
      analytics: 'analytics',
      comments: 'comments'
    }),
    ...mapGetters({ keywordsLength: 'keywordsLength' }),
    wordcloud() {
      return {
        words: this.analytics.keywords.map(w => [w.word, Math.log2(w.count) * 5]),
        colors: arraystat(this.analytics.keywords.map(w => [w.count]).flat(Infinity)).histogram.map(
          m => Math.log2(parseInt(m.min.toFixed())) * 4
        )
      };
    }
  }
};
</script>
<style lang="scss">
.wordcloud {
  height: 30rem;
}
.keyword-list {
  overflow-y: scroll;
  height: 45vh !important;
  margin-left: 2px;
  border-radius: 0.25em;
  background-color: #343c3d;
}
.keyword-content {
  max-width: 100%;
  padding-top: 2rem;
}
@media screen and (min-width: 760px) {
  .wordcloud {
    width: 70rem;
    height: 45rem;
  }
  .keyword-list {
    overflow-y: scroll;
    height: 45rem !important;
    margin-left: 2px;
    border-radius: 0.25em;
    background-color: #343c3d;
  }
}
</style>
