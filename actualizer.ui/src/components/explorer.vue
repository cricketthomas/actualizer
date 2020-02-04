<template>
    <div>
        <div class="container">
            <b-field>
                <b-input
                    type="text"
                    name="searchbox"
                    id="searchbox"
                    v-model="searchbox"
                    placeholder="filter by commnets text.."
                    autocomplete="false"
                ></b-input>
            </b-field>

            <keep-alive v-if="results">
                <b-table
                    :data="filteredComments"
                    :paginated="isPaginated"
                    :per-page="perPage"
                    :current-page.sync="currentPage"
                    :pagination-simple="isPaginationSimple"
                    :default-sort-direction="defaultSortDirection"
                    :sort-icon="sortIcon"
                    :sort-icon-size="sortIconSize"
                    default-sort="user.first_name"
                    aria-next-label="Next page"
                    aria-previous-label="Previous page"
                    aria-page-label="Page"
                    aria-current-label="Current page"
                    :show-detail-icon="showDetailIcon"
                    :opened-detailed="defaultOpenedDetails"
                    detailed
                    detail-key="id">
                    <template slot-scope="results">
                        <b-table-column field="id" label="Id" width="40" sortable numeric>
                            {{ results.row.id }}
                        </b-table-column>
                        <b-table-column field="text" label="Comment" sortable>
                            {{ `${results.row.text.substring(0, 140)}...` }}
                         <template>
                                <a @click="toggle(results.row)">
                                </a>
                            </template> 
                        </b-table-column>

                        <b-table-column field="publishedAt" label="Date" sortable centered>
                            {{ new Date(results.row.publishedAt).toLocaleDateString() }}
                        </b-table-column>
                        <b-table-column field="likeCount" label="Likes" sortable numeric>
                            {{ results.row.likeCount }}</b-table-column
                        >
                    </template>

                    <template slot="detail" slot-scope="results">
                        <div class="box">
                            <article class="media">
                                <div class="media-left">
                                  <figure class="image is-64x64">
                                        <img src="https://bulma.io/images/placeholders/128x128.png" alt="Image" />
                                    </figure> 
                                </div>
                                <div class="media-content">
                                    <div class="content">
                                        <p>
                                            <small>User Name: Blah</small>
                                            <br />
                                            {{ results.row.text }}
                                        </p>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </template>

                </b-table>
            </keep-alive>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
    name: 'CommentExplorer',
    props: {}, //make this take props
    data() {
        return {
            searchbox: '',
            isPaginated: true,
            isPaginationSimple: true,
            paginationPosition: 'bottom',
            defaultSortDirection: 'asc',
            sortIcon: 'arrow-up',
            sortIconSize: 'is-small',
            currentPage: 1,
            perPage: 15,
            defaultOpenedDetails: [1],
            showDetailIcon: true
        };
    },
    methods: {
        //toggle: (row) => this.$refs.table.toggleDetails(row)
    },
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
        //return commentsArray.filter(stringVal => stringVal.indexOf(search) > -1 )

        ...mapGetters({
            results: 'results'
        })
    }
};
</script>
<style lang="scss">
    .test{
        background-color: red
    }
</style>