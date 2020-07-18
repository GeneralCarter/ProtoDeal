<template>
  <div
    :class="`formulate-input-element formulate-input-element--${context.type}`"
    :data-type="context.type"
  >
   <input type="text" v-model="displayValue" v-bind="context.attributes" @blur="isInputActive = false" @focus="isInputActive = true"/>
  </div>
</template>

<script>
export default {
  props: {
    context: {
      type: Object,
      attributes: {
        '@blur': function() {
          this.isInputActive = false;
        },
        focus: function() {
          this.isInputActive = true;
        }
      },
      model: 0
    }
  },
  data: function() {
    return {
        isInputActive: false,
        formatter: new Intl.NumberFormat('en-US', { style: 'percent' }),
        value: this.context.model
    }
  },
  watch: {
    value(newValue) {
      this.context.model = newValue;
    }
  },
  computed: {
    displayValue: {
        get: function() {
            if (this.isInputActive) { // Editing mode
                return Math.floor(this.value * 100);
            } else {
                return this.formatter.format(this.value);
            }
        },
        set: function(modifiedValue) {
            let newValue = parseFloat(modifiedValue.replace(/[^\d\.]/g, "")); // remove none numbers

            if (isNaN(newValue)) {
                newValue = 0
            }

            this.value = newValue / 100;
        }
    }
  }
}
</script>