<template>
    <div>
        <h1>Settings</h1>

        <div>
            <b-button @click="clearComments">Clear All Comments</b-button>
        </div>
    </div>
</template>

<script>
export default {
    name: 'Settings',
    methods:{
clearComments() {
      this.$store.dispatch('emptyComments');
    },
    }
}
</script>