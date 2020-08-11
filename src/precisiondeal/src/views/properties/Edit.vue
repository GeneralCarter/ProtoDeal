<template>
  <div>
    <h2>Edit Property</h2>

    <PropertyForm v-bind:model="property" />
  </div>
</template>

<script>
import PropertyForm from '../../components/PropertyForm';
import axios from 'axios'

export default {
  name: "EditProperty",
  components: {
    PropertyForm
  },
  data () {
    return {
      property: null
    }
  },
  async beforeRouteEnter (to, from, next) {
    const response = await axios.get("http://localhost:5000/properties/" + to.params.id);
    next(vm => vm.setData(response.data));
  },
  methods: {
    setData ( property) {
      this.property = property;
    }
  }
}
</script>