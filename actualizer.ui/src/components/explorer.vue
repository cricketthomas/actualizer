<template>
    <div>
        <hr>
        <input type="text" name="searchbox" id="searchbox" v-model="searchbox" />
         
         <div v-for="(comment, key) in filteredComments" v-bind:key="key">
            <p>{{ comment }}</p>
            <hr />
        </div>
        <!-- make this a paginated sortable table. -->
        <!-- <div class="container">
            <b-table :data="results.documents" :columns="columns"> </b-table>
        </div> -->

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
            ]
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