<template>
  <div>
    <div class="columns container analytics-container">

      <div class="column is-half">      
        <h1 class="title is-4">Comment Search</h1>
          <div class="columns">
            <div class="column is-2">
              <b-field label="Type" horizontal>
                <b-switch v-model="query.searchType" true-value="Simple" false-value="Bulk">
                  {{ query.searchType }}
                </b-switch>
              </b-field>
            </div>
          </div>
        <b-message>
          <p v-if="this.query.searchType === 'Simple'">Simple queries return 0-100. <br>
            If you would like to search more comments, please sign up for an account.
            Then visit the settings page to 
          </p>
          <p v-else>
            Bulk returns a max of 1000.
            <br />
            <small>TODO: Add use of personal API key for Max Results</small>
          </p>
        </b-message>
      </div>
      <div class="column is-half">
        <form @submit.prevent="GetComments">
          <b-field label="Link" horizontal>
            <b-input
              type="text"
              placeholder="enter a youtube URL"
              v-model="videoURL"
              @input="getIDFromURL(videoURL)"
            ></b-input>
          </b-field>
          <b-field label="VideoID" horizontal>
            <b-input
              type="text"
              id="videoId"
              v-model="query.videoId"
              :disabled="!videoURL ? false : true"
              placeholder="enter a video id"
            ></b-input>
          </b-field>
          <b-field label="Query" horizontal>
            <b-input id="search" v-model="query.search" placeholder="search for something"></b-input>
          </b-field>
          <b-field label="Count" v-show="query.searchType === 'Simple'" horizontal>
            <b-input type="number" id="count" v-model.number="query.count" placeholder="0-100"></b-input>
          </b-field>
          <div class="columns">
            <div class="column has-text-centered">
              <b-button id="getCommentsBtn" class="is-primary" native-type="submit">get comments</b-button>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'fetch',
  data() {
    return {
      results: '',
      videoURL: null,
      query: {
        searchType: 'Simple',
        search: '',
        videoId: 'AyNf4k9xIXg',
        count: 100
      }
    };
  },
  methods: {
    GetComments() {
      this.$store.dispatch('search', {
        video_id: this.query.videoId,
        search: this.query.search,
        count: this.query.count,
        searchType: this.query.searchType == 'Simple' ? 'search' : 'bulk'
      });
    },
    getIDFromURL(videoURL) {
      try{
        if(videoURL.includes('youtu.be')){
          return this.query.videoId = videoURL.match('be/([^;]*)')[1];
        }
        return this.query.videoId = videoURL.match('=([^;]*)&')[1];
      } catch {
        throw new Error("Invalid Yotube URL");
      }
    }
  }
};
</script>
