;(function (win) {
    var Gpay = function () { };
    Gpay.prototype.Config = {
        msgUnsupport: 'Sorry, your browser does not support this payment method.',
        msgConfigure: 'Error with script-configure.',
        key: 'gpay-{SessionId}',
        forms: ['{Forms}'],
        actionUrl: '{ActionUrl}',
        loadingUrl: '{LoadingUrl}'
    };
    Gpay.prototype.Render = function () {
        var that = win.Gpay;
        if (!document.querySelector || !win.postMessage) {
            alert(that.Config.msgUnsupport);
            return;
        }
        if (win.addEventListener) {
            win.addEventListener('message', that.Message, false);
        } else if (win.attachEvent) {
            win.attachEvent('message', that.Message);
        }
        if (that.Config.forms.length === 0 || that.Config.forms[0].length === 0) {
            alert(that.Config.msgConfigure);
            return;
        }
        for (var k = 0; k < that.Config.forms.length; k++) {
            var $form = document.querySelector(that.Config.forms[k]);
            if ($form === null) continue;
            $form.action = that.Config.actionUrl + '#' + that.Config.forms[k];
            $form.method = 'post';
            $form.target = that.Config.key;
            var $mode = $form.querySelector('[name="Mode"]');
            if ($mode === null) {
                var input = document.createElement('input');
                input.setAttribute('name', 'Mode');
                input.setAttribute('type', 'hidden');
                input.setAttribute('value', 'Dialog');
                $form.appendChild(input);
            } else {
                $mode.value = 'Dialog';
            } if ($form.addEventListener) {
                $form.addEventListener('submit', show4submit, false);
            } else if ($form.attachEvent) {
                $form.attachEvent('onsubmit', show4submit);
            }
        }
        var div = document.createElement('div');
        div.id = that.Config.key;
        div.style.position = 'fixed';
        div.style.display = 'none';
        div.style.zIndex = '99999';
        div.style.left = '0';
        div.style.top = '0';
        div.style.right = '0';
        div.style.bottom = '0';
        div.style.width = '100%';
        div.style.height = '100%';
        div.style.backgroundColor = 'rgba(0,0,0,.55)';
        var img = document.createElement('img');
        img.setAttribute('alt', 'loading');
        img.setAttribute('src', that.Config.loadingUrl);
        img.style.position = 'absolute';
        img.style.left = '50%';
        img.style.top = '50%';
        img.style.marginLeft = '-16px';
        img.style.marginTop = '-16px';
        img.style.width = '32px';
        img.style.height = '32px';
        var frame = document.createElement('iframe');
        frame.setAttribute('allowtransparency', 'true');
        frame.setAttribute('name', that.Config.key);
        frame.setAttribute('scrolling', 'no');
        frame.setAttribute('frameborder', '0');
        frame.src = 'about:blank';
        frame.style.visibility = 'hidden';
        frame.style.pointerEvents = 'none';
        frame.style.width = '100%';
        frame.style.height = '100%';
        div.appendChild(img);
        div.appendChild(frame);
        document.body.appendChild(div);

        function show4submit(e) {
            var $div = document.getElementById(win.Gpay.Config.key);
            $div.style.display = 'block';
            e = e || win.event;
            e.stopPropagation ? e.stopPropagation() : e.cancelBubble = true;
            return false;
        }
    };
    Gpay.prototype.Message = function (event) {
        if (event && event.data) {
            var data = event.data.toString(),
                tmps = data.split('|'),
                flag = tmps[0];
            switch (flag) {
                case 'gq_close':
                    hide();
                    break;
                case 'gq_complete':
                    {
                        hide();
                        win.location.href = tmps[1];
                    }
                    break;
                case 'gq_storage':
                    {
                        var array = tmps[1].split('|'),
                            $form = document.querySelector(array[1]);
                        if ($form === null) return;
                        var $id = $form.querySelector('[name="TransactionId"]');
                        if ($id === null) {
                            var input = document.createElement('input');
                            input.setAttribute('name', 'TransactionId');
                            input.setAttribute('type', 'hidden');
                            input.setAttribute('value', array[0]);
                            $form.appendChild(input);
                        } else {
                            $id.value = array[0];
                        }
                    }
                    break;
                case 'gq_toggle':
                    {
                        var $div = document.getElementById(win.Gpay.Config.key),
                            $frame = $div.querySelector('iframe'),
                            $img = $div.querySelector('img');
                        document.body.style.overflow = 'hidden';
                        $div.style.backgroundColor = null;
                        $img.style.display = 'none';
                        $frame.style.visibility = 'visible';
                        $frame.style.pointerEvents = 'all';
                    }
                    break;
            }
        }

        function hide() {
            var $div = document.getElementById(win.Gpay.Config.key),
                $frame = $div.querySelector('iframe'),
                $img = $div.querySelector('img');
            document.body.style.overflow = null;
            $div.style.display = 'none';
            $div.style.backgroundColor = 'rgba(0,0,0,.55)';
            $img.style.display = 'block';
            $frame.style.display = 'none';
        }
    };
    var gpay = new Gpay();
    if (win.addEventListener) {
        win.addEventListener('load', gpay.Render, false);
    } else if (win.attachEvent) {
        win.attachEvent('onload', gpay.Render);
    } else {
        alert(gpay.Config.msgUnsupport);
    }
    win.Gpay = gpay;
})(window, window.document);