$('.typeahead').typeahead()
$('.typeahead').typeahead({
    source: function (query, process) {
        return $.get('/Typeahead', { query: query }, function (data) {
            return process(data.Name.Value);
        });
    }
});
