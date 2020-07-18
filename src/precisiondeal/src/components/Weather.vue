<template>
    <div>
        <button v-on:click="load">load</button>
    </div>
</template>

<script>
import authService from '../services/authService'

export default {
    name: "weatherList",
    methods: {
        load: function() {
            console.log(authService);
            authService.userManager.getUser().then(function (user) {
                var url = "http://localhost:5000/WeatherForecast";

                var xhr = new XMLHttpRequest();
                xhr.open("GET", url);
                xhr.onload = function () {
                    //log(xhr.status, JSON.parse(xhr.responseText));
                }
                xhr.setRequestHeader("Authorization", "Bearer " + authService.getToken());
                xhr.send();
            });
        }
    }

}
</script>