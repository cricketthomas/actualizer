<template>
    <div id="wrapper">
        <fieldset>
            <form @submit.prevent="SearchComments()">
                <label>
                    Enter a search term:
                    <input type="text" id="search" v-model="query.search" required />
                </label>
                <label>
                    Enter Video ID:
                    <input type="text" id="video_id" v-model="query.video_id" />
                </label>
                <label>
                    Count:
                    <input type="number" id="count" v-model.number="query.count" />
                </label>
                <button>Submit</button>
            </form>
            <button @click="storeGetComments()">test comments in store</button>
        </fieldset>
        {{ results }}
    </div>
</template>

<script>
export default {
    data() {
        return {
            userClaims: null,
            results: '',
            query: {
                search: 'love',
                video_id: 'SsKT0s5J8ko',
                count: 2
            }
        };
    },
    methods: {
        async SearchComments() {
            //TODO: put this in a getter.

            this.$http
                .get(
                    `comments/search?video_id=${this.query.video_id}&search=${this.query.search}&count=${this.query.count}`
                )
                .then(response => {
                    this.results = response.data;
                })
                .catch(e => {
                    console.error(e);
                });

            //this.results = response;
            //console.log(response);
        },
        storeGetComments(){
             this.$store.dispatch('basicSearch',{
                video_id:this.query.video_id,
                search: this.query.search,
                count: this.query.count
            })
        }
    }
};
</script>
<style scoped>
</style>

