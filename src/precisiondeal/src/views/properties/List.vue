<template>
  <div>
    <router-link :to="{ name: 'newProperty' }">Create New</router-link>

    <div class="property-list_continaer" >
      <propertyListItem
        v-for="property in properties"
        :key="property.id"
        v-bind:property="property"
      ></propertyListItem>
    </div>
  </div>
</template>

<script>
import axios from 'axios'
import PropertyListItem from '../../components/PropertyListItem'
import PropertyModel from '../../models/property'

export default {
  name: 'PropertyList',
  components: {
    PropertyListItem
  },
  data () {
    return {
      properties: [],
      loading: false
    }
  },
  created () {
    this.fetchData()
  },
  methods: {
    async fetchData () {
      const response = await axios.get('http://localhost:5000/properties')
      this.properties = response.data.map((dto) => new PropertyModel(dto))
    }
  }
}
</script>

<style>
  .property-list_continaer {
      margin-top: 30px;
  }
</style>
