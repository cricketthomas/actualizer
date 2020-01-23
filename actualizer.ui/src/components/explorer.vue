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
    data() {
        return {
            searchbox: ''
        };
    },
    computed: {
        filteredComments(){
        let search = (this.searchbox || "").toLowerCase().trim();
        let commentsArray = this.BasicComments.comments.map(comment => comment.text.toLowerCase());
        return commentsArray.filter(stringVal => stringVal.indexOf(search) > -1 )
        },
        ...mapGetters({
            BasicComments: 'commentsObj'
        })
    }
};
</script>