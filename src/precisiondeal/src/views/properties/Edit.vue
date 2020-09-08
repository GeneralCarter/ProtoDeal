<template>
  <div>
    <h2>Edit Property</h2>

    <PropertyForm />
  </div>
</template>

<script>
import PropertyForm from '../../components/VPropertyForm'
import axios from 'axios'
import types from '../../types'

export default {
  name: 'EditProperty',
  components: {
    PropertyForm
  },
  async beforeRouteEnter (to, from, next) {
    const response = await axios.get('http://localhost:5000/properties/' + to.params.id)

    next(vm => {
      vm.$store.dispatch(types.SET_CURRENT_PROPERTY, response.data)
    })
  }
}
</script>
