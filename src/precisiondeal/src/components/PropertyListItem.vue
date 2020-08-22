<template>
  <div class="property-list_item" v-on:click="onClick">
    <span>{{ property.propertyName }}</span>
    <div class="property-list_item-price">
      Purchase Price
      <span>{{ property.price }}</span>
    </div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'PropertyListItem',
  props: ['property'],
  methods: {
    onClick: function () {
      this.$router.push({ name: 'editProperty', params: { id: this.property.id } })
    }
  }
}
</script>

<style>
  .property-list_item {
    padding: 10px;
    display: flex;
    background-color: #97d8bb38;
    margin: 10px 0;
    justify-content: space-between;
    cursor: pointer;
    align-items: center;
  }
  .property-list_item-price {
    display: flex;
    flex-direction: column;
    color: #2084f9;
    font-size: .8em;
  }
  .property-list_item-price span {
    font-size: 2em;
  }
</style>
