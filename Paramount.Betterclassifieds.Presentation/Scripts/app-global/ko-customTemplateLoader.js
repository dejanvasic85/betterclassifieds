/*
 * This allows ko components to look like the following where we can specify the full path to the template html:
 * ko.components.register('ticket-selection', {
        viewModel: ...,
        template: { path: $paramount.baseUrl + '/Scripts/app/events/ticketSelection/ticket-selection.html' }
    });
 */
ko.components.loaders.unshift({
    loadTemplate: function (name, templateConfig, callback) {
        if (templateConfig.path) {
            $.get(templateConfig.path).then(function (markup) {
                ko.components.defaultLoader.loadTemplate(name, markup, callback);
            });
        } else {
            callback(null); // continue process knockout stuff...
        }
    }
});
