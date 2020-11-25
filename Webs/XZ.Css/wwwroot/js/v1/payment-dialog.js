;
(function (win) {
    var Gdialog = function () {
        this.complete = false;
        this.error = false;
        this.returnurl = '';
    };
    Gdialog.prototype.Render = function () {
        if (win.self === win.parent) {
            win.location.href = 'about:blank';
        }
    };
    Gdialog.prototype.Toggle = function () {
        this.Message('toggle', '');
    };
    Gdialog.prototype.Close = function (flag) {
        if (!flag && this.error) {
            win.history.go(-1);
        } else {
            this.Message(this.complete ? 'complete' : 'close', this.returnurl);
        }
    };
    Gdialog.prototype.Storage = function (id) {
        this.Message('storage', id + '|' + window.location.hash.replace('#', ''));
    };
    Gdialog.prototype.Message = function (type, msg) {
        win.parent.postMessage('gq_' + type + '|' + msg, '*');
    };
    var gdialog = new Gdialog();
    gdialog.Render();
    win.Gdialog = gdialog;
})(window, window.document);