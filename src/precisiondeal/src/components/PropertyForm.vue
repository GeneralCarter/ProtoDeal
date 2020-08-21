<template>
  <FormulateForm 
    v-model="property"
    @submit="submitHandler">

    <h3>Description</h3>
    <div class="formulate-group">
      <FormulateInput
        name="propertyName"
        label="Property Name"
        validation="required"
      />
    </div>

    <h3>Address</h3>
    <div class="formulate-group">
      <FormulateInput
        name="streetAddress"
        label="Address"
      />

      <div class="formulate-row">
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
    </div>


    <h3>Purchase</h3>
    <div class="formulate-group">
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
    </div>

    <h3>Financing</h3>
    <div class="formulate-group">
      <FormulateInput
        type="percent"
        name="downPayment"
        label="Down Payment"
        validation="required|number|between:0,1"
      />

      <FormulateInput
        type="percent"
        name="interestRate"
        label="Interest Rate"
        validation="required|number|between:0,1"
      />

      <FormulateInput
        type="number"
        name="loanTerm"
        label="loanTerm"
        validation="required|number|between:1,60"
      />
    </div>

    <h3>Income</h3>
    <div class="formulate-group">
      <FormulateInput
        type="currency"
        name="grossRent"
        label="Gross Montly Rent"
        validation="required|number|between:1,1000000"
      />

      <FormulateInput
        type="percent"
        name="vacancy"
        label="Vacancy"
        validation="required|number|between:0,1"
      />
    </div>

    <h3>Expenses</h3>
    <div class="formulate-group">
      <FormulateInput
        type="percent"
        name="expensePercent"
        help="% of Rent"
        label="Total"
        validation="required|number|between:0,1"
      />
    </div>

    <h3>Growth Projections</h3>
    <div class="formulate-group">
      <FormulateInput
        type="percent"
        name="incomeGrowth"
        help="% per year"
        label="Income Growth"
        validation="required|number|between:0,1"
      />

      <FormulateInput
        type="percent"
        name="expenseGrowth"
        help="% per year"
        label="Expense Growth"
        validation="required|number|between:0,1"
      />

      <FormulateInput
        type="percent"
        name="appreciation"
        help="% per year"
        label="Appreciation"
        validation="required|number|between:0,1"
      />
    </div>

    <FormulateInput
        type="submit"
        label="Save"
    />
  </FormulateForm>
</template>


<script>
import axios from 'axios'
import { mapState } from 'vuex'
import types from '../types'

export default {
  name: "PropertyForm",
  data() {
    return {
      property: {}
    }
  },
  watch: {
    selectedProperty: function() {
      this.property = this.selectedProperty
    }
  },
  computed: mapState([
    "selectedProperty"
  ]),
  methods: {
    async submitHandler (data) {
      try {
        axios.defaults.crossDomain = true;
        axios.defaults.headers.post['Access-Control-Allow-Origin'] = '*';
        var idParam = this.property.id ? "/" + this.property.id : "";
        data.id = this.property.id;
        const response = await axios.post("http://localhost:5000/properties/save" + idParam, data);
        this.property.id = response.data;
        this.$store.dispatch(types.SET_CURRENT_PROPERTY, this.property)
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
  align-items: center;
  flex: 100%;
}

.formulate-row {
  display: flex;
}

.formulate-input {
  margin-right: 1em;
  display: flex;
}

.formulate-group {
    background-color: white;
    box-shadow: #00000054 1px 1px 2px 1px;
    padding: .9em;
}

.formulate-input .formulate-input-label {
  margin-right: 0.5em;
  flex: 165px 0 1;
  text-align: left;
}

.formulate-row .formulate-input .formulate-input-label {
  margin-right: 0.5em;
  flex: auto 0 1;
  text-align: left;
}

.formulate-row .formulate-input:last-child {
    margin-bottom: 1.5em;
}

.formulate-input-element--submit {
  margin-top: 2em;
}


h3 {
  color: #41b883;
  display: flex;
}
</style>