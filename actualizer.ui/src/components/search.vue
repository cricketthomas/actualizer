<template>
    <div id="wrapper">
        <div class="container">
            <form @submit.prevent="GetComments">
                <div class="block">
                    <b-radio v-model="query.searchType" name="name" native-value="search">
                        Search
                    </b-radio>

                    <b-radio v-model="query.searchType" name="name" native-value="bulk">
                        Bulk
                    </b-radio>
                </div>
                <b-field>
                    <b-input id="search" v-model="query.search" placeholder="Search.."> </b-input>
                </b-field>
                <b-field>
                    <b-input type="text" id="video_id" v-model="query.video_id" placeholder="enter a video id">
                    </b-input>
                </b-field>
                <b-field v-show="query.searchType === 'search'">
                    <b-input type="number" id="count" v-model.number="query.count" placeholder="0-25"> </b-input>
                </b-field>
            </form>
            <b-button id="getCommentsBtn" @click="GetComments()">fetch comments</b-button>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex';

export default {
    data() {
        return {
            isFullPage: true,
            userClaims: null,
            results: '',
            query: {
                searchType: 'search',
                search: 'love',
                video_id: 'SsKT0s5J8ko',
                count: 5
            }
        };
    },
    methods: {
        GetComments() {
            this.$store.dispatch('search', {
                video_id: this.query.video_id,
                search: this.query.search,
                count: this.query.count,
                searchType: this.query.searchType
            });
        }
    },
    computed: {
        ...mapGetters({
            isLoading: 'isLoading'
        })
    }
};
</script>
<style scoped>
</style>

