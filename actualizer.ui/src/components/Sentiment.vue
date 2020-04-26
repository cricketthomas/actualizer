/* eslint-disable no-console */
<template>
  <div>
    <div class="columns container analytics-container">
          <div class="column">
            <h1 class="title is-6">Sentiment Scores</h1>
            <small>
            Use the get sentiment button to fetch sentiment of the comments you've searched. 
            <br>
            Learn more about the sentiment scoring <a href="https://github.com/cjhutto/vaderSentiment">here</a>.
             <br>
            I'm fairly confident that these charts won't have any meaning to them whatsoever.
            </small>
            <div class="is-flex">
                <b-select v-model="sentiment_type" size="is-small">
                  <option value="compound" label="compound"></option>
                  <option value="positive" label="positive"></option>
                  <option value="negative" label="negative"></option>
                </b-select>
              <b-button @click="sentiment()" id="sentiment-btn" size="is-small" class="is-info">Get Setiment</b-button>
            </div>
          </div>

      <section class="column">
        <form @submit.prevent="addChartTypes">
          <b-field class="control" label="Title" horizontal>
            <b-input placeholder="Enter something descriptive" v-model="chart_title" size="is-small"></b-input>
          </b-field>

          <b-field label="Type" horizontal>
            <b-select v-model="chart_type" expanded size="is-small">
              <option value="area" selected>Area</option>
              <option value="line">Line</option>
              <option value="bubble">Bubble</option>
            </b-select>
          </b-field>
          <b-field
            label="Metric"
            horizontal
            :type="{ 'is-danger': hasError }"
            :message="[{ 'The chart data selection cannot be null': hasError }]">
            <b-select placeholder="select a timeseries metric" v-model="data_selection" expanded size="is-small">
              <option value="month" label="month"></option>
              <option value="day" label="day"></option>
              <option value="hour" label="hour"></option>
              <option v-show="analytics.sentiment.likeAggregate.length > 0" value="likes" label="likes"></option>
            </b-select>
          </b-field>
          <div class="columns">
            <div class="column has-text-centered">
              <b-button @click="addChartTypes()" class="is-primary" :disabled="!analytics.sentiment.textanalyticsbase">Add Chart</b-button>
            </div>
          </div>
        </form>
      </section>
    </div>
    <div v-if="charts_to_render">
      <div v-for="(chart, index) in charts_to_render" :key="index">
        <a class="delete is-small" @click="removeChart(charts_to_render, index)"></a>
          <CustomChart :chart_options="chart" />
      </div>
    </div>
  </div>
</template>
<script>
import { mapActions, mapState } from 'vuex';
import CustomChart from '@/components/CustomChart.vue';
export default {
  name: 'Sentiment',
  data() {
    return {
      data_selection: null,
      chart_type: 'area',
      chart_title: '',
      charts_to_render: [],
      sentiment_type: 'compound',
      hasError: false
    };
  },

  methods: {
    ...mapActions({ VaderSentiment: 'VaderSentiment' }),

    removeChart: (charts_to_render, index) => charts_to_render.splice(index, 1),

    addChartTypes() {
      if (null === this.data_selection) {
        this.hasError = true;
        return;
      }
      this.hasError = false;

      this.charts_to_render.push({
        ...this.chart_data_returned
      });
    },

    sentiment() {
      this.VaderSentiment({
        score_type: this.sentiment_type,
        stopword: true,
        documents: this.comments.results
      });
    },

    getDataSelection() {
      switch (this.data_selection) {
        case 'month':
          return this.analytics.sentiment.monthAggregate.map(d => d.avg.toFixed(2));
        case 'day':
          return this.analytics.sentiment.dayAggregate.map(d => d.avg.toFixed(2));
        case 'hour':
          return this.analytics.sentiment.hourAggregate.map(d => d.avg.toFixed(2)).sort();
        case 'likes':
          return this.analytics.sentiment.likeAggregate.map(d => d.avg.toFixed(2));
        default:
          break;
      }
    },
    getDataCategory() {
      switch (this.data_selection) {
        case 'month':
          return this.analytics.sentiment.monthAggregate.map(d => d.month);
        case 'day':
          return this.analytics.sentiment.dayAggregate.map(d => d.day);
        case 'hour':
          return this.analytics.sentiment.hourAggregate.map(d => d.hour);
        case 'likes':
          return this.analytics.sentiment.likeAggregate.map(d => d.likeCount);
        default:
          break;
      }
    }
  },
  computed: {
    ...mapState({
      analytics: 'analytics',
      comments: 'comments'
    }),
    chart_data_returned() {
      return {
        title: this.chart_title,
        type: this.chart_type,
        subtitle: this.data_selection,
        data_group: this.getDataSelection(this.data_selection),
        data_category: this.getDataCategory(this.data_selection)
      };
       
    }
  },
  // eslint-disable-next-line vue/no-unused-components
  components: { CustomChart }
};
</script>
<style lang="scss">
  #sentiment-btn {
    margin-left: 1rem;
    height: 2rem
  }
</style>
