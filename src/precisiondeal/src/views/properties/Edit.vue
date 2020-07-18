<template>
  <div>
    <h3>Edit Property</h3>

    <PropertyForm/>
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
  async beforeRouteEnter (to, from, next) {
    const response = await axios.get("http://localhost:5000/Property/" + to.params.id);
    next(vm => vm.setData(response.data));
  },
  methods: {
    setData ( property) {
      this.property = property;
    }
  }
}
</script>