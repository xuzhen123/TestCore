; !function (win) {
    "use strict";

    var gof = function () {
        this.v = '1.0.0'; //版本号
        this.win = win;//使用的窗体对象
    };

    //扩展
    gof.fn = gof.prototype;

    var doc = win.document,
        op = Object.prototype,
        ostring = op.toString,
        config = gof.fn.config = {
            routeExt: '',//路由后缀
            imgSize: 5242880,
            timeout: 10,//模块最长请求秒数
            res1: '//static.gof-dev2.com',
            res2: '//static.gof-dev2.com'
        },
        cache = gof.fn.cache = {
            modules: {},//记录模块加载地址
            status: {}//记录模块加载状态
        },
        //获取Gq所在目录
        getPath = function () {
            var js = doc.scripts, jsPath = js[js.length - 1].src;
            return jsPath.substring(0, jsPath.lastIndexOf('/') + 1);
        }(),
        //异常提示
        error = gof.fn.error = function (msg) {
            win.console && console.error && console.error('Gq error: ' + msg);
        },
        //模块配置
        modules = {
            calendar: 'modules/calendar', //日历
            contextmenu: 'modules/contextmenu', //右键菜单
            creditcard: 'modules/creditcard', //信用卡
            form: 'modules/form', //表单校验
            popup: 'modules/popup', //弹层
            gesture: 'modules/gesture', //手势效果
            gridsort: 'modules/gridsort', //表格排序
            search: 'modules/search', //搜索
            tpl: 'modules/tpl', //模版引擎
            //第三方
            jquery: 'libs/jquery' //jquery
        };

    //设备信息
    gof.fn.device = {
        //是否为已知的移动端设备
        isMoblie: function () {
            var u = navigator.userAgent.toLowerCase();
            var mobileAgent = new Array("iphone", "ipod", "ipad", "android", "mobile", "wxwork", "blackberry", "webos", "incognito", "webmate", "bada", "nokia", "lg", "ucweb", "skyfire");
            for (var i = 0; i < mobileAgent.length; i++) {
                if (u.indexOf(mobileAgent[i]) != -1) {
                    return true;
                }
            }
            return false;
        }(),
        //设备宽度
        Width: win.screen.width || win.screen.availWidth,
        //设备高度
        height: win.screen.height || win.screen.availHeight
    };

    //定义模块
    gof.fn.define = function (deps, callback) {
        var that = this,
            mods = function () {
                that.isFunction(callback) && callback(function (app, exports) {
                    Gq[app] = exports;
                    cache.status[app] = true;
                });
                return this;
            };
        that.isFunction(deps) && (
            callback = deps,
            deps = []
        );
        that.use(deps, mods);
        return that;
    };

    //使用特定模块
    gof.fn.use = function (apps, callback, exports) {
        var that = this;

        apps = that.isString(apps) ? [apps] : apps;
        exports = exports || [];

        if (win.jQuery) {
            that.each(apps, function (index, item) {
                if (item === 'jquery') {
                    apps.splice(index, 1);
                }
            });
            Gq.jquery = jQuery;
        }

        if (apps.length === 0) {
            return typeof callback === 'function' && callback.apply(Gq, exports), that;
        }

        //加载模块
        var dir = cache.dir = cache.dir ? cache.dir : getPath,
            head = doc.getElementsByTagName('head')[0],
            item = apps[0],
            timeout = 0,
            node = doc.createElement('script'),
            url = dir + (modules[item] ? modules[item] : item) + '.js';
        node.async = true;
        node.charset = 'utf-8';
        node.src = url;

        if (cache.modules[item]) {
            (function poll() {
                if (++timeout > config.timeout * 1000 / 5) {
                    return error(item + ' is not a valid module');
                };
                (that.isString(cache.modules[item]) && cache.status[item]) ? onCallback() : setTimeout(poll, 5);
            }());
        } else {
            //首次加载
            head.appendChild(node);
            if (node.attachEvent) {
                node.attachEvent('onreadystatechange', function (e) {
                    onScriptLoad(e, url);
                });
            } else {
                node.addEventListener('load', function (e) {
                    onScriptLoad(e, url);
                }, false);
            }
        }

        cache.modules[item] = url;

        function onScriptLoad(e, url) {
            var readyRegExp = /^(complete|loaded)$/;
            if (e.type === 'load' || (readyRegExp.test((e.currentTarget || e.srcElement).readyState))) {
                cache.modules[item] = url;
                head.removeChild(node);
                (function poll() {
                    if (++timeout > config.timeout * 1000 / 5) {
                        return error(item + ' is not a valid module');
                    };
                    cache.status[item] ? onCallback() : setTimeout(poll, 5);
                }());
            }
        }

        function onCallback() {
            exports.push(Gq[item]);
            apps.length > 1 ? that.use(apps.slice(1), callback, exports) : (that.isFunction(callback) && callback.apply(Gq, exports));
        }

        return that;
    };

    //css加载器
    gof.fn.link = function (url, fn) {
        var that = this,
            head = doc.getElementsByTagName('head')[0],
            link = doc.createElement('link'),
            app = 'gq-' + url.substring(url.lastIndexOf('/') + 1, url.lastIndexOf('.')).replace(/\.|\//g, ''),
            timeout = 0;

        link.id = app;
        link.rel = 'stylesheet';
        link.href = url;
        link.media = 'all';

        if (!doc.getElementById(app)) {
            head.appendChild(link);
        }

        if (!that.isFunction(fn)) return;

        if (link.attachEvent) {
            link.attachEvent('onload', function () { fn.call(link) });
        } else {
            var webkit = /webkit/i.test(navigator.userAgent), loaded = false;
            //轮询css是否加载完毕
            (function poll() {
                if (++timeout > config.timeout * 1000 / 100) {
                    return error(url + ' load timeout');
                };

                if (webkit) {
                    loaded = link.sheet;
                } else if (link.sheet) {
                    try {
                        loaded = link.sheet.cssRules;
                    } catch (e) {
                        loaded = (e.code && e.code === 1000);
                    }
                }

                loaded ? function () { fn.call(link); }() : setTimeout(poll, 100);
            }());
        }
    };

    //初始化设置
    gof.fn.options = function (config, options) {
        if (typeof options === 'undefined') return;

        for (var key in config) {
            if (options.hasOwnProperty(key)) {
                config[key] = options[key];
            }
        }
    };

    //是否为方法
    gof.fn.isFunction = function (it) {
        return ostring.call(it) === '[object Function]';
    };

    //是否为数组
    gof.fn.isArray = function (it) {
        return ostring.call(it) === '[object Array]';
    };

    //是否为字符串
    gof.fn.isString = function (it) {
        return ostring.call(it) === '[object String]';
    };

    //是否为空或空白字符组成
    gof.fn.isEmpty = function (s) {
        return s.replace(/[ ]/g, '').length == 0
    };

    //遍历
    gof.fn.each = function (obj, fn) {
        var that = this, key;
        if (!that.isFunction(fn)) return that;
        obj = obj || [];
        if (obj.constructor === Object) {
            for (key in obj) {
                if (fn.call(obj[key], key, obj[key])) break;
            }
        } else {
            for (key = 0; key < obj.length; key++) {
                if (fn.call(obj[key], key, obj[key])) break;
            }
        }
        return that;
    };

    //阻止事件冒泡
    gof.fn.stope = function (e) {
        e = e || win.event;
        e.stopPropagation ? e.stopPropagation() : e.cancelBubble = true;
        //e.preventDefault ? e.preventDefault() : win.event.returnValue = false; 
    };

    //是否支持css属性
    gof.fn.supportcss = function (attr) {
        var prefix = ['webkit', 'Moz', 'ms', 'o'],
            styles = document.documentElement.style,
            _toAttr = function (str) {
                return str.replace(/-(\w)/g, function ($0, $1) {
                    return $1.toUpperCase();
                });
            },
            attrs = [_toAttr(attr)],
            i;

        for (i in prefix) attrs.push(_toAttr(prefix[i] + '-' + attr));

        for (i in attrs) if (attrs[i] in styles) return true;

        return false;
    };

    //等待时间后执行
    gof.fn.sleep = function (callback, timer, args) {
        win.setTimeout(args ? function () {
            callback(args);
        } : callback, timer);
    };

    //组成链接地址
    gof.fn.urlString = function () {
        var url = '';
        for (var i = 0; i < arguments.length; i++) {
            url += '/' + arguments[i]
        }
        return url ? (url + config.routeExt) : url
    };

    //获取get参数
    gof.fn.getRequest = function () {
        var url = win.location.search;
        var query = new Object();
        if (url.indexOf('?') != -1) {
            var array = url.substr(1).split('&');
            for (var i = 0; i < array.length; i++) {
                var item = array[i].split('=');
                query[item[0]] = item[1];
            }
        }
        return query;
    };

    //返回顶端
    gof.fn.goTop = function () {
        function x() {
            return (doc.documentElement && doc.documentElement.scrollTop) ? doc.documentElement.scrollTop : doc.body.scrollTop;
        }
        var t = x() / 30;
        function y() {
            var _value = x();
            if (_value > 0) {
                win.setTimeout(function () {
                    win.scrollTo(0, _value - t);
                    y();
                }, 10);
            }
        }
        y();
    };

    //导航切换
    gof.fn.nav = function () {
        var nav = document.getElementById('nav-view'),
            _class = nav.className;
        if (_class.indexOf('open') == -1) {
            nav.className = 'open';
        }
        else {
            nav.className = '';
        }
    };

    //移动导航切换
    gof.fn.mNav = function () {
        var nav = document.getElementById('nav-view'),
            _class = nav.className;
        if (_class.indexOf('mopen') == -1) {
            nav.className = 'mopen open';
        }
        else {
            nav.className = 'open';
        }
    };

    win.Gq = new gof();
}(window);