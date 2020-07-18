<template>
    <div class="g-signin2" id="google-signin-button" data-onsuccess="onSignIn"></div>

    <!-- <div>
        <button id="login" v-on:click="login">Login</button>
    </div> -->
</template>

<script>
import authService from '../../services/authService'

export default {
    name: 'LoginButton',
    mounted() {
        gapi.signin2.render('google-signin-button', {
            onsuccess: this.onSignIn
        })
    },
    methods: {
        login: function() {
            authService.userManager.signinRedirect();
        },
        onSignIn: function(user) {
            authService.onSignIn(user);
        }
    },
}

//document.getElementById("login").addEventListener("click", login, false);
// document.getElementById("api").addEventListener("click", api, false);
// document.getElementById("logout").addEventListener("click", logout, false);

function onSignIn(googleUser) {
    var id_token = googleUser.getAuthResponse().id_token;
}

function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}
</script>