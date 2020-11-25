Gq.define(['jquery'], function (exports) {
    "use strict";

    var $ = Gq.jquery,
        win = Gq.win;

    var Festure = function () { };

    Festure.prototype.config = {
        pIsPreview: false
    };

    Festure.prototype.cache = [];

    Festure.prototype.bubble = function () {
        var $elems = $('[gq-bubble]');
        if ($elems.length === 0) return;

        var bubbleKey = 'bubble-view';
        var $bubble = $('body:first').append('<div id="' + bubbleKey + '" class="gq-radius-little gq-shadow-little" oncontextmenu="return false"></div>').find('#' + bubbleKey);
        $elems.hover(function (e) {
            var $this = $(this),
                _tw = $this.outerWidth(true),
                _left = $this.offset().left,
                _top = $this.offset().top,
                _text = $this.attr('gq-bubble'),
                _bw = $bubble.text(_text).outerWidth(true),
                _bw2 = $bubble.text(_text).width(),
                _bh = $bubble.outerHeight(true) + 5;

            if (_text === undefined || _text.length === 0) {
                $bubble.fadeTo(100, 0, function () {
                    $(this).css({ left: -999, top: -999 });
                });
                return;
            }

            _left = Math.max(_left - _bw / 2 + _tw / 2, 0);
            _top = Math.max(_top - _bh, 0);

            $bubble.css({ left: _left, top: _top, width: _bw2 }).stop(true, true).fadeTo(300, 1);
        }, function () {
            $bubble.fadeTo(100, 0, function () {
                $(this).css({ left: -999, top: -999, width: 'auto' });
            });
        });
    };

    Festure.prototype.preview = function () {
        var that = this,
            $elems = $('[gq-preview]');
        if ($elems.length === 0) return;

        var previewKey = 'preview-view';
        var $preview = $('body:first').append('<div id="' + previewKey + '" class="gq-radius-little gq-shadow-little" oncontextmenu="return false"></div>').find('#' + previewKey);
        $elems.hover(function (e) {
            if (that.config.pIsPreview) return;
            that.config.pIsPreview = true;
            var $this = $(this),
                _tw = $this.outerWidth(true),
                _th = $this.outerHeight(true),
                _left = $this.offset().left,
                _top = $this.offset().top,
                _text = $this.attr('gq-preview'),
                _array = _text && _text.split('|');

            if (_array === undefined || _array.length === 0) {
                $preview.fadeTo(100, 0, function () {
                    $(this).css({ left: -999, top: -999 }).empty();
                });
                return;
            }

            for (var i = 0; i < that.cache.length; ++i) {
                if (that.cache[i].tag === _text) {
                    _init(that.cache[i].data);
                    return;
                }
            }

            $.ajax({
                url: _array[1],
                method: _array[0],
                beforeSend: function (xhr) {
                    $preview.empty();
                },
                success: function (res) {
                    if (!res || Object.keys(res).length === 0) return;
                    that.cache.push({ tag: _text, data: res });
                    _init(res);
                },
                error: function (e) {
                    console.log(e);
                }
            });

            function _init(res) {
                if (!that.config.pIsPreview) return;
                var _tmp = '<div class="' + res.Flag + '"><dl><dt>' + res.Name + '</dt><dd>' + res.Status + '</dd><dd>' + res.Remark + '</dd></dl>';
                for (var kp in res.Data) {
                    _tmp += '<p><span>' + kp + '</span><span>' + res.Data[kp] + '</span></p>';
                }
                $preview.html(_tmp + '</div>');

                var _winh = $(win).height(),
                    _pw = $preview.outerWidth(true),
                    _ph = $preview.outerHeight(true),
                    _pl = Math.max(_left + _tw / 2 - _pw / 2, 0),
                    _pt = _top + _th + 10;

                $preview.css({ left: _pl, top: _pt }).stop(true, true).fadeTo(300, 1);
            }
        }, function () {
            that.config.pIsPreview = false;
            $preview.fadeTo(100, 0, function () {
                $(this).css({ left: -999, top: -999 }).empty();
            });
        });
    };

    Festure.prototype.filter = function () {
        var $view = $('#shortcuts-view'),
            $all = $view.find('a.all'),
            $link = $view.children('a.link');

        if ($all.length === 0 || $link.length === 0) return;

        var filterKey = 'link-view';
        var $filter = $('body:first').append('<div id="' + filterKey + '" class="gq-radius-little gq-shadow-little" oncontextmenu="return false"></div>').find('#' + filterKey).append($link);

        Gq.use('popup', function (p) {
            $all.on('click', function (e) {
                $filter.toggle(300);
                Gq.stope(e);
                return false;
            });
        });
    };

    Festure.prototype.search = function () {
    };

    var gesture = new Festure();

    if (!Gq.device.isMoblie) {
        gesture.bubble();
        gesture.preview();
    }
    else {
        gesture.filter();
        gesture.search();
    }

    exports('gesture', gesture);
});