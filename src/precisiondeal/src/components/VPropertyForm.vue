<template>
  <v-form 
    ref="form">

    <h3>Description</h3>
    <v-container>
      <v-text-field v-model="property.propertyName"
        label="Property Name"
        required
      ></v-text-field>
    </v-container>

    <h3>Address</h3>
    <v-container>
      <v-text-field v-model="property.streetAddress"
        label="Address"
      ></v-text-field>

      <v-row>
        <v-col cols="12" sm="4">
          <v-text-field v-model="property.city"
            label="City"
          ></v-text-field>
        </v-col>

        <v-col cols="12" sm="4">
          <v-text-field v-model="property.state"
            label="State"
          ></v-text-field>
        </v-col>

        <v-col cols="12" sm="4">
          <v-text-field v-model="property.zip"
            label="Zip"
          ></v-text-field>
        </v-col>
      </v-row>
    </v-container>

    <h3>Purchase</h3>
    <v-container>
      <v-text-field v-model.number="property.price"
        label="Price"
        required
        type="number"
        prefix="$"
      ></v-text-field>

      <v-text-field v-model.number="property.closingCost"
        label="Closing Cost"
        required
        type="number"
        prefix="$"
      ></v-text-field>
    </v-container>

    <h3>Financing</h3>
    <v-container>
      <v-text-field v-model.number="property.downPayment"
        label="Down Payment"
        required
        type="number"
        suffix="%"
      ></v-text-field>

      <v-text-field v-model.number="property.interestRate"
        label="Interest Rate"
        required
        type="number"
        suffix="%"
      ></v-text-field>

      <v-text-field v-model.number="property.loanTerm"
        label="Loan Term"
        required
        type="number"
      ></v-text-field>
    </v-container>

    <h3>Income</h3>
    <v-container>
      <v-text-field v-model.number="property.grossRent"
        label="Gross Montly Rent"
        required
        type="number"
        prefix="$"
      ></v-text-field>

      <v-text-field v-model.number="property.vacancy"
        label="Vacancy"
        required
        type="number"
        suffix="%"
      ></v-text-field>
    </v-container>

    <h3>Expenses</h3>
    <v-container>
      <v-text-field v-model.number="property.expensePercent"
        label="Total"
        required
        type="number"
        suffix="%"
      ></v-text-field>
    </v-container>

    <h3>Growth Projections</h3>
    <v-container>
      <v-row>
        <v-col cols="12" sm="4">
          <v-text-field v-model.number="property.incomeGrowth"
            label="Income Growth"
            required
            type="number"
            hint="% per year"
            persistent-hint
            suffix="%"
          ></v-text-field>
        </v-col>

        <v-col cols="12" sm="4">
          <v-text-field v-model.number="property.expenseGrowth"
            label="Expense Growth"
            required
            type="number"
            hint="% per year"
            persistent-hint
            suffix="%"
          ></v-text-field>
        </v-col>

        <v-col cols="12" sm="4">
          <v-text-field v-model.number="property.appreciation"
            label="Appreciation"
            required
            hint="% per year"
            persistent-hint
            type="number"
            suffix="%"
          ></v-text-field>
        </v-col>
      </v-row>


    </v-container>

    <v-btn @click="submitHandler">submit</v-btn>

  </v-form>
</template>

<script>
import axios from 'axios'
import { mapState } from 'vuex'
import types from '../types'

export default {
  name: 'PropertyForm',
  created () {
    this.property = this.selectedProperty || {}
  },
  data () {
    return {
      property: {}
    }
  },
  watch: {
    selectedProperty: function () {
      this.property = this.selectedProperty
    }
  },
  computed: mapState([
    'selectedProperty'
  ]),
  methods: {
    async submitHandler (data) {
      try {
        var params = this.property.id ? { id: this.property.id } : null
        const response = await axios.post('http://localhost:5000/properties/save' + idParam, this.property, { params })
        this.property.id = response.data
        this.$store.dispatch(types.SET_CURRENT_PROPERTY, this.property)
        if (this.$router.currentRoute.params.id != this.property.id) {
          this.$router.push({ name: 'editProperty', params: { id: this.property.id } })
        };
      } catch (err) {
        console.log('secure api call failed: ' + err.message)
      }
    }
  }
}
</script>

<style lang="css">
@import '../../node_modules/@braid/vue-formulate/dist/snow.css';

form.v-form .container {
  background-color: white;
  box-shadow: #00000054 1px 1px 2px 1px;
}

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
