(function (win) {
    var Gtacker = function () { };
    Gtacker.prototype.Config = {
        FId: "{FId}",
        Url: "{Url}"
    };
    Gtacker.prototype.Render = function () {
        var that = win.Gtacker;
        var soul = new Object();
        soul.fingerprint = null; //设备指纹
        soul.browserLanguage = null;//浏览器语言
        soul.browserName = null;//浏览器名称
        soul.browserVersion = null;//浏览器版本
        soul.cookieEnabled = false;//cookie是否启用
        soul.coordinate = null;//地理坐标
        soul.cPUArchitecture = null;//处理器架构
        soul.deviceModel = null;//设备型号
        soul.deviceName = null;//设备名称
        soul.flashEnabled = false;//flash是否启用
        soul.imageEnabled = false;//图像是否启用
        soul.isMobileDevice = false;//是否为移动设备
        soul.javaScriptEnabled = false;//JavaScript 已启用
        soul.oSLanguage = null;//操作系统语言
        soul.oSName = null;//操作系统名称
        soul.oSVersion = null;//操作系统版本
        soul.screenPixelRatio = null;//屏幕解析度
        soul.screenResolution = null;//屏幕分辨率
        soul.silverlightEnabled = false;//Silverlight 已启用
        soul.silverlightVersion = null;//Silverlight 版本
        soul.timeZone = null;//时区

        var frame = document.createElement("iframe");
        frame.setAttribute("allowtransparency", "true");
        frame.setAttribute("name", "gtacker-" + that.Config.FId);
        frame.setAttribute("scrolling", "no");
        frame.setAttribute("frameborder", "0");
        frame.src = "about:blank";
        frame.style.display = "none";
        frame.style.width = "0";
        frame.style.height = "0";
        document.body.appendChild(frame);
        var form = document.createElement("form");
        form.setAttribute("id", "gtff");
        form.setAttribute("method", "post");
        form.setAttribute("action", that.Config.Url);
        for (prop in soul) {
            var input = document.createElement("input");
            input.setAttribute("name", prop);
            input.setAttribute("type", "hidden");
            input.setAttribute("value", soul[prop]);
            form.appendChild(input);
        }
        frame.contentDocument.body.appendChild(form);
        var script = document.createElement("script");
        script.setAttribute("type", "text/javascript");
        var sctxt = document.createTextNode('document.getElementById("gtff").submit();');
        script.appendChild(sctxt);
        frame.contentDocument.body.appendChild(script);
    };
    var gt = new Gtacker();
    if (win.addEventListener) {
        win.addEventListener("load", gt.Render, false);
    } else if (win.attachEvent) {
        win.attachEvent("onload", gt.Render);
    }
    win.Gtacker = gt;
})(window, window.document);