<template>
  <div>
    <div class="one-off-searchbox" >
            <b-input v-if="results" placeholder="type to filter comments" type="search" v-model="searchbox"></b-input>
          </div>
    <div class="container">   
      <keep-alive v-if="results">   
        <section>
        <b-table
        :data="filteredComments" ref="table"  narrowed :mobile-cards="true" 
        @click="(row, index) => $buefy.dialog.alert({ message:`${row.text}`,confirmText: 'close', trapFocus: true, canCancel: ['escape','outside']})"
        :paginated="isPaginated" :per-page="perPage" :current-page.sync="currentPage" :pagination-simple="isPaginationSimple" :pagination-position="paginationPosition"
        :default-sort-direction="defaultSortDirection" :sort-icon="sortIcon" :sort-icon-size="sortIconSize"      
        aria-next-label="Next page" aria-previous-label="Previous page" aria-page-label="Page" aria-current-label="Current page">
          <template slot-scope="results">
            <b-table-column field="id" label="Id" width="40" numeric>{{ results.row.id }}</b-table-column>
            <b-table-column field="text" label="Comment" sortable>{{ results.row.text.length > 100 ? `${results.row.text.substring(0,140)}...`: results.row.text  }}</b-table-column>
            <b-table-column field="likeCount" label="Votes" width="40" numeric sortable>{{ results.row.likeCount }}</b-table-column>
            <b-table-column field="publishedAt" label="Date" sortable centered> {{ new Date(results.row.publishedAt).toLocaleDateString() }}</b-table-column>
          </template>
            <!-- <template slot="detail" slot-scope="results" class="comment-article">
            <article class="media message">
              <figure class="media-left"><figure class="image is-64x64"><img src="https://bulma.io/images/placeholders/128x128.png" alt="Image" /></figure></figure>
              <div class="media-content"><div class="content"><p>{{ results.row.text }} </p></div></div>
            </article>
          </template> -->
          </b-table>
        </section>
      </keep-alive>
      <div class="notification is-warning has-text-centered" v-else>
        <strong>No Comments! Start search or try a different video ID, or query.</strong>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex';

export default {
  name: 'CommentExplorer',
  props: {}, //make this take props
  data() {
    return {
      labelPosition: 'on-border',
      searchbox: '',
      isPaginated: true,
      isPaginationSimple: true,
      paginationPosition: 'top',
      defaultSortDirection: 'asc',
      sortIcon: 'arrow-up',
      sortIconSize: 'is-small',
      currentPage: 1,
      perPage: 15,
      defaultOpenedDetails: [1],
      showDetailIcon: true
    };
  },
  methods: {},
  computed: {
    filteredComments() {
      if (this.results) {
        let search = (this.searchbox || '').toLowerCase().trim();
        let filtered = this.results.documents.filter(values => {
          let textsearch = (values.text.trim() || '').toLowerCase();
          textsearch = textsearch.replace(/\s{2,}/g, '');
          let IdSearch = values.id;
          return textsearch.indexOf(search) > -1 || IdSearch == parseInt(search);
        });
        return filtered;
      }
      return null;
    },

    ...mapGetters({
      results: 'results'
    }),
    ...mapState({
      user: 'user',
      loading: 'loading',
      comments: 'comments'
    })
  }
};
</script>
<style lang="scss">
  .one-off-searchbox {
    padding: 20px 0px 15px 0px; 
    max-width: 20rem;
    margin-left: auto;
  }
  @media screen and (max-width: 692px){
    .one-off-searchbox {
      padding: 20px 0px 15px 0px; 
      margin-left: auto;
      max-width: 200rem;
    }
  }
</style>