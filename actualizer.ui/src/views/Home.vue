<template>
  <div class="home">
    <section class="is-fullheight main-header">
      <div class="hero-body">
        <router-link to="dashboard/search">
          <div class="container has-text-centered">
            <div class="is-flex has-centered-text main-body">
              <h1 class="main-logo title is-1">Actualizer</h1>
            </div>
            <div>
              <h3 class="title is-5">search youtube comments in bulk.</h3>
              <small class="enter-msg"></small>
            </div>
          </div>
          <div v-if="stats">
            <div class="columns has-text-centered is-flex main-body">
              <p class="title is-3 column is-3 stats">
                Total Searches: <span ref="totalsearches"> {{ countUp(stats.totalSearches, 'totalsearches') }} </span>
              </p>
              <p class="title is-3 column is-3 stats">
                Total Comments: <span> {{ stats.totalCommentsSearched }} </span>
              </p>
            </div>
            <div class="columns  has-text-centered is-flex main-body">
              <p class="title is-3 column is-3 stats">
                Keywords Extracted: <span>{{ stats.keywordsExtracted }} </span>
              </p>
              <p class="title is-3 column is-3 stats">
                Sentiment Extracted: <span> {{ stats.sentimentAPIRequests }} </span>
              </p>
            </div>
          </div>
          <div v-else class="loading-text has-text-centered is-flex main-body">
            <h2 class="title is-3">loading search stats<span>.</span><span>.</span><span>.</span></h2>
          </div>
        </router-link>
      </div>
    </section>
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
.home {
  height: 2000px;
  font-family: 'Courier New', Courier, monospace;
  background-color: #1f2424 !important;
}

.main-header {
  transform: translateY(10rem);
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

@keyframes blink {
  from {
    opacity: 0.1;
  }
}
div.loading-text {
  display: inline-block;
  span:nth-child(1) {
    animation-name: blink;
    animation-duration: 0.66s;
    animation-fill-mode: forwards;
    animation-iteration-count: infinite;
    animation-direction: alternate;
    animation-timing-function: linear;
  }
  span:nth-child(2) {
    animation-name: blink;
    animation-duration: 0.8s;
    animation-fill-mode: forwards;
    animation-iteration-count: infinite;
    animation-direction: alternate;
    animation-timing-function: linear;
  }
  span:nth-child(3) {
    animation-name: blink;
    animation-duration: 1s;
    animation-fill-mode: both;
    animation-iteration-count: infinite;
    animation-direction: alternate;
    animation-timing-function: linear;
  }
}

@media screen and (min-width: 760px) {
  .main-logo {
    animation-name: bouncy;
    animation-duration: 1s;
    animation-fill-mode: both;
    animation-iteration-count: infinite;
    animation-direction: alternate;
    animation-timing-function: linear;
    font-weight: bolder;
    font-size: 7vw !important;
  }

  .enter-msg::before {
    content: 'Click to Enter';
  }
  .stats {
    text-align: center;
    p {
      font-weight: bold;
    }
    span {
      font-weight: bolder;
    }
  }
}

@media screen and (max-width: 760px) {
  .main-logo {
    animation-name: bouncy;
    animation-duration: 1.5s;
    animation-fill-mode: both;
    animation-iteration-count: infinite;
    animation-direction: alternate;
    animation-timing-function: linear;
    font-weight: bolder;
    font-size: 12vw !important;
  }
  .enter-msg::before {
    content: 'Tap to Enter';
  }

  .stats {
    text-align: center;
    font-size: 20pt !important;

    p {
      font-weight: bold;
    }
    span {
      font-weight: bolder;
    }
  }
}
</style>
