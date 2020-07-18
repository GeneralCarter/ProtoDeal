<template>
  <FormulateForm 
    v-model="property"
    @submit="submitHandler">

    <FormulateInput
      name="propertyName"
      label="Property Name"
      validation="required"
    />

    <FormulateInput
      name="address"
      label="Address"
    />

    <div class="pform-row">
      <FormulateInput
        name="city"
        label="City"
      />

      <FormulateInput
        name="state"
        label="State"
      />

      <FormulateInput
        name="zip"
        label="Zip"
      />
    </div>


    <h3>Purchase</h3>

    <FormulateInput
      inputmode="numeric"
      type="currency"
      name="price"
      label="Purchase Price"
      validation="required|number"
    />

    <FormulateInput
      type="currency"
      name="closingCost"
      label="Closing Cost"
      validation="required|number"
    />


    <h3>Financing</h3>

    <FormulateInput
      type="percent"
      name="downPayment"
      label="Down Payment"
      validation="required|number|between:0,100"
    />


    <FormulateInput
        type="submit"
        label="Save"
    />
  </FormulateForm>
</template>


<script>
import axios from 'axios'

export default {
  name: "PropertyForm",
  data() {
      return {
          property: {
            propertyName: "test",
            price: 20000,
            downPayment: .16,
            closingCost: 7500
          }
      }
  },
  methods: {
    async submitHandler (data) {
      try {
        var test = data;
        axios.defaults.crossDomain = true;
        axios.defaults.headers.post['Access-Control-Allow-Origin'] = '*';
        const response = await axios.post("http://localhost:5000/Property/Save", data);
        this.property.id = response.data;
        this.$router.push({ name: 'editProperty', params: { id: this.property.id }});
      } catch (err) {
        console.log('secure api call failed');
      }
    }
  }
}
</script>

<style lang="css">
@import '../../node_modules/@braid/vue-formulate/dist/snow.css';

form.formulate-form {
  display: flex;
  flex-direction: column;
}

.formulate-input-wrapper {
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}

.pform-row {
  display: flex;
}

.formulate-input{
  margin-right: 1em;
}

h3 {
  color: #41b883;
  display: flex;
}
</style>