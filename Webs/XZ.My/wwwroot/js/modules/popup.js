Gq.define(['jquery'], function (exports) {
    "use strict";

    var $ = Gq.jquery,
        win = Gq.win,
        topWin = null,
        doc = win.document,
        topDoc = null;

    try { topWin = win.top; topDoc = topWin.document; }
    catch (e) { topWin = win; topDoc = topWin.document; }

    var Popup = function () {
    };

    var config = Popup.prototype.config = {
        loadKey: 'loading-view',
        alertKey: 'alert-view',
        maskKey: 'mask-view',
        dialogKey: 'dialog-view',
        tipsKey: 'tips-view'
    };

    Popup.prototype.loading = function (timer) {
        if (timer <= 0) {
            closeLoading();
            return;
        }
        var $body = $('body:first', doc),
            $load = $body.append('<div id="' + config.loadKey + '" class="gq-loading"><div><span></span><span></span><span></span><span></span><span></span></div></div>').find('#' + config.loadKey),
            $submit = $('from .gq-submit', doc);
        $submit.attr('disabled', 'disabled').focus();
        setTimeout(closeLoading, timer || 60000);

        function closeLoading() {
            var $load = $('#' + config.loadKey, doc);
            $load.fadeOut(200, function () {
                $load.remove();
                $('from .gq-submit', doc).removeAttr('disabled');
            });
        }
    };
    Popup.prototype.tips = function (msg, callback) {
        closeTips();
        var $body = $('body:first', doc),
            $tips = $body.append('<div id="' + config.tipsKey + '" class="gq-tips-center"><span class="gq-radius-little gq-shadow gq-noselect">' + msg + '</span></div>').find('#' + config.tipsKey).fadeIn();
        $tips.one('click', closeTips);
        setTimeout(closeTips, 3500);

        function closeTips() {
            var $tips = $('#' + config.tipsKey, doc);
            $tips.stop(true, true).fadeOut(200, function () {
                $tips.remove();
                Gq.isFunction(callback) && callback.call();
            });
        }
    };
    Popup.prototype.alert = function (options) {
        var that = this;
        options = Gq.isString(options) ? { content: options } : (options || {});
        var $body = $('body:first', doc),
            $mask = $body.append('<div id="' + config.maskKey + '" class="gq-mask gq-noselect"></div>').find('#' + config.maskKey),
            $alert = $body.append('<div id="' + config.alertKey + '" class="gq-dialog gq-alert gq-shadow gq-radius-little gq-noselect"><div class="gq-alert-title"></div><div class="gq-alert-content"></div><div class="gq-alert-buttons"></div></div>').find('#' + config.alertKey),
            $title = $alert.find('.gq-alert-title').append('<span>' + (options.title || '提示') + '</span>'),
            $content = $alert.find('.gq-alert-content').append(options.content),
            $button = $alert.find('.gq-alert-buttons').append('<a href="javascript:;" class="gq-alert-buttons-ok gq-radius-little"></a><a href="javascript:;" class="gq-alert-buttons-cancel gq-radius-little"></a>'),
            $ok = $button.find('.gq-alert-buttons-ok').append((options.buttons && options.buttons.ok) || '确定'),
            $cancel = $button.find('.gq-alert-buttons-cancel').append((options.buttons && options.buttons.cancel) || '取消');
        if (options.canClose) {
            var $close = $title.append('<a href="javascript:;" class="gq-rotate gq-icon">&#xe792;</a>').find('a');
            $close.one('click', that.closeAlert);
        }
        if (options.callback && options.callback.ok) {
            $ok.one('click', function () {
                that.closeAlert();
                options.callback.ok();
            });
        } else {
            $ok.one('click', that.closeAlert);
        }
        if (options.callback && options.callback.cancel) {
            $cancel.one('click', function () {
                that.closeAlert();
                options.callback.cancel();
            });
        } else {
            $cancel.one('click', that.closeAlert);
        }
        $mask.fadeIn(150, function () {
            $alert.fadeIn(150);
        });
    };
    Popup.prototype.closeAlert = function () {
        var $body = $('body:first', doc),
            $mask = $body.find('#' + config.maskKey),
            $alert = $body.find('#' + config.alertKey);
        $alert.animate({
            top: 0,
            'margin-top': -30 - $alert.outerHeight(true)
        }, 350, function () {
            $mask.fadeOut(450, function () {
                $alert.remove();
                $mask.remove();
            });
        });
    };
    Popup.prototype.maskDialog = function (options) {
        options = options || {};
        var $body = $('body:first', doc),
            $mask = $body.append('<div id="' + config.maskKey + '" class="gq-mask gq-noselect"></div>').find('#' + config.maskKey),
            $dialog = $body.append('<div id="' + config.dialogKey + '" class="gq-dialog gq-shadow gq-radius-little"></div>').find('#' + config.dialogKey),
            $title = $dialog.append('<div id="dialog-title-view" class="gq-dialog-title"><span class="gq-singleline">' + (options.loadingTitle || '加载中…') + '</span><a class="gq-icon gq-rotate" href="javascript:;">&#xe792;</a></div>').find('#dialog-title-view'),
            $title_text = $title.find('span'),
            $close = $title.find('a'),
            $loading = $dialog.append('<div id="dialog-loading-view" class="gq-dialog-loading"><img alt="loading" src="' + Gq.config.res1 + '/images/loading.gif"/></div>').find('#dialog-loading-view'),
            $content = $dialog.append('<div id="dialog-content-view" class="gq-dialog-content"></div>').find('#dialog-content-view');
        if (options['class']) {
            $dialog.addClass(options['class']);
        }
        if (options.style) {
            $dialog.css(options.style);
        }
        if (options.url) {
            if (options.url.indexOf('mode=dialog') === -1) options.url += (options.url.indexOf('?') !== -1 ? '&' : '?') + 'mode=dialog';
            var $frame = $content.append('<iframe id="dialog-content-iframe" name="dialog-content-iframe" allowtransparency="true" scrolling="auto" frameborder="0" src="about:blank"></iframe>').find('#dialog-content-iframe').attr('src', options.url);
            $frame.on('load', function (e) {
                var $fc = $(this).contents(),
                    $ft = $fc.find('h1').eq(0),
                    $fb = $fc.find("body").children(':visible').eq(0),
                    width = $fb.outerWidth(true),
                    height = $fb.outerHeight(true) + $title.outerHeight(true),
                    maxW = $(win).width(),
                    maxH = $(win).height(),
                    title = $ft.text();
                //防超出
                width = Math.min(width, maxW * 0.9);
                height = Math.min(height, maxH * 0.9);
                //尺寸动画
                $dialog.animate({
                    width: width,
                    height: height,
                    'margin-left': -width / 2,
                    'margin-top': -height / 2
                }, 350, function () {
                    //显示切换
                    $title_text.text(options.title || title);
                    $loading.remove();
                });
                $fc.one('keydown', function (ee) {
                    if (ee.keyCode === 27) {
                        window.top.Gq.popup.closeDialog();
                    }
                });
            });
        } else {
            $content.append(options.content);
            var width = $content.children().outerWidth(true),
                height = $content.children().outerHeight(true) + $title.outerHeight(true);
            $dialog.animate({
                width: width,
                height: height,
                'margin-left': -width / 2,
                'margin-top': -height / 2
            }, 350, function () {
                $title_text.text(options.title || '提示');
                $loading.remove();
            });
        }
        $mask.fadeIn(250, function () {
            $close.one('click', function () {
                topWin.Gq.popup.closeDialog(options.callback);
            });
        });
        if (options.autoClose) {
            var that = this;
            $mask.one('click', function () {
                that.closeDialog(options.callback);
            });
            $(topDoc).one('keydown', function (e) {
                if (e.keyCode === 27) {
                    that.closeDialog(options.callback);
                }
            });
        }
    };
    Popup.prototype.closeDialog = function (callback) {
        var $body = $('body:first', doc),
            $mask = $body.find('#' + config.maskKey),
            $mini = $body.find('#' + config.dialogKey);
        $mini.animate({
            top: 0,
            'margin-top': -30 - $mini.outerHeight(true)
        }, 350, function () {
            $mask.fadeOut(450, function () {
                $mini.remove();
                $mask.remove();
                Gq.isFunction(callback) && callback.call();
            });
        });
    };

    var popup = new Popup();

    //dialog
    $('[gq-role="dialog"]', doc).on('click', function (e) {
        var href = $(this).attr('href'), title = $(this).attr('title');
        if (!href) return;
        if (/^#/.test(href)) {
            popup.maskDialog({
                title: title,
                content: $(href).html(),
                style: $(href).attr('gq-style'),
                autoClose: true
            });
        }
        else {
            if (Gq.device.isMoblie) return;
            if (win === win.top) {
                popup.maskDialog({
                    url: href,
                    autoClose: true
                });
            }
            else {
                if (href.indexOf('mode=dialog') === -1)
                    win.location.href = href + (href.indexOf('?') !== -1 ? '&' : '?') + 'mode=dialog';
                else
                    win.location.href = href;
            }
        }

        Gq.stope(e);
        return false;
    });
    //ajax
    $('a[gq-role="ajax"]', doc).on('click', function (e) {
        var $this = $(this),
            url = $this.attr('href');

        $.ajax({
            url: url,
            type: 'get',
            dataType: 'json',
            success: function (data) {
                data.Message && popup.tips(data.Message);
                data.Result && $this.css('cursor', 'not-allowed').attr('href', 'javascript:;');
            },
            error: function (e) { popup.tips('ajax load error'); }
        });

        Gq.stope(e);
        return false;
    });

    exports('popup', popup);
});