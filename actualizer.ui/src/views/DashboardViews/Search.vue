<template>
    <div class="search">
        <Fetch />
        <TableList />
    </div>
</template>

<script>
import Fetch from '@/components/Fetch.vue';
import TableList from '@/components/TableList.vue';
export default {
    name: 'Search',
    components: {
        Fetch, TableList
    }
};
</script>