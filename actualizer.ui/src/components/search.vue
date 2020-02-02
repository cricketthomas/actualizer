<template>
    <div id="wrapper" class="container is-centered">
        <div class="columns">
            <form @submit.prevent="GetComments" class="form">
                <div class="columns">
                    <div class="column is-half">
                        <!-- <b-tooltip
                        label="Simple Search get you 0-100, while bulk search keeps querying the youtube api, until it reaches a max of 1000 comments"
                        :animated="true"
                        :multilined="true"
                        size="is-medium">                    
                        </b-tooltip>-->
                        <div class="field-label is-normal">
                        <label for="radio" class="label">Search Type</label>
                        <b-radio v-model="query.searchType" name="name" native-value="search">Simple Search</b-radio>
                        <b-radio v-model="query.searchType" name="name" native-value="bulk">Bulk Search</b-radio>
                    </div>
                        </div>

                <div class="column is-half">
                    <div class="field-label is-normal">
                        <label for="inputs" class="label">Search Params</label>
                        <b-field>
                            <b-input id="search" v-model="query.search" placeholder="Search.."> </b-input>
                        </b-field>
                        <b-field>
                            <b-input type="text" id="video_id" v-model="query.video_id" placeholder="enter a video id">
                            </b-input>
                        </b-field>
                        <b-field v-show="query.searchType === 'search'">
                            <b-input type="number" id="count" v-model.number="query.count" placeholder="0-25">
                            </b-input>
                        </b-field>
                    </div>            
                    </div>
                </div>
                <div class="columns is-mobile">
                    <div class="column is-half is-offset-one-quarter">
                        <b-button id="getCommentsBtn" @click="GetComments()">get comments</b-button>
                    </div>
                </div>

            </form>
        
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

