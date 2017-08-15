/*
 treeMenu - jQuery plugin
 version: 0.4

 Copyright 2014 Stepan Krapivin

*/

(function($){
    $.fn.openActive = function(activeSel) {
        activeSel = activeSel || ".active";

        var c = this.attr("class");

        this.find(activeSel).each(function(){
            var el = $(this).parent();
            while (el.attr("class") !== c) {
                if(el.prop("tagName") === 'UL') {
                    el.show();
                } else if (el.prop("tagName") === 'LI') {
                    el.removeClass('tree1-closed');
                    el.addClass("tree1-opened");
                }

                el = el.parent();
            }
        });

        return this;
    }

    $.fn.tree1menu = function(options) {
        options = options || {};
        options.delay = options.delay || 0;
        options.openActive = options.openActive || false;
        options.activeSelector = options.activeSelector || "";

        this.addClass("tree1menu");
        this.find("> li").each(function() {
            e = $(this);
            var subtree1 = e.find('> ul');
            var button = e.find('span').eq(0).addClass('toggler');

            if( button.length == 0) {
                var button = $('<span>');
                button.addClass('toggler');
                e.prepend(button);
            } else {
                button.addClass('toggler');
            }

            if(subtree1.length > 0) {
                subtree1.hide();

                e.addClass('tree1-closed');

                e.find(button).click(function() {
                    var li = $(this).parent('li');
                    li.find('> ul').slideToggle(options.delay);
                    li.toggleClass('tree1-opened');
                    li.toggleClass('tree1-closed');
                    li.toggleClass(options.activeSelector);
                });

                $(this).find('> ul').tree1menu(options);
            } else {
                $(this).addClass('tree1-empty');
            }
        });

        if (options.openActive) {
            this.openActive(options.activeSelector);
        }

        return this;
    }
})(jQuery);

$(document).ready(function(){
  $("#qiehuan").click(function(){
  $("#tuceng").toggle();
  });
});

$(function(){
        $(".tree1").tree1menu({delay:300}).openActive();
    });
