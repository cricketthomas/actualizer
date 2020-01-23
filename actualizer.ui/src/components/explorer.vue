<template>
    <div>
        <input type="text" name="searchbox" id="searchbox" v-model="searchbox" />
        <div v-for="(comment, key) in filteredComments" v-bind:key="key">
            <p>{{ comment }}</p>
            <hr />
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
    name: 'CommentExplorer',
    props: [], //make this take props
    data() {
        return {
            searchbox: ''
        };
    },
    computed: {
        filteredComments() {
            if (this.simpleComments) {
                let search = (this.searchbox || '').toLowerCase().trim();
                let filtered = this.simpleComments.comments.filter(values => {
                    return values.text.indexOf(search) > -1;
                });
                return filtered;
            }
            return null;
        },
        //return commentsArray.filter(stringVal => stringVal.indexOf(search) > -1 )

        ...mapGetters({
            simpleComments: 'commentsObj'
        })
    }
};
</script>