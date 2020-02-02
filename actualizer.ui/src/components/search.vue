<template>
    <div>
        <div id="wrapper" class="form-wrapper">
            <div class="columns form-container">
                <form @submit.prevent="GetComments">
                    <div class="columns">
                        <div class="column is-6">
                            <div class="field-label is-normal">
                                <label for="radio" class="label">Search Type</label>

                                <b-switch
                                    v-model="query.searchType"
                                    true-value="Simple"
                                    false-value="Bulk">
                                    {{ query.searchType }}
                                </b-switch>
                            </div>
                            <div class="column is-9 searchtype-message">
                                <b-message size="is-small">
                                    <p v-if="this.query.searchType === 'Simple'">
                                        Simple queries return 0-100.
                                    </p>
                                    <p v-else>
                                        Bulk returns a max of 1000.
                                    </p>
                                </b-message>
                            </div>
                        </div>

                        <div class="column is-6">
                            <div class="field-label is-normal">
                                <label for="inputs" class="label">Search Params</label>
                                <b-field>
                                    <b-input id="search" v-model="query.search" placeholder="Search.."> </b-input>
                                </b-field>
                                <b-field>
                                    <b-input
                                        type="text"
                                        id="video_id"
                                        v-model="query.video_id"
                                        placeholder="enter a video id"
                                    >
                                    </b-input>
                                </b-field>
                                <b-field v-show="query.searchType === 'Simple'">
                                    <b-input type="number" id="count" v-model.number="query.count" placeholder="0-25">
                                    </b-input>
                                </b-field>
                            </div>
                        </div>
                    </div>
                    <div class="columns is-mobile">
                        <div class="column is-half is-offset-one-fifth">
                            <b-button id="getCommentsBtn" @click="GetComments()">get comments</b-button>
                        </div>
                    </div>
                </form>
            </div>
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
                searchType: 'Simple',
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
                searchType: this.query.searchType == 'Simple' ? 'search' : 'bulk'
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
<style scoped lang="scss">

.form-wrapper {
    background-color: rgb(252, 251, 251);
    border-top: 1px solid lightgray;
    border-left: 1px solid lightgray;
    box-shadow: 3px 2px 1px 1px gray;
    border-radius: 0.25em;
    //margin: auto;
    padding: 2em;
    width: 50vw;
    display: block;
    margin-left: auto;
    margin-right: auto;
    .form-container {
        display: flex;
        justify-content: center;
        margin: auto;
    }
    .searchtype-message {
        float: right;
    }
}

@media screen and (max-width:700px){
.form-wrapper {
    background-color: rgb(252, 251, 251);
    border-top: 1px solid lightgray;
    border-left: 1px solid lightgray;
    box-shadow: 3px 2px 1px 1px gray;
    border-radius: 0.25em;
    //margin: auto;
    padding: 2em;
    width: 80vw;
    display: block;
    margin-left: auto;
    margin-right: auto;
}   
}
</style>

