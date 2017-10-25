<template lang="pug">
 .footer
    hr
    footer
        p &copy; {{copyright}}

</template>

<script>

  module.exports = {
    name: 'botogp-footer',
    data() {
      return {
        copyright: '2017 - swizkon'
      }
    }
  } 
</script>