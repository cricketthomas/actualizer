<template>
    <div>
        <hr />
        <input type="text" name="searchbox" id="searchbox" v-model="searchbox" />
        <!-- <div v-for="(comment, key) in filteredComments" v-bind:key="key">
            <p>{{ comment }}</p>
            <hr />
        </div>  -->
        <!-- make this a paginated sortable table. -->
        <!-- <div class="container">
            <b-table :data="results.documents" :columns="columns"> </b-table>
        </div> -->
        <keep-alive>
            <div class="container">
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
                    aria-current-label="Current page">
                    <template slot-scope="results">
                        <b-table-column field="id" label="ID" width="40" sortable numeric>
                            {{ results.row.id }}
                        </b-table-column>

                        <b-table-column field="text" label="comment text" sortable >
                            {{ results.row.text }}
                        </b-table-column>

                        <b-table-column field="publishedAt" label="date" sortable centered>
                            {{ new Date(results.row.publishedAt).toLocaleDateString() }}
                        </b-table-column>

                        <b-table-column field="likeCount" label="likes" sortable numeric>
                            {{ results.row.likeCount }}
                        </b-table-column>
                    </template>
                </b-table>
            </div>
        </keep-alive>
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
            columns: [
                { field: 'id', label: 'id', width: '20', numeric: true },
                { field: 'publishedAt', label: 'date', searchable: false },
                { field: 'text', label: 'comment', searchable: true }
            ],
            isPaginated: true,
            isPaginationSimple: true,
            paginationPosition: 'bottom',
            defaultSortDirection: 'asc',
            sortIcon: 'arrow-up',
            sortIconSize: 'is-small',
            currentPage: 1,
            perPage: 15
        };
    },
    computed: {
        filteredComments() {
            if (this.results) {
                let search = (this.searchbox || '').toLowerCase().trim();
                let filtered = this.results.documents.filter(values => {
                    let textsearch = (values.text || '').toLowerCase();
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