Gq.define(['jquery'], function (exports) {
    "use strict";

    var $ = Gq.jquery,
        win = Gq.win,
        doc = win.document,
        key = 'contextmenu-view';

    var Contextmenu = function () {
    };

    Contextmenu.prototype.pc = function () {
        var $trs = $('table.grid tbody tr');
        if ($trs.length === 0) return;

        var $menu = $('body:first').append('<div id="' + key + '" class="gq-radius-little gq-shadow-little" oncontextmenu="return false"></div>').find('#' + key);
        $trs.bind('contextmenu', function (e) {
            $trs.removeClass('selected');
            var $links = $(this).addClass('selected').children('td.actions').find('a').clone(true);
            if ($links.length === 0) {
                hide();
                return false;
            }
            $links.appendTo($menu.empty());

            var winw = $(win).width(),
                winh = $(win).height(),
                meuw = $menu.outerWidth(true),
                meuh = $menu.outerHeight(true),
                mleft = e.pageX,
                mtop = e.pageY;
            mleft -= mleft + meuw > winw ? meuw : 0;
            mtop -= mtop + meuh > winh ? meuh : 0;
            if (mtop < 0) {
                mtop = 0;
            }
            //显示
            $menu.css({ left: mleft, top: mtop }).fadeIn(350).find('a').each(function (j) {
                $(this, $menu).bind('click', function () {
                    hide();
                });
            });
            //debug
            $(doc).one('click', hide);
            $(doc).one('contextmenu', hide);
            $(doc).one('resize', hide);
            Gq.stope(e);
            return false;
        });
        function hide() {
            $trs.removeClass('selected');
            $menu.fadeOut(0).find('a').remove();
        }
    };

    Contextmenu.prototype.mobile = function () {
        var $trs = $('table.grid tbody tr'),
            $cma = $('#shortcuts-view a.contextmenu');
        if ($trs.length === 0) return;

        var $menu = $('body:first').append('<div id="' + key + '" class="gq-radius-little gq-shadow-little" oncontextmenu="return false"></div>').find('#' + key);
        $trs.bind('mousedown', function (e) {
            if (e.which !== 1 && e.which !== 3) return;
            $(this).addClass('selected').siblings().removeClass('selected');
        });
        $cma.bind('click', function () {
            var $tr = $trs.filter('tr.selected'),
                $links = $tr.find('td.actions').find('a').clone(true);
            if ($links.length === 0) {
                hide();
                return false;
            }
            $links.appendTo($menu.empty());
            //显示
            $menu.fadeIn(350, function () {
                $(doc).one('click', hide);//DEBUG
            }).find('a').each(function (j) {
                $(this, $menu).bind('click', function () {
                    hide();
                });
            });
            return false;
        });
        function hide() {
            $trs.removeClass('selected');
            $menu.fadeOut(0).find('a').remove();
        }
    };

    var contextmenu = new Contextmenu();

    //初始化
    if (Gq.device.isMoblie) {
        contextmenu.mobile();
    }
    else {
        contextmenu.pc();
    }

    exports('contextmenu', contextmenu);
});