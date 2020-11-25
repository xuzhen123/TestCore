Gq.define(['jquery', 'popup'], function (exports) {
    "use strict";

    var $ = Gq.jquery,
        win = Gq.win,
        doc = win.document,
        popup = Gq.popup;

    var Form = function () {
    };

    //控件渲染
    Form.prototype.render = function (type) {
        var $froms = $('form[gq-role="form"]'),
            $radios = $froms.find('input[type="radio"]:not([gq-role="no"])'),
            $checks = $froms.find('input[type="checkbox"]:not([gq-role="no"])');
        if ($radios.length > 0 || $checks.length > 0) {
            Gq.link(Gq.config.res1 + '/styles/magic-form.css', function () {
                $radios.each(function () {
                    $(this).addClass('magic-radio');
                });
                $checks.each(function () {
                    $(this).addClass('magic-checkbox');
                });
            });
        }
    };

    //表单验证
    Form.prototype.valid = function (e) {
        var target, type, action, method;
        if (e.currentTarget.nodeName.toLowerCase() === 'form') {
            target = e.currentTarget;
            type = target.target.toLowerCase();
            action = target.action;
            method = target.method;
        }
        else {
            var $that = $(e.currentTarget);
            target = $that.parents('form')[0];
            type = ($that.attr('formtarget') || target.target).toLowerCase();
            action = $that.attr('formaction') || target.action;
            method = ($that.attr('formmethod') || target.method).toLowerCase();
        }
        var $elems = $(target).find('input[gq-verify],select[gq-verify],textarea[gq-verify]'),
            error = false,
            message = null;

        Gq.each($elems, function (k, item) {
            var $this = $(item).removeClass('validation-error'),
                $parent = $this.parent(),
                $span = $parent.find('span.validation-field'),
                value = $this.val(),
                verifyAttr = $this.attr('gq-verify').replace(/&#39;/g, '\''),
                verifies = eval('(' + verifyAttr + ')');

            for (var i = 0; i < verifies.length; i++) {
                var _verify = verifies[i];
                if (_verify.type === 'required') {
                    error = !/[\S]+/.test(value);
                }
                //仅当输入值了才做以下校验
                else if (value.length > 0) {
                    if (_verify.type === 'length') {
                        error = (_verify.hasOwnProperty('min') && value.length < _verify.min) || (_verify.hasOwnProperty('max') && value.length > _verify.max);
                    }
                    else if (_verify.type === 'range') {
                        //先正则，防止特殊值parse能通过
                        error = !(/^((([1-9]\d{0,9})|0)|-(([1-9]\d{0,9})|0))(\.\d{1,6})?$/.test(value));
                        if (!error) {
                            var _num = parseFloat(value);
                            error = (_verify.hasOwnProperty('min') && _num < _verify.min) || (_verify.hasOwnProperty('max') && _num > _verify.max);
                        }
                    }
                    else if (_verify.type === 'number') {
                        error = !(/^((([1-9]\d{0,18})|0)|-(([1-9]\d{0,18})|0))(\.\d{1,6})?$/.test(value));
                    }
                    else if (_verify.type === 'regex') {
                        var _reg = new RegExp(_verify.pattern);
                        error = !(_reg.test(value));
                    }
                    else if (_verify.type === 'date') {
                        error = !(/^\d{4}(\-|\/|\.)\d{1,2}\1\d{1,2}$/.test(value));
                    }
                    else if (_verify.type === 'file') {
                        var _ele = $this[0];
                        if (_ele.files && _ele.files.length > 0) {
                            var _name = _ele.files[0].name,
                                _size = _ele.files[0].size,
                                _suf = _name.substring(_name.lastIndexOf('.')).toLowerCase();

                            if (_verify.hasOwnProperty('size') && _size > _verify.size * 1024000) {
                                error = true;
                            }
                            else if (_verify.hasOwnProperty('suffix') && _verify.suffix.indexOf(_suf) === -1) {
                                error = true;
                            }
                        }
                    }
                }

                if (error) {
                    if ($span.length > 0) {
                        $span.text(_verify.msg);
                    } else {
                        $parent.append('<span class="validation-field">' + _verify.msg + '</span>');
                    }
                    $this.addClass('validation-error').focus();
                    return error;
                }
            }

            $span.remove();
        });

        Gq.stope(e);
        if (error) return false;
        (method === 'post' && type !== '_blank') && popup.loading(120000);
        target.action = action;
        target.method = method;
        target.target = type;
        return true;
    };

    var form = new Form();

    //自动渲染
    form.render();
    //表单校验
    $(doc).on('submit', '[gq-role="form"]', form.valid).on('click', '[gq-role="submit"]', form.valid);

    exports('form', form);
});