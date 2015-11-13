// Sets up the global settings for the bootstrap validation styling
ko.validation.init({
    errorElementClass: 'has-error',
    errorMessageClass: 'help-block',
    decorateInputElement: true,
    grouping: {
        deep: true,
        live: true,
        observable: true
    }
});