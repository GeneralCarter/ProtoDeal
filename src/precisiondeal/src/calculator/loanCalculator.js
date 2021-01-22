import { mapState } from 'vuex'

export default {
  name: "Calculator",
  computed: {
    testTerm: function () {
      return this.property.loanTerm + 'test'
    },
    ...mapState({
      property: 'selectedProperty'
    })
  },
  mounted () {
    
  },
  beforeDestroy () {
    
  },
  render: () => null,
}
