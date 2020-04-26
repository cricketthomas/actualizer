<template>
  <div class="home">
    <router-link to="dashboard/search">
      <section class="is-fullheight">
        <div class="hero-head">
          <div class="container has-text-centered">
            <div class="is-flex has-centered-text main-header">
              <img class="main-img" src="@/assets/actualizerheader.png" alt="Actualizer logo" />
            </div>
            <div>
              <h3 class="title is-5">search youtube comments in bulk.</h3>
              <small class="enter-msg"></small>
            </div>
          </div>
        </div>
        <div class="hero-body main-body" v-if="stats">
          <div>
            <div class="stats-block">
              <p class="title is-3">
                Total Searches: <span ref="totalsearches">{{ countUp(stats.totalSearches, 'totalsearches') }}</span>
              </p>
            </div>
            <div class="stats-block">
              <p class="title is-3">
                Total Comments: <span ref="fetch">{{ countUp(stats.totalCommentsSearched, 'fetch') }}</span>
              </p>
            </div>
            <div class="stats-block">
              <p class="title is-3">
                Keywords Extracted: <span ref="keyword">{{ countUp(stats.keywordsExtracted, 'keyword') }}</span>
              </p>
            </div>
            <div class="stats-block">
              <p class="title is-3">
                Sentiment Extracted: <span ref="sentiment">{{ countUp(stats.sentimentAPIRequests, 'sentiment') }}</span>
              </p>
            </div>
          </div>
        </div>
      </section>
    </router-link>
  </div>
</template>

<script>
import api from '@/api/axios.js';
export default {
  name: 'home',
  data() {
    return {
      stats: null
    };
  },
  created() {
    api
      .get('/stats')
      .then(response => (this.stats = response.data))
      .catch(err => new Error(err));
  }
};
</script>
<style lang="scss" scoped>
h3 {
  font-family: 'Courier New', Courier, monospace !important;
  font-weight: bold;
}
.home {
  overflow-y: hidden !important;
  max-height: 100%;
  font-family: 'Courier New', Courier, monospace;
  background-color: #1f2424 !important;
}

.main-img {
  max-width: 85vw;
  animation-name: bouncy;
  animation-duration: 1.5s;
  animation-fill-mode: both;
  animation-iteration-count: infinite;
  animation-direction: alternate;
  animation-timing-function: linear;
}

.main-header {
  display: flex;
  justify-content: center;
}
.main-body {
  display: flex;
  justify-content: center;
}
@keyframes bouncy {
  from {
    transform: translateY(-5px);
  }
  to {
    transform: translateY(10px);
  }
}

@keyframes mergeIn {
  from {
    transform: translateY(-200px);
  }
  to {
    transform: translateY(0px);
  }
}
@media screen and (min-width: 760px) {

  .stats-block {
    border: 2px solid whitesmoke;
    width: 65vh;
    padding: 3rem;
    margin-bottom: 2rem;
    animation-name: mergeIn;
    animation-duration: 1.5s;
    p {
      font-weight: bold;
      font-size: 2vw;
    }
  }
  .enter-msg::before {
    content: 'Click to Enter';
  }
}

@media screen and (max-width: 760px) {
  .main-img {
    animation-name: mergeIn;
    animation-duration: 1s;
    animation-fill-mode: both;
    animation-iteration-count: 1;
    animation-direction: alternate;
    animation-timing-function: linear;
  }
  .enter-msg::before {
    content: 'Tap to Enter';
  }
  .stats-block {
    border: 2px solid whitesmoke;
    padding: 40px;
    margin-bottom: 15px;
    animation-name: mergeIn;
    animation-duration: 1s;

    p {
      font-weight: bold;
      font-size: 16pt;
    }
  }
}
</style>
