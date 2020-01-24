<template>
    <div>
        <b-navbar>
            <template slot="brand">
                <b-navbar-item tag="router-link" :to="{ path: '/' }">
                    <img src="/img/icons/actualizer_web.png" alt="actualizer logo"
                /></b-navbar-item>
            </template>
            <template slot="start">
                <b-navbar-item tag="router-link" :to="{ path: '/' }">Home</b-navbar-item>
                <b-navbar-item tag="router-link" :to="{ path: '/about' }">About</b-navbar-item>
            </template>
            <template slot="end">
                <b-navbar-item tag="div">
                    <div class="buttons">
                        <a class="button is-primary">
                            <strong v-if="authenticated" @click="logout()" id="logout-button"> sign out</strong>
                            <strong v-else @click="login()" id="login-button">log in</strong>
                        </a>
                    </div>
                </b-navbar-item>
            </template>
        </b-navbar>
    </div>
</template>

