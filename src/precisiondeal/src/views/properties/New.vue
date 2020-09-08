<template>
  <div>
    <h3>New Property</h3>

    <PropertyForm/>
  </div>
</template>

<script>
import PropertyForm from '../../components/VPropertyForm'
export default {
  name: 'NewProperty',
  components: {
    PropertyForm
  },
  beforeRouteUpdate (to, from, next) {
    console.log(to)
  }
}
</script>
