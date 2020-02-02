<template>
    <div class="DefaultContainer">
        <b-button @click="toggleLoading">toogle loading</b-button>
        <b-loading class="loading" :is-full-page="isFullPage" :active.sync="loading.isLoading" :can-cancel="false">
            <b-icon id="icon" pack="fas" icon="sync-alt" size="is-large" custom-class="fa-spin"> </b-icon>
        </b-loading>
            <Search />
        <Explorer />
    </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex';
import Explorer from '@/components/explorer.vue';
import Search from '@/components/search.vue';

export default {
    name: 'DefaultContainer',
    data() {
        return {
            isFullPage: true
        };
    },
    computed: {
        ...mapState({
            user: 'user',
            loading: 'loading'
        })
    },
    methods: {
        toggleLoading() {
            //this.$store.dispatch('ChangeLoading');
            this.$store.dispatch('emptyComments');
        }
    },
    components: { Explorer, Search }
};
</script>
<style lang="scss">
// Import Bulma's core
@import url('https://use.fontawesome.com/releases/v5.2.0/css/all.css');

// Import Bulma's core
@import '~bulma/sass/utilities/_all';

// Set your colors
$primary: #8c67ef;
$primary-invert: findColorInvert($primary);
$twitter: #4099ff;
$twitter-invert: findColorInvert($twitter);

// Setup $colors to use as bulma classes (e.g. 'is-twitter')
$colors: (
    'white': (
        $white,
        $black
    ),
    'black': (
        $black,
        $white
    ),
    'light': (
        $light,
        $light-invert
    ),
    'dark': (
        $dark,
        $dark-invert
    ),
    'primary': (
        $primary,
        $primary-invert
    ),
    'info': (
        $info,
        $info-invert
    ),
    'success': (
        $success,
        $success-invert
    ),
    'warning': (
        $warning,
        $warning-invert
    ),
    'danger': (
        $danger,
        $danger-invert
    ),
    'twitter': (
        $twitter,
        $twitter-invert
    )
);

// Links
$link: $primary;
$link-invert: $primary-invert;
$link-focus-border: $primary;

// Import Bulma and Buefy styles
@import '~bulma';
@import '~buefy/src/scss/buefy';

.loading {
    backdrop-filter: blur(1px);
    background-color: rgba(0, 157, 255, 0.2);
    .icon {
        color: navy;
    }
}

// my style
</style>