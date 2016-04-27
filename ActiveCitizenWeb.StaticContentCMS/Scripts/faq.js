window.ag = window.ag || {};
window.ag.faq = (function () {
    var _settings;

    var _defaultSettings = {
        richTextEditorAttribute: 'data-richtexteditor',
        confirmButtonAttribute: 'data-confirmation'
    };

    var $;

    var _confirmAction = function (message) {
        return confirm(message);
    }

    var _initTinyMce = function (editorSelector) {
        if (!window.tinymce && console.log) {
            console.log("Tinymce javascript library is not referenced on the page.");
            return;
        }

        tinymce.init({
            selector: editorSelector,
            plugins: [
                "advlist autolink lists link image charmap preview anchor",
                "searchreplace visualblocks code fullscreen",
                "insertdatetime table contextmenu paste"
            ],
            toolbar: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image"
        });
    }

    var _initialize = function (settings) {
        $ = jQuery;
        _settings = $.extend({}, settings || {}, _defaultSettings);

        // Init confirmations
        var attr = _settings.confirmButtonAttribute;
        $('[' + attr + ']').each(function () {

            var $el = $(this);
            var message = $el.attr(attr);

            $el.data('onclick-saved-action', this.onclick);
            $el.attr('onclick', null);

            $el.click(function (event) {
                var proceed = _confirmAction(message);
                if (!proceed) {
                    event.stopImmediatePropagation();
                    event.preventDefault();
                    return false;
                } else {
                    var action = $el.data('onclick-saved-action');
                    $el.attr('onclick', action);
                    return true;
                }
            });
        });

        // Init rich-text editor
        var attr2 = _settings.richTextEditorAttribute;
        _initTinyMce('textarea[' + attr2 + ']');
    };

    return {
        initialize: _initialize
    };

})();
