Gq.define(['jquery', 'calendar'], function (exports) {
    "use strict";

    var $ = Gq.jquery,
        cd = Gq.calendar,
        win = Gq.win,
        doc = win.document;

    var Search = function () {
    };

    Search.prototype.config = {
        left: '#filter-view a.left',
        right: '#filter-view a.right',
        button: '#filter-view #filter-button',
        scroll: '#filter-view div.scroll',
        input: '#filter-view input[name="q"]',
        conditions: '#filter-view ul.conditions',
        values: '#filter-view ul.values',
        form: '#filter-form form[gq-role="filter-form"]',
        tmplDisplay: '<span class="display" gq-key="{key}">{display}</span>',
        tmplCondition: '<span class="condition gq-radius-little" gq-name="{name}" gq-type="{type}" gq-loading="{loading}" gq-complete="{complete}"><span class="text">{text}</span><span class="display" gq-key="{key}">{display}</span><a href="javascript:;" class="close gq-icon gq-radius-full gq-rotate">&#xe792;</a></span>',
        tmplLiCondition: '<li class="{cls}" gq-type="{type}" gq-name="{name}" gq-fill="{fill}">{text}</li>',
        tmplLiValue: '<li gq-name="{name}" gq-key="{key}" gq-value="{value}" title="{value}">{display}</li>',
        autoCondition: [],
        waitting: false
    };
    Search.prototype.cache = [];
    Search.prototype.left = function (range) {
        var that = this,
            $scroll = $(that.config.scroll),
            left = range || $scroll.scrollLeft() - 330;
        $scroll.stop(true, true).animate({
            scrollLeft: left
        }, 200, function () {
            that.position();
        });
    };
    Search.prototype.right = function (range) {
        var that = this,
            $scroll = $(that.config.scroll),
            left = range || $scroll.scrollLeft() + 330;
        $scroll.stop(true, true).animate({
            scrollLeft: left
        }, 200, function () {
            that.position();
        });
    };
    Search.prototype.position = function () {
        var that = this,
            $input = $(that.config.input),
            $scroll = $(that.config.scroll),
            $conditions = $(that.config.conditions),
            $values = $(that.config.values),
            left = $input.position().left,
            maxHeight = $(win).height() - top,
            cH = Math.min(maxHeight, $conditions.children('li').length * 33 - 1),
            vH = Math.min(maxHeight, $values.children('li').length * 33 - 1);

        left = Math.min(left, $scroll.outerWidth(true) - 200);

        $conditions.css({ left: left, height: cH });
        $values.css({ left: left, height: vH });
    };
    Search.prototype.append = function (type, name, key, text, display, complete, loading) {
        var that = this,
            $scroll = $(that.config.scroll),
            $condition = $scroll.find('span[gq-name="' + name + '"]');
        //生成新条件
        if ($condition.length === 0) {
            $condition = $(that.tmpl(that.config.tmplCondition, {
                type: type,
                name: name,
                key: key,
                text: text,
                display: display,
                complete: complete,
                loading: loading
            }));
            if (key === undefined || display === undefined) {
                $condition.find('span.display').remove();
            }
            //绑定移除事件
            $condition.find('a.close').one('click', function () {
                that.remove.call(that, type, name);
            });
            //添加当前
            $(that.config.input).before($condition);
        }
        else {//已有
            $condition.attr('gq-complete', complete).attr('gq-loading', loading);
            if (key === undefined || display === undefined) {
                return;
            }
            var $display = $condition.find('span[gq-key="' + key + '"]');
            if ($display.length === 0) {
                $display = $(that.tmpl(that.config.tmplDisplay, {
                    key: key,
                    display: display
                }));
                $condition.find('a.close').before($display);
            }
            else {
                $display.text(display);
            }
        }
    };
    Search.prototype.remove = function (type, name) {
        var that = this,
            $scroll = $(that.config.scroll),
            $form = $(that.config.form),
            $conditions = $(that.config.conditions),
            $values = $(that.config.values),
            $condition = $scroll.find('span[gq-type="' + type + '"][gq-name="' + name + '"]'),
            width = $condition.outerWidth(true);
        //重置表单
        if (type === 'checkbox' || type === 'radio') {
            $condition.find('span[gq-key]').each(function () {
                var _key = $(this).attr('gq-key');
                $form.find('#' + _key).prop('chekced', false).removeAttr('checked');
            });
        }
        else {
            $condition.find('span[gq-key]').each(function () {
                var _key = $(this).attr('gq-key');
                $form.find('#' + _key).val('');
            });
        }
        //移除并滚动
        $condition.remove();
        that.left(width);
        //重置选项
        that.position();
        $values.fadeOut(200);
        $conditions.fadeIn(200);
        //标记
        that.config.waitting = true;
    };
    Search.prototype.selectCondition = function (type, name, text, fill) {
        var that = this,
            $conditions = $(that.config.conditions),
            $values = $(that.config.values).empty(),
            $form = $(that.config.form),
            $input = $(that.config.input);
        //添加条件
        that.append(type, name, undefined, text, undefined, 'false', 'false');
        //隐藏选项
        $conditions.fadeOut(0);
        if (type === 'checkbox') {
            $form.find('input[type="checkbox"][name="' + name + '"]').each(function () {
                var _$this = $(this),
                    _id = _$this.attr('id'),
                    _value = _$this.val(),
                    _checked = _$this.is(':checked'),
                    _text = $form.find('label[for="' + _id + '"]').text();

                var _$value = $(that.tmpl(that.config.tmplLiValue, {
                    name: name,
                    key: _id,
                    value: _value,
                    display: _text
                }));
                if (_checked) {
                    _$value.addClass('selected');
                }
                _$value.appendTo($values);
            });
            //切换显示
            that.position.call(that);
            $values.fadeIn(300);
        }
        else if (type === 'select') {
            var $select = $form.find('select[name="' + name + '"]'),
                id = $select.attr('id');
            $select.children('option').each(function () {
                var _$this = $(this),
                    _value = _$this.val(),
                    _checked = _$this.is(':selected'),
                    _text = _$this.text();

                if (_value.length === 0) return;

                var _$value = $(that.tmpl(that.config.tmplLiValue, {
                    name: name,
                    key: id,
                    value: _value,
                    display: _text
                }));
                if (_checked) {
                    _$value.addClass('selected');
                }
                _$value.appendTo($values);
            });
            //切换显示
            that.position.call(that);
            $values.fadeIn(300);
        }
        else if (type === 'radio') {
            $form.find('input[type="radio"][name="' + name + '"]').each(function () {
                var _$this = $(this),
                    _id = _$this.attr('id'),
                    _value = _$this.val(),
                    _checked = _$this.is(':checked'),
                    _text = $form.find('label[for="' + _id + '"]').text();

                var _$value = $(that.tmpl(that.config.tmplLiValue, {
                    name: name,
                    key: _id,
                    value: _value,
                    display: _text
                }));
                if (_checked) {
                    _$value.addClass('selected');
                }
                _$value.appendTo($values);
            });
            //切换显示
            that.position.call(that);
            $values.fadeIn(300);
        }
        else if (type === 'date') {
            var _$dates = $form.find('[name="' + name + '"]');
            var _year = new Date().getFullYear(),
                _minY = _year - 5,
                _maxY = _year + 3;
            cd.init({
                field: $input[0],
                firstDay: 1,
                minDate: new Date(_minY + '-01-01'),
                maxDate: new Date(_maxY + '-12-31'),
                yearRange: [_minY, _maxY],
                onSelected: function (date) {
                    that.selectValue.call(that, name, name, date, date);
                    $input.removeAttr('gq-tag').val('');
                    cd.destroy();
                }
            });
            $input.attr('gq-tag', 'calendar');
            Gq.sleep(function () { $input.focus(); }, 200);
        }
        else {
            var _fArray = fill && fill.split('|');
            if (_fArray) {
                for (var i = 0; i < that.cache.length; ++i) {
                    if (that.cache[i].tag === fill) {
                        _init(that.cache[i].data);
                        return;
                    }
                }
                $.ajax({
                    url: _fArray[1],
                    method: _fArray[0],
                    async: false,
                    success: function (res) {
                        if (!res || Object.keys(res).length === 0) return;
                        that.cache.push({ tag: fill, data: res });
                        _init(res);
                    }
                });

                function _init(res) {
                    for (var _kp in res) {
                        var _$value = $(that.tmpl(that.config.tmplLiValue, {
                            name: name,
                            key: name,
                            value: _kp,
                            display: res[_kp]
                        }));
                        _$value.appendTo($values);
                    }
                    //切换显示
                    that.position.call(that);
                    $values.fadeIn(300);
                    //
                    $input.attr('gq-tag', 'fill');
                }
            }
            $input.focus();
        }

        $values.find('li').bind('click', function () {
            var _$this = $(this),
                _name = _$this.attr('gq-name'),
                _key = _$this.attr('gq-key'),
                _value = _$this.attr('gq-value'),
                _text = _$this.text();
            that.selectValue.call(that, _name, _key, _value, _text);
        });
        //标记
        that.config.waitting = true;
    };
    Search.prototype.selectValue = function (name, key, value, display) {
        var that = this,
            $form = $(that.config.form),
            $input = $(that.config.input),
            $conditions = $(that.config.conditions),
            $values = $(that.config.values);
        that.append(undefined, name, key, undefined, display, 'true', 'false');
        //设置表单值
        $form.find('#' + key).val(value);
        $form.find('#' + key).prop('checked', true);
        //重置选项
        that.position();
        $values.fadeOut(200);
        $conditions.fadeIn(200);
        $input.val('').removeAttr('gq-tag');
        //标记
        that.config.waitting = false;
    };
    Search.prototype.tmpl = function (tmp, data) {
        for (var p in data) {
            var reg = new RegExp('{' + p + '}', 'g');
            tmp = tmp.replace(reg, data[p]);
        }
        return tmp;
    };
    Search.prototype.submit = function () {
        $(this.config.form).submit();
    };
    Search.prototype.render = function () {
        var that = this,
            $conditions = $(that.config.conditions);
        //绑定事件
        $(that.config.left).bind('click', function () { that.left.call(that); });
        $(that.config.right).bind('click', function () { that.right.call(that); });
        $(that.config.button).bind('click', function () { that.submit.call(that); });
        $(that.config.input).bind('click', function () {
            var $scroll = $(that.config.scroll);
            that.config.waitting = $scroll.children('span[gq-complete="false"]').length > 0;
            if (!that.config.waitting) {
                that.position.call(that);
                $conditions.fadeIn(200);
            }
        });
        $(that.config.input).bind('keydown', function (e) {
            var _$input = $(this),
                _value = _$input.val().trim(),
                _fill = _$input.attr('gq-fill'),
                _tag = _$input.attr('gq-tag');
            if (e.keyCode === 13) {//回车
                if (_value.length > 0) {
                    if (that.config.waitting) {//提交当前条件
                        var _$current = $(that.config.scroll).find('span.condition[gq-complete="false"]'),
                            _type = _$current.attr('gq-type'),
                            _name = _$current.attr('gq-name');
                        if (_type === 'text') {
                            that.selectValue.call(that, _name, _name, _value, _value);
                            _$input.val('');
                        }
                    }
                    else {//自动识别
                        var _matched;
                        for (var i = 0; i < that.config.autoCondition.length; i++) {
                            var _match = that.config.autoCondition[i];
                            if (_match.type === 'reg' && new RegExp(_match.value).test(_value)) {
                                _matched = _match;
                                break;
                            }
                            else if (_match.type === 'default') {
                                _matched = _match;
                            }
                        }

                        if (_matched) {
                            if (/end$/i.test(_matched.key) || /^max/i.test(_matched.key)) {
                                _value = _value.replace(/[\-~]/, '');
                            }
                            $conditions.find('li[gq-name="' + _matched.key + '"]').trigger('click');
                            that.selectValue.call(that, _matched.key, _matched.key, _value, _value);
                            _$input.val('');
                        }
                    }
                }
                else {
                    $(that.config.form).submit();
                }
            }
            else if (e.keyCode === 8) {//退格
                if (_value.length === 0) {
                    //删除前一个
                    var _$last = $(that.config.scroll).find('span.condition:last'),
                        _type = _$last.attr('gq-type'),
                        _name = _$last.attr('gq-name');
                    _$last.length > 0 && that.remove.call(that, _type, _name);

                    _tag === 'calendar' && cd.destroy();
                }
            }
        });
        $(that.config.input).bind('keyup', function (e) {
            var _$input = $(this),
                _value = _$input.val().trim(),
                _fill = _$input.attr('gq-tag');
            if (_fill) {
                var _$values = $(that.config.values);
                _$values.children().each(function () {
                    var _$this = $(this),
                        _txt = _$this.text().toLowerCase(),
                        _val = _value.toLowerCase();
                    if (_txt.indexOf(_val) > -1) {
                        _$this.show();
                    }
                    else {
                        _$this.hide();
                    }
                });
            }
        });
        //初始化表单及条件
        $(that.config.form).children('div.filter-line').each(function () {
            var $item = $(this),
                $label = $item.find('label:eq(0)'),
                name = $label.attr('for'),
                text = $label.text(),
                $inputs = $item.find('[name="' + name + '"]'),
                type = ($inputs.attr('type') || $inputs[0].tagName).toLowerCase(),
                auto = $inputs.attr('gq-auto') || '',
                fill = $inputs.attr('gq-fill') || '';
            if (auto) {
                var auto_array = auto.split('|'),
                    auto_type = auto_array.shift(),
                    auto_value = auto_array.join('|');
                $(that.tmpl(that.config.tmplLiCondition, {
                    type: type,
                    name: name,
                    text: text,
                    fill: fill,
                    cls: auto_type
                })).appendTo($conditions);
                that.config.autoCondition.push({ 'key': name, 'type': auto_type, 'value': auto_value });
            }
            else {
                $(that.tmpl(that.config.tmplLiCondition, {
                    type: type,
                    name: name,
                    text: text,
                    fill: fill,
                    cls: ''
                })).appendTo($conditions);
            }
            //添加默认条件
            $inputs.each(function () {
                var _$this = $(this),
                    _key = _$this.attr('id'),
                    _display = '',
                    _falg = false;

                if (type === 'select') {
                    var _$selected = _$this.find('option:selected'),
                        _sv = _$selected.val();
                    _display = _$selected.text();
                    _falg = _$selected.length > 0 && _sv.length > 0;
                }
                else if (type === 'radio') {
                    _display = $item.find('label[for="' + _key + '"]').text();
                    _falg = $item.find('input#' + _key).is(':checked');
                }
                else if (type === 'checkbox') {
                    _display = $item.find('label[for="' + _key + '"]').text();
                    _falg = $item.find('input#' + _key).is(':checked');
                }
                else {
                    _display = _$this.val();
                    _falg = _display.length > 0;
                }

                if (_falg) {
                    that.append.call(that, type, name, _key, text, _display, 'true', 'false');
                }
            });
        });
        //选择事件
        $conditions.bind('mouseleave', function () {
            $(this).fadeOut(200);
        });
        $conditions.find('li').bind('click', function () {
            var $this = $(this),
                type = $this.attr('gq-type'),
                name = $this.attr('gq-name'),
                fill = $this.attr('gq-fill'),
                text = $this.text();
            that.selectCondition.call(that, type, name, text, fill);
        });
    };

    var search = new Search();

    //初始化
    search.render();

    exports('search', search);
});